using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DungeonPapperWPF
{
    /// <summary>
    /// Логика взаимодействия для Field.xaml
    /// </summary>
    public partial class Field : UserControl
    {
        public static ImageBrush heroBrush = new ImageBrush();
        public static SolidColorBrush brushGreen = new SolidColorBrush(Colors.Green);
        public static SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);
        public static SolidColorBrush brushBlue = new SolidColorBrush(Colors.Blue);
        public static SolidColorBrush brushYellow = new SolidColorBrush(Colors.Yellow);
        public static SolidColorBrush currentBrush = new SolidColorBrush(Colors.DarkBlue);

        public static ImageBrush opacityMaskCircleBrush = new ImageBrush();
        public static ImageBrush opacityMaskKrestBrush = new ImageBrush();


        public FieldDto dto { get; set; }
        public bool isPath { get; set; } = false;
        public bool isCurrent { get; set; } = false;

        public Field()
        {
            InitializeComponent();
        }

        static Field()
        {
            heroBrush.ImageSource =
                new BitmapImage(new Uri(@"Resources\hero_pack.png", UriKind.Relative));
            opacityMaskCircleBrush.ImageSource =
                new BitmapImage(new Uri(@"Resources\circle.png", UriKind.Relative));
            opacityMaskKrestBrush.ImageSource =
                new BitmapImage(new Uri(@"Resources\krest.png", UriKind.Relative));
        }

        public void greanColor()
        {
            if (!isCurrent)
            {
                brushGreen.Opacity = 0.40;
                grid.Background = brushGreen;
            }
        }

        public void redColor()
        {
            brushRed.Opacity = 0.40;
            grid.Background = brushRed;
        }

        public void clearColor()
        {
            if (!isPath)
            {
                grid.Background = null;
            }
            else if (!isCurrent)
            {
                blueColor();
                heroes.Fill = null;
            }
        }

        public void blueColor()
        {
            brushBlue.Opacity = 0.40;
            grid.Background = brushBlue;
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grid.Opacity > 0 && grid.Background == brushGreen && !isCurrent)
            {
                if (MainWindow.currentCountStep > 0)
                {
                    blueColor();

                    if (MainWindow.party.path.Count > 0)
                    {
                        var last = MainWindow.party.path.Last();


                        if (last.dto.x == dto.x)
                        {
                            if (last.dto.y > dto.y)
                            {
                                if (last.dto.topBarrier == Barrier.River)
                                {
                                    MainWindow.party.countUserMission2 += 1;
                                    if (MainWindow.party.isAddMoveFromRiver) MainWindow.currentCountStep++;
                                }
                                else if (last.dto.topBarrier == Barrier.Black || last.dto.topBarrier == Barrier.Wall)
                                {
                                    MainWindow.party.countUserMission2 += 1;
                                }
                            }
                            else
                            {
                                if (last.dto.downBarrier == Barrier.River)
                                {
                                    MainWindow.party.countUserMission2 += 1;
                                    if (MainWindow.party.isAddMoveFromRiver) MainWindow.currentCountStep++;
                                }
                                else if (last.dto.downBarrier == Barrier.Black || last.dto.downBarrier == Barrier.Wall)
                                {
                                    MainWindow.party.countUserMission2 += 1;
                                }
                            }
                        }
                        else
                        {
                            if (last.dto.x > dto.x)
                            {
                                if (last.dto.leftBarrier == Barrier.River)
                                {
                                    MainWindow.party.countUserMission2 += 1;
                                    if (MainWindow.party.isAddMoveFromRiver) MainWindow.currentCountStep++;
                                }
                                else if (last.dto.leftBarrier == Barrier.Black || last.dto.leftBarrier == Barrier.Wall)
                                {
                                    MainWindow.party.countUserMission2 += 1;
                                }
                            }
                            else
                            {
                                if (last.dto.rightBarrier == Barrier.River)
                                {
                                    MainWindow.party.countUserMission2 += 1;
                                    if (MainWindow.party.isAddMoveFromRiver) MainWindow.currentCountStep++;
                                }
                                else if (last.dto.rightBarrier == Barrier.Black ||
                                         last.dto.rightBarrier == Barrier.Wall)
                                {
                                    MainWindow.party.countUserMission2 += 1;
                                }
                            }
                        }
                    }

                    if (dto.trap && !MainWindow.party.isIgnoreTrap)
                    {
                        MainWindow.party.countUserMission2 += 1;
                        ((MainWindow) Window.GetWindow(this)).damage(1);
                    }

                    if (dto.monster != null && !dto.monster.isDead)
                    {
                        ((MainWindow) Window.GetWindow(this)).deadMonster(dto.monster);
                        monsterFieldAction.Fill = opacityMaskKrestBrush;
                        monsterField.Opacity = 1;
                    }

                    if (dto.prey != null)
                    {
                        switch (dto.prey)
                        {
                            case Potion potion:
                                ((MainWindow) Window.GetWindow(this)).addPotion(potion.count);
                                break;
                            case Diamond diamond:
                                ((MainWindow) Window.GetWindow(this)).addDiamond(diamond);
                                MessageBox.Show("Украден алмаз " + diamond.name);
                                break;
                            case MagicThing magic:
                                MainWindow.currentCountMagicPart++;
                                ((MainWindow) Window.GetWindow(this)).highlightCreateMagic(null);
                                MessageBox.Show("Получена половина магического предмета");
                                break;
                            case LevelUp level:
                                ((MainWindow) Window.GetWindow(this)).levelUpFromMove();
                                break;
                            default: break;
                        }

                        dto.prey = null;
                        preyFieldAction.Fill = opacityMaskCircleBrush;
                        preyField.Opacity = 1;
                    }

                    isPath = true;
                    isCurrent = true;
                    currentBrush.Opacity = 0.40;
                    grid.Background = currentBrush;
                    heroes.Fill = heroBrush;

                    if (MainWindow.party.path.Count > 0) MainWindow.party.path.Last().isCurrent = false;
                    MainWindow.party.path.Add(this);
                    MainWindow.currentCountStep--;


                    ((MainWindow) Window.GetWindow(this)).highlightWhereToGo();
                }
            }
            else if (grid.Opacity > 0 && grid.Background == brushRed)
            {
                ((MainWindow) Window.GetWindow(this)).deadMonsterFromArtifact(dto.monster);
                monsterFieldAction.Fill = opacityMaskKrestBrush;
                monsterField.Opacity = 1;
            }

            else if (grid.Opacity > 0 && grid.Background == brushYellow)
            {
                ((MainWindow) Window.GetWindow(this)).addDiamond(dto.prey as Diamond);
                dto.prey = null;
                preyFieldAction.Fill = opacityMaskCircleBrush;
                preyField.Opacity = 1;

                for (var i = 0; i < 6; i++)
                for (var j = 0; j < 7; j++)
                    MainWindow.fields[j, i].clearColor();
            }
        }

        private void monsterFieldAction_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(dto.monster.monsterType.ToString());
        }

        internal void yellowColor()
        {
            brushYellow.Opacity = 0.40;
            grid.Background = brushYellow;
        }
    }
}