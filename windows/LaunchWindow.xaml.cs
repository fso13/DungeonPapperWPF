using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using DungeonPapperWPF.code;

namespace DungeonPapperWPF.windows
{
    /// <summary>
    /// Логика взаимодействия для LaunchWindow.xaml
    /// </summary>
    public partial class LaunchWindow : Window
    {
        static HttpClient client = new HttpClient();

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
           new RatingWindow().Show();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScaleTransform.ScaleX = e.NewSize.Height / 378;
            ScaleTransform.ScaleY = e.NewSize.Height / 378;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            getLastVersion();
        }

        private async Task getLastVersion()
        {
            this.progressBar.Visibility = Visibility.Visible;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask =
                client.GetStreamAsync("https://api.github.com/repos/fso13/DungeonPapperWPF/releases/latest");

            var msg = await System.Text.Json.JsonSerializer.DeserializeAsync<Release>(await streamTask);

            string currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            currentVersion = currentVersion.Substring(0, currentVersion.Length - 3);
            string lastVersion = msg.name.Replace("v", "");

            if (lastVersion.CompareTo(currentVersion) > 0)
            {
                string messageBoxText = "Хотите скачать новую версию?";
                string caption = "Доступна новая версия";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(
                            "https://github.com/fso13/DungeonPapperWPF/releases/latest/download/DungeonPapperWPF.zip",
                            System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) +
                            "//DungeonPapperWPF.zip");
                        MessageBox.Show("Обновление скачанно в текущую папку.");
                    }
                }
            }
            else
            {
                MessageBox.Show("У вас последняя версия");
            }

            this.progressBar.Visibility = Visibility.Hidden;
        }

        public class Release
        {
            public string name { get; set; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            if (File.Exists("user.conf"))
            {
                props = ConfUtil.read();

                if (!props.ContainsKey("nick"))
                {
                    new NickWindow().Show();
                }
            }
            else
            {
                new NickWindow().Show();
            }
        }
    }
}
