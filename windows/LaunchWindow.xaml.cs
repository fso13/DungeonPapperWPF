using DungeonPapperWPF.code;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace DungeonPapperWPF.windows
{
    /// <summary>
    /// Логика взаимодействия для LaunchWindow.xaml
    /// </summary>
    public partial class LaunchWindow : Window
    {
        private static HttpClient client = new HttpClient();

        public LaunchWindow()
        {
            var splashScreen = new SplashScreen("Resources/logo.png");
            splashScreen.Show(false);
            splashScreen.Close(TimeSpan.FromSeconds(5));
            System.Threading.Thread.Sleep(5000);
            InitializeComponent();

            var currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            currentVersion = currentVersion.Substring(0, currentVersion.Length - 2);

            Title += " v" + currentVersion;
            ConfUtil.read();

        }

        private void NewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new WindowsStart(this);
            window.Owner = this;
            window.Show();

            Hide();
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
            progressBar.Visibility = Visibility.Visible;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask =
                client.GetStreamAsync("https://api.github.com/repos/fso13/DungeonPapperWPF/releases/latest");

            var msg = await System.Text.Json.JsonSerializer.DeserializeAsync<Release>(await streamTask);

            var currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            currentVersion = currentVersion.Substring(0, currentVersion.Length - 2);
            var lastVersion = msg.name.Replace("v", "");
            lastVersion = lastVersion.Substring(0, lastVersion.LastIndexOf('.'));

            if (String.Compare(lastVersion, currentVersion, StringComparison.Ordinal) > 0)
            {
                var messageBoxText = "Хотите скачать новую версию?";
                var caption = "Доступна новая версия";
                var button = MessageBoxButton.YesNoCancel;
                var icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(
                            "https://github.com/fso13/DungeonPapperWPF/releases/latest/download/DungeonPapperWPF.zip",
                            Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) +
                            "//DungeonPapperWPF.zip");
                        MessageBox.Show("Обновление скачанно в текущую папку.");
                    }
            }
            else
            {
                MessageBox.Show("У вас последняя версия");
            }

            progressBar.Visibility = Visibility.Hidden;
        }

        public class Release
        {
            public string name { get; set; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("user.conf"))
            {
                if (!ConfUtil.props.ContainsKey("nick")) new NickWindow().Show();
            }
            else
            {
                new NickWindow().Show();
            }
        }
    }
}