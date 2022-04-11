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
using System.Windows.Shapes;
using static DungeonPapperWPF.code.Quests;

namespace DungeonPapperWPF
{
    /// <summary>
    /// Логика взаимодействия для WindowsStart.xaml
    /// </summary>
    public partial class WindowsStart : Window
    {
        double zoomScale = 1.0;

        public Random random = new Random(DateTime.Now.Millisecond);
        public Quest quest = null;
        public WindowsStart()
        {
            InitializeComponent();
        }

        private void cbox_quest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbox_quest.SelectedIndex + 1 == 1)
            {
                quest = quest1;
            }

            if (quest != null)
            {
                tbox_legenda.Text = quest.text;
                ImageBrush imageBrush1 = new ImageBrush();
                imageBrush1.ImageSource =
                    new BitmapImage(new Uri(@"Resources\monstr_" + quest.bosses[0].number + ".jpg", UriKind.Relative));
                monster_1.Fill = imageBrush1;

                ImageBrush imageBrush2 = new ImageBrush();
                imageBrush2.ImageSource =
                    new BitmapImage(new Uri(@"Resources\monstr_" + quest.bosses[1].number + ".jpg", UriKind.Relative));
                monster_2.Fill = imageBrush2;

                ImageBrush imageBrush3 = new ImageBrush();
                imageBrush3.ImageSource =
                    new BitmapImage(new Uri(@"Resources\monstr_" + quest.bosses[2].number + ".jpg", UriKind.Relative));
                monster_3.Fill = imageBrush3;

            }

            slectedQuestButton.IsEnabled = true;

        }

        public static Quest quest1 = new Quest(1, "Встреча с разбойниками с Большой дороги всегда сулила беду. \n\n" +
            "Но ныче бандитов развелось так много, что королевской гвардии уже не под силу справиться со всеми негодяями, которые прячутся среди руин.\n\n" +
            "Слава, ждёт тех искателей приключений, кто отважится разобраться с разбойниками. Но банды, опаснее той, что устроило свое логово в древней крпости Дартима, в королевстве еще не видали.\n\n" +
            "В качестве грубой силы бандиты используют МИНОТАВРА, пленительная ВАМПИРША строит козни, а заправляет всем древний СТРАЖ.\n\n" +
            "Пусть эта банда и кажется странной, но с ней даже самым смелым героям королевства будет не до шуток.",
            new List<Boss>() { MINOTAVR, VAMP, STRAG });

        private void monster_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            zoom.Opacity = 1;
            zoom.Fill = ((Rectangle)sender).Fill;
            Canvas.SetZIndex(zoom, 1);

        }

        private void zoom_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            zoom.Opacity = 1;
            zoom.Fill = null;
            Canvas.SetZIndex(zoom, 100);
        }

        private void slectedQuestButton_Click(object sender, RoutedEventArgs e)
        {
            List<int> indexes = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,14,15,16 };
            int index = randomInt(indexes);
            int m1 = indexes[index];
            indexes.RemoveAt(index);

            index = randomInt(indexes);
            int m2 = indexes[index];
            indexes.RemoveAt(index);

            index = randomInt(indexes);
            int m3 = indexes[index];
            indexes.RemoveAt(index);

            indexes = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            index = randomInt(indexes);
            int um1 = indexes[index];
            indexes.RemoveAt(index);

            index = randomInt(indexes);
            int um2 = indexes[index];
            indexes.RemoveAt(index);


            indexes = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            index = randomInt(indexes);
            index = 5;
            int ab1 = indexes[index];
            indexes.RemoveAt(index);

            index = randomInt(indexes);
            int ab2 = indexes[index];
            indexes.RemoveAt(index);


            ImageBrush imageBrush1 = new ImageBrush();
            imageBrush1.ImageSource =
                new BitmapImage(new Uri(@"Resources\mission_" + m1 + ".jpg", UriKind.Relative));
            mission_1.Fill = imageBrush1;
            mission_1.Tag = m1.ToString();

            ImageBrush imageBrush2 = new ImageBrush();
            imageBrush2.ImageSource =
                new BitmapImage(new Uri(@"Resources\mission_" + m2 + ".jpg", UriKind.Relative));
            mission_2.Fill = imageBrush2;
            mission_2.Tag = m2.ToString();

            ImageBrush imageBrush3 = new ImageBrush();
            imageBrush3.ImageSource =
                new BitmapImage(new Uri(@"Resources\mission_" + m3 + ".jpg", UriKind.Relative));
            mission_3.Fill = imageBrush3;
            mission_3.Tag = m3.ToString();


            ImageBrush imageBrush4 = new ImageBrush();
            imageBrush4.ImageSource =
                new BitmapImage(new Uri(@"Resources\user_mission_" + um1 + ".png", UriKind.Relative));
            user_mission_1.Fill = imageBrush4;
            user_mission_1.Tag = um1.ToString();

            ImageBrush imageBrush5 = new ImageBrush();
            imageBrush5.ImageSource =
                new BitmapImage(new Uri(@"Resources\user_mission_" + um2 + ".png", UriKind.Relative));
            user_mission_2.Fill = imageBrush5;
            user_mission_2.Tag = um2.ToString();


            ImageBrush imageBrush6 = new ImageBrush();
            imageBrush6.ImageSource =
                new BitmapImage(new Uri(@"Resources\ability_" + ab1 + ".png", UriKind.Relative));
            abbility_1.Fill = imageBrush6;
            abbility_1.Tag = ab1.ToString();

            ImageBrush imageBrush7 = new ImageBrush();
            imageBrush7.ImageSource =
                new BitmapImage(new Uri(@"Resources\ability_" + ab2 + ".png", UriKind.Relative));
            abbility_2.Fill = imageBrush7;
            abbility_2.Tag = ab2.ToString();

            quest.missions = new List<int> {m1,m2,m3 };
            selectUserMission.IsEnabled = true;
            slectedQuestButton.IsEnabled = false;
        }

        public int randomInt(List<int> indexes)
        {
            return random.Next(indexes.Count());
        }

        private void selectUserMission_Click(object sender, RoutedEventArgs e)
        {
            if(quest.selectMission != 0)
            {
                user_mission_1.IsEnabled = false;
                user_mission_2.IsEnabled = false;
                selectUserMission.IsEnabled = false;
                selectAbblity.IsEnabled = true;
            }
            
        }

        private void selectAbblity_Click(object sender, RoutedEventArgs e)
        {
            if (quest.selectAbility != 0)
            {
                abbility_1.IsEnabled = false;
                abbility_1.IsEnabled = false;
                selectAbblity.IsEnabled = false;
                startButton.IsEnabled = true;
            }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.quest = quest;
            this.Close();
        }

        private void user_mission_1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            user_mission_1.Stroke = Brushes.Red;
            user_mission_1.StrokeThickness = 5;
            user_mission_2.Stroke = null;
            quest.selectMission = int.Parse(user_mission_1.Tag as string);
        }

        private void user_mission_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            user_mission_2.Stroke = Brushes.Red;
            user_mission_2.StrokeThickness = 5;

            user_mission_1.Stroke = null;
            quest.selectMission = int.Parse(user_mission_2.Tag as string);
        }

        private void abbility_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            abbility_1.Stroke = Brushes.Red;
            abbility_1.StrokeThickness = 5;

            abbility_2.Stroke = null;
            quest.selectAbility = int.Parse(abbility_1.Tag as string);
        }

        private void abbility_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            abbility_2.Stroke = Brushes.Red;
            abbility_2.StrokeThickness = 5;

            abbility_1.Stroke = null;
            quest.selectAbility = int.Parse(abbility_2.Tag as string);
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (zoomScale <= 1.9)
                {
                    zoomScale += 0.1;
                }

            }
            else
            {
                if (zoomScale >= 1.1)
                {
                    zoomScale -= 0.1;
                }


            }

            ScaleTransform.ScaleX = zoomScale;
            ScaleTransform.ScaleY = zoomScale;
        }
    }
}
