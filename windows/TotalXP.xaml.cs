using DungeonPapperWPF.code;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;

namespace DungeonPapperWPF.windows
{
    public partial class TotalXP : Window
    {
        private static HttpClient client = new HttpClient();

        public Window parent;

        public TotalXP(Window parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        public int xpUserMission()
        {
            switch (MainWindow.quest.selectMission)
            {
                case 1:
                    switch (MainWindow.party.deadMonsters.FindAll(m => m.heroClass.level == 3).Count())
                    {
                        case 2: return 2;
                        case 3: return 4;
                        case 4: return 6;
                        default: return 0;
                    }

                case 2:
                    var countUserMission2 = MainWindow.party.countUserMission2;
                    if (countUserMission2 >= 4 && countUserMission2 < 7)
                        return 2;
                    else if (countUserMission2 >= 7 && countUserMission2 < 10)
                        return 4;
                    else if (countUserMission2 >= 10) return 6;
                    return 0;

                case 3:
                    switch (MainWindow.party.deadMonsters.FindAll(m => m.heroClass.level == 5).Count())
                    {
                        case 2: return 2;
                        case 3: return 4;
                        case 4: return 6;
                        default: return 0;
                    }

                case 4:
                    switch (MainWindow.party.hp)
                    {
                        case 12: return 2;
                        case 13: return 2;
                        case 14: return 2;
                        case 15: return 2;
                        case 16: return 4;
                        case 17: return 4;
                        case 18: return 4;
                        case 19: return 4;
                        case 20: return 6;
                        case 21: return 6;
                        case 22: return 6;
                        case 23: return 6;
                        case 24: return 6;
                        default: return 0;
                    }

                case 5:
                {
                    var room = 0;

                    MainWindow.party.path.ForEach(row =>
                    {
                        if (row.dto.x == 0 || row.dto.x == 5 || row.dto.y == 0 || row.dto.y == 6) room++;
                    });

                    if (room >= 10 && room < 13)
                        return 2;
                    else if (room >= 13 && room < 16)
                        return 4;
                    else if (room >= 16) return 6;
                    return 0;
                }
                case 6:
                    switch (MainWindow.party.deadMonsters.Count())
                    {
                        case 4: return 2;
                        case 5: return 2;
                        case 6: return 2;
                        case 7: return 4;
                        case 8: return 4;
                        case 9: return 4;
                        case 10: return 4;
                        case 11: return 6;
                        case 12: return 6;
                        default: return 0;
                    }

                case 7:
                {
                    var xp = MainWindow.party.xpBoss1 + MainWindow.party.xpBoss2 + MainWindow.party.xpBoss3;
                    if (xp >= 10 && xp < 15)
                        return 2;
                    else if (xp >= 15 && xp < 20)
                        return 4;
                    else if (xp >= 20) return 6;
                    return 0;
                }
                case 8:
                {
                    var columns = new int[6] {0, 0, 0, 0, 0, 0};

                    MainWindow.party.path.ForEach(row => { columns[row.dto.x] += 1; });

                    var rr = columns.OfType<int>().ToList().FindAll(x => x >= 4).Count();
                    if (rr == 4)
                        return 2;
                    else if (rr == 5)
                        return 4;
                    else if (rr == 6) return 6;
                    return 0;
                }
                case 9:
                    switch (MainWindow.party.deadMonsters.FindAll(m => m.heroClass.level == 4).Count())
                    {
                        case 2: return 2;
                        case 3: return 4;
                        case 4: return 6;
                        default: return 0;
                    }

                case 10:
                {
                    var potion = MainWindow.party.potions.Count();
                    if (potion >= 6 && potion < 9)
                        return 2;
                    else if (potion >= 9 && potion < 12)
                        return 4;
                    else if (potion >= 12) return 6;
                    return 0;
                }
                case 11:
                {
                    var room = 0;

                    MainWindow.party.path.ForEach(row =>
                    {
                        if (row.dto.x == 0 && row.dto.y == 0 ||
                            row.dto.x == 0 && row.dto.y == 6 ||
                            row.dto.x == 5 && row.dto.y == 0 ||
                            row.dto.x == 5 && row.dto.y == 6)
                            room++;
                    });

                    if (room == 1)
                        return 2;
                    else if (room == 2)
                        return 4;
                    else if (room >= 3) return 6;
                    return 0;
                }

                case 12:
                {
                    //todo потом заменить на норм карточку, потому что должна быть пол части волшебных предметов
                    var columns = new int[6] {0, 0, 0, 0, 0, 0};

                    MainWindow.party.path.ForEach(row => { columns[row.dto.x] += 1; });

                    var rr = columns.OfType<int>().ToList().FindAll(x => x >= 4).Count();
                    if (rr == 4)
                        return 2;
                    else if (rr == 5)
                        return 4;
                    else if (rr == 6) return 6;
                    return 0;
                }
                case 13:
                    var diamond = MainWindow.party.diamonds.Count();
                    if (diamond >= 2 && diamond < 5)
                        return 2;
                    else if (diamond >= 5 && diamond < 7)
                        return 4;
                    else if (diamond >= 7) return 6;
                    return 0;


                case 14:
                    var heroes = MainWindow.party.GetHeroes().FindAll(h => h.level >= 5).Count();
                    if (heroes >= 2 && heroes < 3)
                        return 2;
                    else if (heroes >= 3 && heroes < 4)
                        return 4;
                    else if (heroes >= 4) return 6;
                    return 0;

                case 15:
                    var path = MainWindow.party.path.Count();
                    if (path >= 20 && path < 25)
                        return 2;
                    else if (path >= 25 && path < 30)
                        return 4;
                    else if (path >= 30) return 6;
                    return 0;

                case 16:

                    var rows = new int[7] {0, 0, 0, 0, 0, 0, 0};

                    MainWindow.party.path.ForEach(row => { rows[row.dto.y] += 1; });

                    var r = rows.OfType<int>().ToList().FindAll(x => x == 6).Count();
                    if (r == 1)
                        return 2;
                    else if (r == 2)
                        return 4;
                    else if (r >= 3) return 6;
                    return 0;

                default: return 0;
            }
        }

        public int xpMissions()
        {
            var one = xpMission(MainWindow.quest.roundMissionComplete1);
            var two = xpMission(MainWindow.quest.roundMissionComplete2);
            var three = xpMission(MainWindow.quest.roundMissionComplete3);
            return one + two + three;
        }

        public int xpAbbility()
        {
            switch (MainWindow.quest.selectAbility)
            {
                case 1: return -3;
                case 2: return 2;
                case 3: return 2;
                case 4: return -2;
                case 5: return 2;
                case 6: return -3;
                case 7: return -1;
                case 8: return -1;
                case 9: return 0;
                case 10: return 0;
                case 11: return -1;
                case 12: return 3;
                case 13: return 0;
                case 14: return -1;
                case 15: return -2;
                case 16: return -2;
                default: return 0;
            }
        }

        public static int xpMission(int round)
        {
            switch (round)
            {
                case 1: return 6;
                case 2: return 5;
                case 3: return 4;
                case 4: return 4;
                case 5: return 3;
                case 6: return 3;
                case 7: return 2;
                case 8: return 1;
                default: return 0;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var xp_boss1 = MainWindow.party.xpBoss1;
            var xp_boss2 = MainWindow.party.xpBoss2;
            var xp_boss3 = MainWindow.party.xpBoss3;

            var xp_level = 0;
            var minLevel = 6;
            MainWindow.party.GetHeroes().ForEach(hero =>
            {
                if (minLevel > hero.level) minLevel = hero.level;
                ;
                if (hero.level == 6) xp_level++;
            });

            if (minLevel == 2) xp_level += 3;
            if (minLevel == 3) xp_level += 6;
            if (minLevel == 4) xp_level += 8;
            if (minLevel == 5) xp_level += 11;
            if (minLevel == 6) xp_level += 14;

            var xp_magic = 0;
            MainWindow.party.magics.ForEach(mag =>
            {
                xp_magic += mag.countPart;

                if (mag.countPart == 2)
                    switch (mag.number)
                    {
                        case 2:
                            xp_magic += -1;
                            break;
                        case 3:
                            xp_magic += 1;
                            break;
                        case 5:
                            xp_magic += 4;
                            break;
                        case 7:
                            xp_magic += 2;
                            break;
                        case 8:
                            xp_magic += 3;
                            break;
                        default: break;
                    }
            });

            var xp_diamond = 0;

            switch (MainWindow.party.diamonds.Count())
            {
                case 0: break;
                case 1:
                    xp_diamond = 3;
                    break;
                case 2:
                    xp_diamond = 6;
                    break;
                case 3:
                    xp_diamond = 10;
                    break;
                case 4:
                    xp_diamond = 15;
                    break;
                case 5:
                    xp_diamond = 20;
                    break;
                case 6:
                    xp_diamond = 25;
                    break;
                case 7:
                    xp_diamond = 30;
                    break;
                case 8:
                    xp_diamond = 36;
                    break;
                case 9:
                    xp_diamond = 42;
                    break;
                case 10:
                    xp_diamond = 48;
                    break;
                default: break;
            }

            var xp_monsters = 0;

            switch (MainWindow.party.deadMonsters.Count())
            {
                case 0: break;
                case 1:
                    xp_monsters = 1;
                    break;
                case 2:
                    xp_monsters = 3;
                    break;
                case 3:
                    xp_monsters = 4;
                    break;
                case 4:
                    xp_monsters = 6;
                    break;
                case 5:
                    xp_monsters = 8;
                    break;
                case 6:
                    xp_monsters = 10;
                    break;
                case 7:
                    xp_monsters = 12;
                    break;
                case 8:
                    xp_monsters = 14;
                    break;
                case 9:
                    xp_monsters = 16;
                    break;
                case 10:
                    xp_monsters = 19;
                    break;
                case 11:
                    xp_monsters = 22;
                    break;
                case 12:
                    xp_monsters = 25;
                    break;
                default: break;
            }

            var xp_blood = 0;
            if (MainWindow.party.blood >= 3 && MainWindow.party.blood < 6) xp_blood = -1;
            if (MainWindow.party.blood >= 6 && MainWindow.party.blood < 9) xp_blood = -3;
            if (MainWindow.party.blood >= 9 && MainWindow.party.blood < 12) xp_blood = -6;
            if (MainWindow.party.blood >= 12 && MainWindow.party.blood < 15) xp_blood = -9;
            if (MainWindow.party.blood >= 15 && MainWindow.party.blood < 18) xp_blood = -13;
            if (MainWindow.party.blood >= 18 && MainWindow.party.blood < 21) xp_blood = -17;
            if (MainWindow.party.blood >= 21 && MainWindow.party.blood < 25) xp_blood = -21;
            if (MainWindow.party.blood == 24) xp_blood = -25;

            var xp_dead = MainWindow.party.isDeadPaty ? -9 : 0;

            var xp_user_mission = xpUserMission();
            var xp_mission = xpMissions();

            label_1.Content = xp_boss1;
            label_2.Content = xp_boss2;
            label_3.Content = xp_boss3;
            label_4.Content = xp_level;
            label_5.Content = xp_magic;
            label_6.Content = xp_diamond;
            label_7.Content = xp_monsters;
            label_8.Content = xp_blood;
            label_9.Content = xp_dead;
            label_10.Content = xp_user_mission + xpAbbility();
            label_11.Content = xp_mission;

            total_xp.Content = xp_boss1 + xp_boss2 + xp_boss3 + xp_level + xp_magic + xp_diamond + xp_monsters +
                               xp_blood + xp_dead + xp_user_mission + xpAbbility() + xp_mission;


            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            client.PostAsync(
                "https://dnd5-webapi.herokuapp.com/rating?nick=" + ConfUtil.props["nick"] + "&quest=" +
                MainWindow.quest.questNumber + "&&xp=" + total_xp.Content, null);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((MainWindow) parent).parent.Show();
        }
    }
}