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
        SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);
        public Array valuesOutlook = Enum.GetValues(typeof(Outlook));
        public Random random = new Random(DateTime.Now.Millisecond);


        public static Dice[] dices = new Dice[6];
        public static Dice currentDice;
        public static List<Dice> selectDices = new List<Dice>();

        private static bool isStart = false;
        private static FieldDto[,] fieldDtos;

        private int round = 0;
        public static int currentCountStep = 0;
        public static int currentCountLevel = 0;
        public static int currentCountDice = 0;

        public static ImageBrush riverBrushVertical = new ImageBrush();
        public static ImageBrush riverBrushHorizontal = new ImageBrush();
        public static ImageBrush woodyBrushVertical = new ImageBrush();
        public static ImageBrush woodyBrushHorizontal = new ImageBrush();
        public static ImageBrush trapBrush = new ImageBrush();

        public static List<Field> path = new List<Field>();

        private static Field[,] fields = new Field[7, 6];

        private int hp = 0;
        private int blood = 0;


        public static HeroClass warrior;
        public static HeroClass wizard;
        public static HeroClass cleric;
        public static HeroClass plut;

        public Party party;


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

            deleteCurrentDic();
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

            return (Outlook)valuesOutlook.GetValue(random.Next(10000) % valuesOutlook.Length);
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isStart && cbox_quest.SelectedIndex > 0)
            {
                party = new Party();
                isStart = true;
                addHp();
                addHp();
                addHp();
                addHp();
                //TwoMoveButton.IsEnabled = true;
                //LevelUpButton.IsEnabled = true;
                buttonDiceGenereted.IsEnabled = true;

                warrior = new HeroClass(HeroClassType.Warrior, 1, randomOutlook());
                wizard = new HeroClass(HeroClassType.Wizard, 1, randomOutlook());
                cleric = new HeroClass(HeroClassType.Cleric, 1, randomOutlook());
                plut = new HeroClass(HeroClassType.Plut, 1, randomOutlook());

                party.warrior = warrior;
                party.wizard = wizard;
                party.cleric = cleric;
                party.plut = plut;

                drawLevel();
                drawOutlook();

                cbox_quest.IsEnabled = false;
                startButton.IsEnabled = false;

            }
        }

        public void drawOutlook()
        {

            List<HeroClass> heroes = new List<HeroClass>() { warrior, wizard, cleric, plut };


            heroes.ForEach(hero =>
            {

                if (hero.outlook == Outlook.Black)
                {
                    ((CheckBox)this.FindName(hero.getControlOutlookName())).IsChecked = true;
                }
            });

        }

        public void drawLevel()
        {

            List<HeroClass> heroes = new List<HeroClass>() { warrior, wizard, cleric, plut };


            heroes.ForEach(hero =>
            {

                for (int i = 0; i < hero.level; i++)
                {
                    ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + (i + 1))).IsChecked = true;
                }
            });

        }


        public void damage(int damage)
        {
            blood += damage;
            for (int i = 1; i <= blood; i++)
            {
                ((CheckBox)this.FindName("blood_" + i)).IsChecked = true;
            }
            //todo потом учитывать зелья

            MessageBox.Show("здоровье стало: " + (hp - blood));
        }

        public void levelUpFromMove()
        {
            List<HeroClass> heroes = new List<HeroClass>() { warrior, wizard, cleric, plut };


            heroes.ForEach(hero =>
            {
                ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + (hero.level + 1))).IsEnabled = true;
            });
            MessageBox.Show("Повышение уровня");
            currentCountLevel++;

        }

        public void levelUp(HeroClassType type)
        {
            List<HeroClass> heroes = new List<HeroClass>() { warrior, wizard, cleric, plut };

            heroes.ForEach(hero =>
            {

                if (hero.type == type)
                {
                    for (int i = 0; i < hero.level; i++)
                    {
                        ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + (i + 1))).IsChecked = true;
                    }
                }

            });

            drawLevel();
            hp++;

        }

        private void LevelUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentDice != null)
            {
                currentCountLevel++;

                List<HeroClass> heroes = new List<HeroClass>() { warrior, wizard, cleric, plut };


                heroes.ForEach(hero =>
                {

                    if (hero.getNumberDiceForLevel() == currentDice.number)
                    {
                        ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + (hero.level + 1))).IsEnabled = true;
                    }
                });
            }
        }

        private void cbox_Level_Checked(object sender, RoutedEventArgs e)
        {
            if (currentCountLevel > 0)
            {
                CheckBox checkBox = (CheckBox)sender;
                checkBox.IsChecked = true;
                string name = checkBox.Name;
                List<HeroClass> heroes = new List<HeroClass>() { warrior, wizard, cleric, plut };

                heroes.ForEach(hero =>
                {

                    if (name.Contains(hero.type.ToString().ToLower()))
                    {
                        hero.level++;
                        addHp();
                        currentCountLevel--;
                        deleteCurrentDic();
                    }

                });
                heroes.ForEach(hero =>
                {

                    for (int i = 0; i <= hero.level; i++)
                    {
                        ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + (i + 1))).IsEnabled = false;
                    }

                });


            }
        }

        private void deleteCurrentDic()
        {
            if (currentDice != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    Rectangle rectangle = ((Rectangle)this.FindName("dice" + (i + 1) + "_pic"));

                    if(!selectDices.Contains(rectangle.Tag as Dice) && ((Dice)rectangle.Tag).number<10)
                    {
                        rectangle.IsEnabled = true;
                    }
                    
                }

                currentDice.rectangle.Stroke = null;
                currentDice.rectangle.Fill = null;
                currentDice.rectangle.IsEnabled = false;
                currentDice = null;
                currentCountDice--;

                if (currentCountDice == 0)
                {
                    buttonDiceGenereted.IsEnabled = true;

                    for (int i = 0; i < 6; i++)
                    {
                        Rectangle rectangle = ((Rectangle)this.FindName("dice" + (i + 1) + "_pic"));
                        rectangle.Stroke = null;
                        rectangle.Fill = null;
                        rectangle.IsEnabled = false;
                    }
                }
            }
            selectDiceButton.IsEnabled = true;

            LevelUpButton.IsEnabled = false;
            TwoMoveButton.IsEnabled = false;
        }

        private void buttonDiceGenereted_Click(object sender, RoutedEventArgs e)
        {
            if (round < 8)
            {
                if (currentCountDice == 0)
                {
                    selectDices.Clear();
                    buttonDiceGenereted.IsEnabled = false;
                    round++;
                    currentCountDice = 3;
                    for (int i = 0; i < 6; i++)
                    {
                        Rectangle rectangle = ((Rectangle)this.FindName("dice" + (i + 1) + "_pic"));
                        rectangle.IsEnabled = true;
                        dices[i] = Dice.fromNumber(random.Next(12));
                        dices[i].rectangle = rectangle;
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = dices[i].getPath();
                        rectangle.Fill = brush;
                        rectangle.Tag = dices[i];
                        if (dices[i].number > 9)
                        {
                            damage(1);
                            rectangle.IsEnabled = false;
                            rectangle.Stroke = brushRed;
                            rectangle.StrokeThickness = 4;
                        }
                    }
                }
            }
        }

        public void addHp()
        {
            hp++;
            ((CheckBox)this.FindName("hp_" + hp)).IsChecked = true;
        }

        private void dice_pic_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                Rectangle r = ((Rectangle)this.FindName("dice" + (i + 1) + "_pic"));
                if (r.Stroke == brushGreen)
                {
                    r.Stroke = null;
                }
            }

            Rectangle rectangle = (Rectangle)sender;
                rectangle.Stroke = brushGreen;
                rectangle.StrokeThickness = 4;
                currentDice = rectangle.Tag as Dice;

            

        }

        private void selectDiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentDice != null)
            {
                selectDiceButton.IsEnabled = false;
                selectDices.Add(currentDice);

                for (int i = 0; i < 6; i++)
                {
                    ((Rectangle)this.FindName("dice" + (i + 1) + "_pic")).IsEnabled=false;
                }

                TwoMoveButton.IsEnabled = true;

                if (currentDice.number > 0 && currentDice.number < 9)
                {
                    if (cleric.level > 3)
                    {
                        LevelUpButton.IsEnabled = true;
                    }
                    else
                    {
                        if (warrior.outlook == Outlook.White && warrior.level < 6 && currentDice.number == 1)
                        {
                            LevelUpButton.IsEnabled = true;
                        }
                        if (wizard.outlook == Outlook.White && wizard.level < 6 && currentDice.number == 2)
                        {
                            LevelUpButton.IsEnabled = true;
                        }
                        if (cleric.outlook == Outlook.White && cleric.level < 6 && currentDice.number == 3)
                        {
                            LevelUpButton.IsEnabled = true;
                        }
                        if (plut.outlook == Outlook.White && plut.level < 6 && currentDice.number == 4)
                        {
                            LevelUpButton.IsEnabled = true;
                        }
                        if (warrior.outlook == Outlook.Black && warrior.level < 6 && currentDice.number == 5)
                        {
                            LevelUpButton.IsEnabled = true;
                        }
                        if (wizard.outlook == Outlook.Black && wizard.level < 6 && currentDice.number == 6)
                        {
                            LevelUpButton.IsEnabled = true;
                        }
                        if (cleric.outlook == Outlook.Black && cleric.level < 6 && currentDice.number == 7)
                        {
                            LevelUpButton.IsEnabled = true;
                        }
                        if (plut.outlook == Outlook.Black && plut.level < 6 && currentDice.number == 8)
                        {
                            LevelUpButton.IsEnabled = true;
                        }
                    }

                }

            }
        }
    }
}
