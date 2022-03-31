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
    /// Логика взаимодействия для Field.xaml
    /// </summary>
    public partial class Field : UserControl
    {
        public static ImageBrush heroBrush = new ImageBrush();
        public static SolidColorBrush brushGreen = new SolidColorBrush(Colors.Green);
        public static SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);
        public static SolidColorBrush brushBlue = new SolidColorBrush(Colors.Blue);
        public static SolidColorBrush currentBrush = new SolidColorBrush(Colors.DarkBlue);

        public static ImageBrush opacityMaskCircleBrush = new ImageBrush();


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
        }
        public void greanColor()
        {
            if (!isPath)
            {
                brushGreen.Opacity = 0.40;
                this.grid.Background = brushGreen;
            }
        }

        public void redColor()
        {
            brushRed.Opacity = 0.40;
            this.grid.Background = brushRed;
        }

        public void clearColor()
        {
            if (!isPath)
            {
                this.grid.Background = null;
            }
            else if (!isCurrent)
            {
                blueColor();
                this.heroes.Fill = null;
            }


        }

        public void blueColor()
        {
            brushBlue.Opacity = 0.40;
            this.grid.Background = brushBlue;
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.grid.Opacity > 0 && (this.grid.Background == brushGreen || isPath))
            {
                if (MainWindow.currentCountStep > 0)
                {
                    blueColor();
                    if (dto.trap)
                    {
                        ((MainWindow)Window.GetWindow(this)).damage(1);

                    }
                    if (dto.prey != null)
                    {
                        switch (dto.prey)
                        {
                            case Potion potion:;
                                ((MainWindow)Window.GetWindow(this)).addPotion(potion.count); break;
                            case Diamond diamond: MessageBox.Show("Украден алмаз " + diamond.name); break;
                            case MagicThing magic: MessageBox.Show("Получена половина магического предмета"); break;
                            case LevelUp level:
                                ((MainWindow)Window.GetWindow(this)).levelUpFromMove();
                                break;
                            default: break;
                        }
                        dto.prey = null;
                        preyFieldAction.Fill = opacityMaskCircleBrush;
                        preyField.Opacity = 1;
                    }

                    this.isPath = true;
                    isCurrent = true;
                    currentBrush.Opacity = 0.40;
                    this.grid.Background = currentBrush;
                    this.heroes.Fill = heroBrush;

                    if (((MainWindow)Window.GetWindow(this)).party.path.Count > 0)
                    {
                        ((MainWindow)Window.GetWindow(this)).party.path.Last().isCurrent = false;
                    }
                     ((MainWindow)Window.GetWindow(this)).party.path.Add(this);
                    MainWindow.currentCountStep--;

                    ((MainWindow)Window.GetWindow(this)).highlightWhereToGo();
                }

            }
        }
    }
}
