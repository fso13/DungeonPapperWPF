using DungeonPapperWPF.code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DungeonPapperWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SolidColorBrush brushGreen = new SolidColorBrush(Colors.Green);
        public Array valuesOutlook = Enum.GetValues(typeof(Outlook));
        public Random random = new Random(DateTime.Now.Millisecond);


        public static Dice[] dices = new Dice[6];

        private static bool isStart = false;
        private static FieldDto[,] fieldDtos;

        private int step = 0;
        public static int currentCountStep = 0;
        public static int currentCountLevel = 0;

        public static ImageBrush riverBrushVertical = new ImageBrush();
        public static ImageBrush riverBrushHorizontal = new ImageBrush();
        public static ImageBrush woodyBrushVertical = new ImageBrush();
        public static ImageBrush woodyBrushHorizontal = new ImageBrush();
        public static ImageBrush trapBrush = new ImageBrush();

        public static List<Field> path = new List<Field>();

        private static Field[,] fields = new Field[7, 6];

        private static int hp = 4;
        private static int blood = 0;
        public static int levelWarrior = 1;
        public static int levelWizard = 1;
        public static int levelCleric = 1;
        public static int levelPlut = 1;


        public static HeroClass warrior;
        public static HeroClass wizard;
        public static HeroClass cleric;
        public static HeroClass plut;


        public MainWindow()
        {
            SplashScreen splashScreen = new SplashScreen("Resources/logo.png");
            splashScreen.Show(false);
            /* do some things */


            riverBrushVertical.ImageSource =
                    new BitmapImage(new Uri(@"Resources\river_vertical.png", UriKind.Relative));
            riverBrushHorizontal.ImageSource =
                    new BitmapImage(new Uri(@"Resources\river_horizontal.png", UriKind.Relative));
            woodyBrushVertical.ImageSource =
                    new BitmapImage(new Uri(@"Resources\woody_vertical.png", UriKind.Relative));
            woodyBrushHorizontal.ImageSource =
                    new BitmapImage(new Uri(@"Resources\woody_horizontal.png", UriKind.Relative));
            trapBrush.ImageSource =
                    new BitmapImage(new Uri(@"Resources\trap.png", UriKind.Relative));
            splashScreen.Close(TimeSpan.FromSeconds(5));
            System.Threading.Thread.Sleep(5000);
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fieldDtos = Quests.getQuest(0);

            drawField();
        }

        private void drawField()
        {
            for (int i = 0; i < 6; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                gridFields.ColumnDefinitions.Add(colDef);
            }

            for (int j = 0; j < 7; j++)
            {
                RowDefinition rowDef = new RowDefinition();
                gridFields.RowDefinitions.Add(rowDef);
            }

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    drawField(i, j);
                }
            }
        }

        private void drawField(int i, int j)
        {
            FieldDto dto = fieldDtos[j, i];

            Field f = new Field();

            if (dto.trap)
            {
                f.trapField.Fill = trapBrush;
            }

            if (dto.prey != null)
            {
                ImageBrush preyBrush = new ImageBrush();
                preyBrush.ImageSource = new BitmapImage(dto.prey.getPath());
                f.preyField.Fill = preyBrush;
            }

            if (dto.monster != null)
            {
                ImageBrush monsterBrush = new ImageBrush();
                monsterBrush.ImageSource = new BitmapImage(dto.monster.getPathMonster());
                f.monsterField.Fill = monsterBrush;

                ImageBrush heroLevelBrush = new ImageBrush();
                heroLevelBrush.ImageSource = new BitmapImage(dto.monster.getPathHeroLevel());
                f.levelHeroField.Fill = heroLevelBrush;
            }


            if (dto.rightBarrier == Barrier.Wall)
            {
                f.rightWallButton.Fill = woodyBrushVertical;
            }
            if (dto.leftBarrier == Barrier.Wall && i == 0)
            {
                f.leftWallButton.Fill = woodyBrushVertical;
            }
            if (dto.topBarrier == Barrier.Wall)
            {
                f.topWallButton.Fill = woodyBrushHorizontal;
            }


            if (dto.rightBarrier == Barrier.Black)
            {
                f.rightWallButton.Fill = Brushes.Black;
            }
            if (dto.leftBarrier == Barrier.Black && i == 0)
            {
                f.leftWallButton.Fill = Brushes.Black;
            }
            if (dto.topBarrier == Barrier.Black)
            {
                f.topWallButton.Fill = Brushes.Black;
            }


            if (dto.leftBarrier == Barrier.River)
            {
                f.leftWallButton.Fill = riverBrushVertical;
            }
            if (dto.downBarrier == Barrier.River)
            {
                f.downWallButton.Fill = riverBrushHorizontal;
            }


            f.Name = String.Format("field_{0}_{1}", i, j);
            Grid.SetRow(f, j);
            Grid.SetColumn(f, i);

            f.dto = dto;
            fields[j, i] = f;
            gridFields.Children.Add(f);
        }

        private void cbox_quest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            fieldDtos = Quests.getQuest(cbox_quest.SelectedIndex);

            gridFields.Children.Clear();
            gridFields.RowDefinitions.Clear();
            gridFields.ColumnDefinitions.Clear();

            drawField();
            //gridFields.UpdateLayout();
        }


        public Field[] GetColumn(int columnNumber)
        {
            return Enumerable.Range(0, fields.GetLength(0))
                    .Select(x => fields[x, columnNumber])
                    .ToArray();
        }

        public Field[] GetRow(int rowNumber)
        {
            return Enumerable.Range(0, fields.GetLength(1))
                    .Select(x => fields[rowNumber, x])
                    .ToArray();
        }


        private void TwoMoveButton_Click(object sender, RoutedEventArgs e)
        {
            currentCountStep = 2;

            highlightWhereToGo();
        }


        public static void highlightWhereToGo()
        {
            if (currentCountStep > 0)
            {
                if (path.Count == 0)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        fields[6, i].greanColor();
                    }
                }
                else
                {

                    for (int i = 0; i < 6; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            fields[j, i].clearColor();
                        }
                    }


                    Field last = path.Last();

                    if (last.dto.leftBarrier == Barrier.None)
                    {
                        if (last.dto.x > 0)
                        {
                            Field left = fields[last.dto.y, last.dto.x - 1];
                            if (left.dto.rightBarrier == Barrier.None)
                            {
                                left.greanColor();
                            }
                        }
                        else if (last.dto.x == 0)
                        {
                            Field left = fields[last.dto.y, 5];
                            left.greanColor();
                        }
                    }


                    if (last.dto.rightBarrier == Barrier.None)
                    {
                        if (last.dto.x < 5)
                        {
                            Field right = fields[last.dto.y, last.dto.x + 1];
                            if (right.dto.leftBarrier == Barrier.None)
                            {
                                right.greanColor();
                            }
                        }
                        else if (last.dto.x == 5)
                        {
                            Field right = fields[last.dto.y, 0];
                            right.greanColor();
                        }
                    }


                    if (last.dto.topBarrier == Barrier.None)
                    {
                        if (last.dto.y > 0)
                        {
                            Field top = fields[last.dto.y - 1, last.dto.x];
                            if (top.dto.downBarrier == Barrier.None)
                            {
                                top.greanColor();
                            }
                        }

                    }

                    if (last.dto.downBarrier == Barrier.None)
                    {
                        if (last.dto.y < 6)
                        {
                            Field down = fields[last.dto.y + 1, last.dto.x];
                            if (down.dto.topBarrier == Barrier.None)
                            {
                                down.greanColor();
                            }
                        }

                    }

                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        fields[j, i].clearColor();
                    }
                }
            }

        }



        private Outlook randomOutlook()
        {
    
            return(Outlook)valuesOutlook.GetValue(random.Next(10000) % valuesOutlook.Length);
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if(!isStart && cbox_quest.SelectedIndex > 0)
            {
                isStart = true;

                TwoMoveButton.IsEnabled = true;
                LevelUpButton.IsEnabled = true;
                buttonDiceGenereted.IsEnabled = true;

                warrior = new HeroClass(HeroClassType.Warrior, 1, randomOutlook());
                wizard = new HeroClass(HeroClassType.Wizard, 1, randomOutlook());
                cleric = new HeroClass(HeroClassType.Cleric, 1, randomOutlook());
                plut = new HeroClass(HeroClassType.Plut, 1, randomOutlook());

                drawLevel();
                drawOutlook();

            }
        }

        public void drawOutlook()
        {

            List<HeroClass> heroes = new List<HeroClass>() { warrior, wizard, cleric, plut };


            heroes.ForEach(hero => {

                if(hero.outlook == Outlook.Black)
                {
                    ((CheckBox)this.FindName(hero.getControlOutlookName())).IsChecked = true;
                }
            });

        }

        public void drawLevel()
        {

            List<HeroClass> heroes = new List<HeroClass>() { warrior, wizard, cleric, plut };


            heroes.ForEach(hero => {

                    for (int i = 0; i < hero.level; i++)
                    {
                        ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + (i + 1))).IsChecked = true;
                    }
            }); 
            
        }


        public static void damage(int damage)
        {
            //todo потом учитывать зелья
            blood += damage;

            MessageBox.Show("здоровье стало: " + (hp - blood));
        }

        public void levelUpFromMove()
        {
            List<HeroClass> heroes = new List<HeroClass>() { warrior, wizard, cleric, plut };


            heroes.ForEach(hero => {
                ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + (hero.level + 1))).IsEnabled = true;
            });
            MessageBox.Show("Повышение уровня");
            currentCountLevel++;

        }

        public void levelUp(HeroClassType type)
        {
            List<HeroClass> heroes = new List<HeroClass>() { warrior, wizard, cleric, plut };

            heroes.ForEach(hero => {

                if(hero.type == type)
                {
                    for (int i = 0; i < hero.level; i++)
                    {
                        ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + (i + 1))).IsChecked = true;
                    }
                }
               
            });

            drawLevel();
            hp ++;

        }

        private void cbox_Level_Checked(object sender, RoutedEventArgs e)
        {
            if(currentCountLevel > 0)
            {
                CheckBox checkBox = (CheckBox)sender;
                checkBox.IsChecked = true;
                string name = checkBox.Name;
                List<HeroClass> heroes = new List<HeroClass>() { warrior, wizard, cleric, plut };

                heroes.ForEach(hero => {

                    if (name.Contains(hero.type.ToString().ToLower()))
                    {
                        hero.level++;
                        hp++;
                        currentCountLevel--;
                    }

                });
                heroes.ForEach(hero => {

                    for (int i = 0; i <= hero.level; i++)
                    {
                        ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + (i + 1))).IsEnabled = false;
                    }

                });


            }
        }

        private void buttonDiceGenereted_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                dices[i] = Dice.fromNumber(random.Next(100 % 12));
                Rectangle rectangle = ((Rectangle)this.FindName("dice" + (i+1) + "_pic"));
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = dices[i].getPath();
                rectangle.Fill = brush;
            }
        }
    }
}
