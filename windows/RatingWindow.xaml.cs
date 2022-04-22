using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DungeonPapperWPF.code;
using Newtonsoft.Json;

namespace DungeonPapperWPF.windows
{
    /// <summary>
    /// Логика взаимодействия для RatingWindow.xaml
    /// </summary>
    public partial class RatingWindow : Window
    {
        static HttpClient client = new HttpClient();
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

           for(int i = 0; i < ratings.Count; i++)
            {
                RatingUserControl userControl = new RatingUserControl();
                userControl.number.Content = i+1;
                userControl.nick.Content = ratings[i].nick;
                userControl.quest.Content = ratings[i].quest;
                userControl.xp.Content = ratings[i].xp;
                listBox.Items.Add(userControl);
            }

        }
    }
}
