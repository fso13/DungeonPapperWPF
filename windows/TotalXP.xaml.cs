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

namespace DungeonPapperWPF.windows
{
    
    public partial class TotalXP : Window
    {
        public TotalXP()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int xp_boss1 = MainWindow.party.xpBoss1;
            int xp_boss2 = MainWindow.party.xpBoss2;
            int xp_boss3 = MainWindow.party.xpBoss3;

            int xp_level = 0;
            int minLevel = 6;
            MainWindow.party.GetHeroes().ForEach(hero =>
            {
                if (minLevel > hero.level)
                {
                    minLevel = hero.level;
                }
;               if(hero.level == 6)
                {
                    xp_level++;
                }
            });

            if (minLevel == 2)
            {
                xp_level += 3;
            }
            if (minLevel == 3)
            {
                xp_level += 6;
            }
            if (minLevel == 4)
            {
                xp_level += 8;
            }
            if (minLevel == 5)
            {
                xp_level += 11;
            }
            if (minLevel == 6)
            {
                xp_level += 14;
            }

            int xp_magic = 0;
            MainWindow.party.magics.ForEach(mag =>
            {
                xp_magic += mag.countPart;

                if (mag.countPart == 2)
                {
                    switch (mag.number)
                    {
                        case 2:
                            xp_magic += -1; break;
                        case 3:
                            xp_magic += 1; break;
                        case 5: xp_magic += 4; break;
                        case 7: xp_magic += 2; break;
                        case 8: xp_magic += 3; break;
                        default: break;
                    }
                }
            });

            int xp_diamond = 0;

            switch (MainWindow.party.diamonds.Count())
            {
                case 0: break;
                case 1: xp_diamond = 3; break;
                case 2: xp_diamond = 6; break;
                case 3: xp_diamond = 10; break;
                case 4: xp_diamond = 15; break;
                case 5: xp_diamond = 20; break;
                case 6: xp_diamond = 25; break;
                case 7: xp_diamond = 30; break;
                case 8: xp_diamond = 36; break;
                case 9: xp_diamond = 42; break;
                case 10: xp_diamond = 48; break;
                default: break;
            }
            int xp_monsters = 0;

            switch (MainWindow.party.diamonds.Count())
            {
                case 0: break;
                case 1: xp_monsters = 1; break;
                case 2: xp_monsters = 3; break;
                case 3: xp_monsters = 4; break;
                case 4: xp_monsters = 6; break;
                case 5: xp_monsters = 8; break;
                case 6: xp_monsters = 10; break;
                case 7: xp_monsters = 12; break;
                case 8: xp_monsters = 14; break;
                case 9: xp_monsters = 16; break;
                case 10: xp_monsters = 19; break;
                case 11: xp_monsters = 22; break;
                case 12: xp_monsters = 25; break;
                default: break;
            }
            int xp_blood = 0;
            if (MainWindow.party.blood >= 3 && MainWindow.party.blood < 6)
            {
                xp_blood = -1;
            }
            if (MainWindow.party.blood >= 6 && MainWindow.party.blood < 9)
            {
                xp_blood = -3;
            }
            if (MainWindow.party.blood >= 9 && MainWindow.party.blood < 12)
            {
                xp_blood = -6;
            }
            if (MainWindow.party.blood >= 12 && MainWindow.party.blood < 15)
            {
                xp_blood = -9;
            }
            if (MainWindow.party.blood >= 15 && MainWindow.party.blood < 18)
            {
                xp_blood = -13;
            }
            if (MainWindow.party.blood >= 18 && MainWindow.party.blood < 21)
            {
                xp_blood = -17;
            }
            if (MainWindow.party.blood >= 21 && MainWindow.party.blood < 25)
            {
                xp_blood = -21;
            }
            if (MainWindow.party.blood ==24)
            {
                xp_blood = -25;
            }

            int xp_dead = MainWindow.party.isDeadPaty ? -9 : 0;

            int xp_user_mission = 0;
            int xp_mission = 0;

            label_1.Content = xp_boss1;
            label_2.Content = xp_boss2;
            label_3.Content = xp_boss3;
            label_4.Content = xp_level;
            label_5.Content = xp_magic;
            label_6.Content = xp_diamond;
            label_7.Content = xp_monsters;
            label_8.Content = xp_blood;
            label_9.Content = xp_dead;
            label_10.Content = xp_user_mission;
            label_11.Content = xp_mission;

            total_xp.Content = xp_boss1+ xp_boss2 + xp_boss3 + xp_level+ xp_magic + xp_diamond+ xp_monsters+ xp_blood+ xp_dead+ xp_user_mission+ xp_mission;

        }
    }
}
