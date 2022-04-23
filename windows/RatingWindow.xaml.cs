using DungeonPapperWPF.code;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DungeonPapperWPF.windows
{
    /// <summary>
    /// Логика взаимодействия для RatingWindow.xaml
    /// </summary>
    public partial class RatingWindow : Window
    {
        public static SolidColorBrush brushGreen = new SolidColorBrush(Colors.Green);
        private static HttpClient client = new HttpClient();
        public static List<Rating> ratings = new List<Rating>();

        public RatingWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getAllRating();
        }

        public class Rating
        {
            public string nick { get; set; }
            public int quest { get; set; }
            public int xp { get; set; }
        }


        private async Task getAllRating()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var ratingsJson =
                client.GetStreamAsync("https://dnd5-webapi.herokuapp.com/rating");

            ratings = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Rating>>(await ratingsJson);

            ratings.Sort((emp1, emp2) => emp2.xp.CompareTo(emp1.xp));
            var nick = ConfUtil.props["nick"];

            for (var i = 0; i < ratings.Count; i++)
            {
                var userControl = new RatingUserControl();
                userControl.number.Content = i + 1;
                userControl.nick.Content = ratings[i].nick;
                userControl.quest.Content = ratings[i].quest;
                userControl.xp.Content = ratings[i].xp;
                if (nick.Equals(ratings[i].nick)) userControl.Background = brushGreen;
                listBox.Items.Add(userControl);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBox.Items.Clear();
            var nick = ConfUtil.props["nick"];

            for (var i = 0; i < ratings.Count; i++)
                if (questCbox.SelectedIndex == 0 || ratings[i].quest == questCbox.SelectedIndex)
                {
                    var userControl = new RatingUserControl();
                    userControl.number.Content = i + 1;
                    userControl.nick.Content = ratings[i].nick;
                    userControl.quest.Content = ratings[i].quest;
                    userControl.xp.Content = ratings[i].xp;

                    if (nick.Equals(ratings[i].nick)) userControl.Background = brushGreen;
                    listBox.Items.Add(userControl);
                }
        }
    }
}