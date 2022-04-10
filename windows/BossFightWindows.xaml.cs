using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DungeonPapperWPF
{
    /// <summary>
    /// Логика взаимодействия для BossFightWindows.xaml
    /// </summary>
    public partial class BossFightWindows : Window
    {

        public static Boss boss = null;
        public BossFightWindows()
        {
            InitializeComponent();

            ImageBrush imageBrushm1 = new ImageBrush();
            imageBrushm1.ImageSource =
                new BitmapImage(new Uri(@"Resources\monstr_" + boss.number + ".jpg", UriKind.Relative));
            boss_rec.Fill = imageBrushm1;

            label_level_party.Content = MainWindow.party.getDamageByBoss(boss);
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            boss.isDead = true;
            MainWindow.party.bosses.Add(boss);

            this.Close();

        }

        private void btn_fail_Click(object sender, RoutedEventArgs e)
        {
            boss.isDead = false;
            MainWindow.party.bosses.Add(boss);
            this.Close();
        }

        private void btn_fign_Click(object sender, RoutedEventArgs e)
        {
            int minLifeBoss = boss.firstDamage.damage;
            if (boss.level == 1)
            {
                if (MainWindow.party.path.Find(f => f.dto.one != null) != null)
                {
                    if (minLifeBoss > MainWindow.party.getDamageByBoss(boss))
                    {
                        MessageBox.Show("Вы не можете нанести урон монстру, бегите...");

                        btn_fail.IsEnabled = true;
                        btn_fail.Visibility = Visibility.Visible;
                    }
                    MessageBox.Show("Вы сразили монстра, заберите награду");

                    btn_ok.IsEnabled = true;
                    btn_ok.Visibility = Visibility.Visible;

                }
                else
                {
                    MessageBox.Show("Вы не добрались до логова монстра, репутация пошатнулась");

                    btn_fail.IsEnabled = true;
                    btn_fail.Visibility = Visibility.Visible;
                }

            }

            if (boss.level == 2)
            {
                if (MainWindow.party.path.Find(f => f.dto.two != null) != null)
                {

                    if (minLifeBoss > MainWindow.party.getDamageByBoss(boss))
                    {
                        MessageBox.Show("Вы не можете нанести урон монстру, бегите...");

                        btn_fail.IsEnabled = true;
                        btn_fail.Visibility = Visibility.Visible;
                    }
                    MessageBox.Show("Вы сразили монстра, заберите награду");

                    btn_ok.IsEnabled = true;
                    btn_ok.Visibility = Visibility.Visible;

                }
                else
                {
                    MessageBox.Show("Вы не добрались до логова монстра, репутация пошатнулась");

                    btn_fail.IsEnabled = true;
                    btn_fail.Visibility = Visibility.Visible;
                }

            }

            if (boss.level == 3)
            {
                if (MainWindow.party.path.Find(f => f.dto.three != null) != null)
                {
                    if (minLifeBoss > MainWindow.party.getDamageByBoss(boss))
                    {
                        MessageBox.Show("Вы не можете нанести урон монстру, бегите...");

                        btn_fail.IsEnabled = true;
                        btn_fail.Visibility = Visibility.Visible;
                    }
                    MessageBox.Show("Вы сразили монстра, заберите награду");

                    btn_ok.IsEnabled = true;
                    btn_ok.Visibility = Visibility.Visible;

                }
                else
                {
                    MessageBox.Show("Вы не добрались до логова монстра, репутация пошатнулась");

                    btn_fail.IsEnabled = true;
                    btn_fail.Visibility = Visibility.Visible;
                }

            }
        }
    }
}
