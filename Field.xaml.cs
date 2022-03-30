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
        SolidColorBrush brushGreen = new SolidColorBrush(Colors.Green);
        SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);
        SolidColorBrush brushBlue = new SolidColorBrush(Colors.Blue);
        SolidColorBrush borderBrush = new SolidColorBrush(Colors.Black);
        Thickness thickness = new Thickness(5);

        public FieldDto dto { get; set; }
        public bool isPath { get; set; } = false;
        public bool isCurrent { get; set; } = false;
        public Field()
        {
            InitializeComponent();
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
            if (!isCurrent)
            {
                this.BorderBrush = null;
                this.BorderThickness = new Thickness(0);
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
                        MainWindow.damage(1);

                    }
                    if (dto.prey != null)
                    {
                        switch (dto.prey)
                        {
                            case Potion potion: MessageBox.Show("Найдено лечебное зелье, в количестве: " + potion.count); break;
                            case Diamond diamond: MessageBox.Show("Украден алмаз " + diamond.name); break;
                            case MagicThing magic: MessageBox.Show("Получена половина магического предмета"); break;
                            case LevelUp level: 
                                ((MainWindow)Window.GetWindow(this)).levelUpFromMove();
                                break;
                            default: break;
                        }

                    }

                    this.isPath = true;
                    isCurrent = true;
                    this.BorderBrush = borderBrush;
                    this.BorderThickness = thickness;

                    if (MainWindow.path.Count > 0)
                    {
                        MainWindow.path.Last().isCurrent = false;
                    }
                    MainWindow.path.Add(this);
                    MainWindow.currentCountStep--;

                    MainWindow.highlightWhereToGo();
                }

            }
        }
    }
}
