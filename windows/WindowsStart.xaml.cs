using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
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
            if (cbox_quest.SelectedIndex + 1 == 2)
            {
                quest = quest2;
            }
            if (cbox_quest.SelectedIndex + 1 == 3)
            {
                quest = quest3;
            }
            if (cbox_quest.SelectedIndex + 1 == 4)
            {
                quest = quest4;
            }
            if (cbox_quest.SelectedIndex + 1 == 5)
            {
                quest = quest5;
            }

            if (cbox_quest.SelectedIndex + 1 == 6)
            {
                quest = quest6;
            }
            if (cbox_quest.SelectedIndex + 1 == 7)
            {
                quest = quest7;
            }
            if (cbox_quest.SelectedIndex + 1 == 8)
            {
                quest = quest8;
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
            "Слава, ждёт тех искателей приключений, кто отважится разобраться с разбойниками. Но банды, опаснее той, что устроило свое логово в древней крпости Дартима," +
            " в королевстве еще не видали.\n\n" +
            "В качестве грубой силы бандиты используют МИНОТАВРА, пленительная ВАМПИРША строит козни, а заправляет всем древний СТРАЖ.\n\n" +
            "Пусть эта банда и кажется странной, но с ней даже самым смелым героям королевства будет не до шуток.",
            new List<Boss>() { MINOTAVR, VAMP, STRAG });

        public static Quest quest2 = new Quest(2, "Королевство в отчаянии: уцелевшим разбойникам из дартимской банды удалось убежать в хрустальный город Банц \n\n" +
    "Днем бандиты прячутся в катакомбах города, а ночью промышляют грабежами и заказными убиствами. Местная воровская гильдия \"Братство змея\" объявила войну пришлым разбойникам" +
            " и посулила щедрую награду тому, кто избавится от них\n\n" +
    "МИНОТАВА видели возле катакомб. Сокровища бандитов теперь стережет ХИМЕРА, из за чего негодяи стали еще опасней. Поведевает бандой все тот же злой гений - СТРАЖ.",
    new List<Boss>() { MINOTAVR, HIMERA, STRAG });

        public static Quest quest3 = new Quest(3, "И вновь главарю дартимских бандитов удалось сбежать. \n\n" +
    "Власти королевства обещают солидную награду героям, которые положат конец его коварным планам.\n\n" +
    "Разведчик обнаружил логово разбойников в Вентиме, зажиточном торговом городе с развлетвленной системой канализации и сетью туннелей. Просто рай для этой нечисти.\n\n" +
    "Вход в логово охранаяет могучий ГОЛЕМ. Верная сообщница главаря, кровожадная ВАМПИРША, вышла из тени, чтобы вновь помочь своему хозяину.\n\n" +
    "А уаправляет ими все тот же беспощадный СТРАЖ - самый опасный из всей банды.",
    new List<Boss>() { GOLEM, VAMP, STRAG });

        public static Quest quest4 = new Quest(4, "Гном Фонкин Великолепный был богатым ремесленником из тогргового города Вентима. " +
            "Он потратил целое состояние и уйму времени, чтобы собрать внушительную коллекцию волшебных предметов, самоцветов, звериных чучел и разных диковинок. \n\n" +
    "И хотя коллекция музея Фонкина получилась поистине изумительной, нн владелец даже не подозревал, какую опасность она представляет.\n\n" +
    "Когда звезды на небе выстроились особым образом, из саркофага востала древняя МУМИЯ! " +
            "С помощью колдоства воскревший жрец сумел вдохнуть жизнь в два музейных экспоната: огромного ВЕЛИКАНА и свирепую ГИДРУ.\n\n" +
    "Придя в отчаяние от возникшего хаоса, Фонкин надеется, что герои смогут победить чудовищ и в музее вновь воцарится мир и покой.",
    new List<Boss>() { MUMIA, VELIKAN, GIDRA });


        public static Quest quest5 = new Quest(5, "Чародеи утверждали, что звезды выстраиваются нужным образом лишь раз в тысячелетие. " +
            "Но они ошиблись.\n\n" +
    "И вновь вывезенная их некрополя в Хамнапте МУМИЯ сумела выбраться из древнего саркофага. Гнев жреца обрушился на жителей Вентимы, важнегойшего города в западных землях.\n\n" +
    "Воскресший жрец призвал огенного ЭЛЕМЕНТАЛЯ, и тотчас же в Вентиме вспыхнул пожар, посеявший панику среди горожан. Но жрецу показалось мало." +
            " Тогда он колдовством заставил море всколыхнуться, и из бедонной пучины возникла могучая ГИДРА.\n\n" +
    "Сейчас как никогда Вентиме нужны герои.",
    new List<Boss>() { MUMIA, ELEMENTAL, GIDRA });

        public static Quest quest6 = new Quest(6,
            "Говорят, что давным-давно была на свете Восточная империя," +
            " столь могущественная, что ее правители познали секрет жизни и смерти, а так же были способны повелевать стихией.\n\n" +
            "И вот коварная МУМИЯ из Хамнапты, натворившая немало бед в западных земляхк, отправилась на восток в поисках мифической империи и ее легендарного могущества. \n\n" +
            "Вскрыв усыпальницу Первого императора, жрец пробудил его верного слугу ХИМЕРУ, и начал совершать обряд, дабы разбить охранные печати императора - ДРАКОНА.\n\n" +
        "Судьба востока и запада в руках героев. Удастся ли им остановить древнего врага?",
        new List<Boss>() { MUMIA, HIMERA, DRAKON });


        public static Quest quest7 = new Quest(7,
    "Магия - это великая сила. Желая избежать опасностей, которые она может навлечь, величайшие чародеи королевства создали Магический совет." +
            " Они решили объединить мистиков, контролировать колдовские эксперименты и установить орграничения для членов совета.\n\n" +
    "Разумеется, некромантов в Магический совет не пригласили. Заклинатели смерти, как их часто называют, пересекли Море Теней и возвели башню Эстибра," +
            " чтобы устроить в ней жуткую лабораторию для экспериментов с черной магией.  \n\n" +
    "Вход в нее стережет ГАРГУЛЬЯ, нападающая на зазевавшихся путников. " +
            "В самых темных закаулках башни искателей приключений подстеригает ХИМЕРА с острыми когтями и клыками. " +
            "Выживших смельчаков испытывает на прочность сам король нежити, ЛИЧ.\n\n" +
"Судьба востока и запада в руках героев. Удастся ли им остановить древнего врага?",
new List<Boss>() { GARGULIA, HIMERA, LICH });


        public static Quest quest8 = new Quest(8,
    "Вернувшись из башни Эстибра разведчики доложили, что нежить заинетерсовалась храмом Мигор, вместилищем тайн безумного бога Вольско.\n\n" +
    "Стремясь овладеть темным знанием и усилить всое могущество в королевстве, колдуны перебрались в древних храм, надежно скрывающий любые эксперименты от любопытных глаз. " +
            "ГАРГУЛЬЕ велено охранять врата храма и вселять страх во всякого, кто посмеет подойти близко. " +
            "Тех же, кого запугать не получится, сотрет в порошок зачарованный ВЕЛИКАН.\n\n" +
"Но если герои одолеют и его, то им предстоит почувствовать на себе гнев повелителя нечести - ЛИЧА.",
new List<Boss>() { GARGULIA, VELIKAN, LICH });

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
            List<int> indexes = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
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

            quest.missions = new List<int> { m1, m2, m3 };
            selectUserMission.IsEnabled = true;
            slectedQuestButton.IsEnabled = false;

            cbox_quest.IsEnabled = false;

            if (ConfigurationManager.AppSettings["VoiceEnable"].ToString() == "true" )
            {
                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                synthesizer.Speak(tbox_legenda.Text);
            }
        }

        public int randomInt(List<int> indexes)
        {
            return random.Next(indexes.Count());
        }

        private void selectUserMission_Click(object sender, RoutedEventArgs e)
        {
            if (quest.selectMission != 0)
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
            if (quest != null && quest.missions != null)
            {
                user_mission_1.Stroke = Brushes.Red;
                user_mission_1.StrokeThickness = 5;
                user_mission_2.Stroke = null;
                quest.selectMission = int.Parse(user_mission_1.Tag as string);
            }

        }

        private void user_mission_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (quest != null && quest.missions != null)
            {
                user_mission_2.Stroke = Brushes.Red;
                user_mission_2.StrokeThickness = 5;

                user_mission_1.Stroke = null;
                quest.selectMission = int.Parse(user_mission_2.Tag as string);
            }
        }

        private void abbility_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (quest != null && quest.missions != null)
            {
                abbility_1.Stroke = Brushes.Red;
                abbility_1.StrokeThickness = 5;

                abbility_2.Stroke = null;
                quest.selectAbility = int.Parse(abbility_1.Tag as string);
            }
        }

        private void abbility_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (quest != null && quest.missions != null)
            {
                abbility_2.Stroke = Brushes.Red;
                abbility_2.StrokeThickness = 5;

                abbility_1.Stroke = null;
                quest.selectAbility = int.Parse(abbility_2.Tag as string);
            }
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScaleTransform.ScaleX = e.NewSize.Height / 633;
            ScaleTransform.ScaleY = e.NewSize.Height / 633;
        }
    }
}
