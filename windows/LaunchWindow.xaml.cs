using System;
using System.Windows;
using System.Windows.Media;

namespace DungeonPapperWPF.windows
{
    /// <summary>
    /// Логика взаимодействия для LaunchWindow.xaml
    /// </summary>
    public partial class LaunchWindow : Window
    {
        public LaunchWindow()
        {

            SplashScreen splashScreen = new SplashScreen("Resources/logo.png");
            splashScreen.Show(false);
            splashScreen.Close(TimeSpan.FromSeconds(5));
            System.Threading.Thread.Sleep(5000);
            InitializeComponent();
        }

        private void NewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new WindowsStart(this);
            window.Owner = this;
            window.Show();

            this.Hide();
        }

        private void SettingsGameBtn_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().Show();
        }

        private void RatingGameBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Еще не реализованно");
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScaleTransform.ScaleX = e.NewSize.Height / 328;
            ScaleTransform.ScaleY = e.NewSize.Height / 328;
        }
    }
}
