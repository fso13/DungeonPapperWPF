using DungeonPapperWPF.code;
using DungeonPapperWPF.windows;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        double zoom = 1.0;
        SolidColorBrush brushGreen = new SolidColorBrush(Colors.Green);
        SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);

        public Random random = new Random(DateTime.Now.Millisecond);

        public static ImageBrush riverBrushVertical = new ImageBrush();
        public static ImageBrush riverBrushHorizontal = new ImageBrush();
        public static ImageBrush woodyBrushVertical = new ImageBrush();
        public static ImageBrush woodyBrushHorizontal = new ImageBrush();
        public static ImageBrush trapBrush = new ImageBrush();
        public static ImageBrush oneBossBrush = new ImageBrush();
        public static ImageBrush twoBossBrush = new ImageBrush();
        public static ImageBrush threeBossBrush = new ImageBrush();


        private static bool isStart = false;
        private static FieldDto[,] fieldDtos = new FieldDto[7, 6];//поля в виже дто
        public static Field[,] fields = new Field[7, 6];//поля в окне приложения

        private int round = 0;//раунд игры
        public static int currentCountStep = 0;//сколько есть клеток передвижения
        public static int currentCountLevel = 0;//сколько есть повышений уровня
        public static int currentCountDice = 0;//сколько еще можно потратить дайсов на действия
        public static int currentCountMagicPart = 0;//сколько еще можно потратить дайсов на действия
        public static int currentCountDeadMonstersFotArtifact = 0;//сколько можно убить монстров за артифакт
        public static int currentCountLevelUpByAbbility = 0;//сколько можно поднять уровней за абилку
        public static int currentCountMagicsByAbbility = 0;//сколько можно крафтить предметов по абилке

        public static Party party;//пати, герои, сокровища и тд

        public static Dice[] generateDices = new Dice[6];//сгенерированный дайсы в раунде
        public static Dice currentDice;//выбранный дайс для действия
        public static List<Dice> selectDicesIsCurrentRound = new List<Dice>();//дайсы которые были выбраны в раунде

        public static Quest quest = null;

        public MainWindow()
        {
            SplashScreen splashScreen = new SplashScreen("Resources/logo.png");
            splashScreen.Show(false);
            /* do some things */


            riverBrushVertical.ImageSource =
                    new BitmapImage(new Uri(@"Resources\river_vertical.png", UriKind.Relative));
            riverBrushHorizontal.ImageSource =
                    new BitmapImage(new Uri(@"Resources\river_horizontal.png", UriKind.Relative));
            woodyBrushVertical.ImageSource =
                    new BitmapImage(new Uri(@"Resources\woody_vertical.png", UriKind.Relative));
            woodyBrushHorizontal.ImageSource =
                    new BitmapImage(new Uri(@"Resources\woody_horizontal.png", UriKind.Relative));
            trapBrush.ImageSource =
                    new BitmapImage(new Uri(@"Resources\trap.png", UriKind.Relative));

            oneBossBrush.ImageSource =
                new BitmapImage(new Uri(@"Resources\boss_1.png", UriKind.Relative));
            twoBossBrush.ImageSource =
                new BitmapImage(new Uri(@"Resources\boss_2.png", UriKind.Relative));
            threeBossBrush.ImageSource =
                new BitmapImage(new Uri(@"Resources\boss_3.png", UriKind.Relative));

            splashScreen.Close(TimeSpan.FromSeconds(5));
            System.Threading.Thread.Sleep(5000);
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fieldDtos = Quests.getQuest(0);
            drawField();
        }


        //отрисовка карты
        private void drawField()
        {
            for (int i = 0; i < 6; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                gridFields.ColumnDefinitions.Add(colDef);
            }

            for (int j = 0; j < 7; j++)
            {
                RowDefinition rowDef = new RowDefinition();
                gridFields.RowDefinitions.Add(rowDef);
            }

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    drawField(i, j);
                }
            }
        }

        //отрисовка комнаты
        private void drawField(int i, int j)
        {
            FieldDto dto = fieldDtos[j, i];

            Field f = new Field();

            if (dto.trap)
            {
                f.trapField.Fill = trapBrush;
            }

            if (dto.prey != null)
            {
                ImageBrush preyBrush = new ImageBrush();
                preyBrush.ImageSource = new BitmapImage(dto.prey.getPath());
                f.preyField.Fill = preyBrush;
            }

            if (dto.monster != null)
            {
                ImageBrush monsterBrush = new ImageBrush();
                monsterBrush.ImageSource = new BitmapImage(dto.monster.getPathMonster());
                f.monsterField.Fill = monsterBrush;

                ImageBrush heroLevelBrush = new ImageBrush();
                heroLevelBrush.ImageSource = new BitmapImage(dto.monster.getPathHeroLevel());
                f.levelHeroField.Fill = heroLevelBrush;
            }

            if (dto.one != null)
            {
                f.monsterField.Fill = oneBossBrush;
            }
            if (dto.two != null)
            {
                f.monsterField.Fill = twoBossBrush;
            }
            if (dto.three != null)
            {
                f.monsterField.Fill = threeBossBrush;
            }

            if (dto.rightBarrier == Barrier.Wall)
            {
                f.rightWallButton.Fill = woodyBrushVertical;
            }
            if (dto.leftBarrier == Barrier.Wall && i == 0)
            {
                f.leftWallButton.Fill = woodyBrushVertical;
            }
            if (dto.topBarrier == Barrier.Wall)
            {
                f.topWallButton.Fill = woodyBrushHorizontal;
            }


            if (dto.rightBarrier == Barrier.Black)
            {
                f.rightWallButton.Fill = Brushes.Black;
            }
            if (dto.leftBarrier == Barrier.Black && i == 0)
            {
                f.leftWallButton.Fill = Brushes.Black;
            }
            if (dto.topBarrier == Barrier.Black)
            {
                f.topWallButton.Fill = Brushes.Black;
            }


            if (dto.leftBarrier == Barrier.River)
            {
                f.leftWallButton.Fill = riverBrushVertical;
            }
            if (dto.downBarrier == Barrier.River)
            {
                f.downWallButton.Fill = riverBrushHorizontal;
            }


            f.Name = String.Format("field_{0}_{1}", i, j);
            Grid.SetRow(f, j);
            Grid.SetColumn(f, i);

            f.dto = dto;
            fields[j, i] = f;
            gridFields.Children.Add(f);
        }

        public Field[] GetRow(int rowNumber)
        {
            return Enumerable.Range(0, fields.GetLength(1))
                    .Select(x => fields[rowNumber, x])
                    .ToArray();
        }

        //кнопка действия движения
        private void TwoMoveButton_Click(object sender, RoutedEventArgs e)
        {
            currentCountStep = 2;
            highlightWhereToGo();
            ButtonCreateMagic.IsEnabled = false;
            ButtonCreatePotion.IsEnabled = false;
            LevelUpButton.IsEnabled = false;
        }

        //подстветка доступных передвижений
        public void highlightWhereToGo()
        {
            if (currentCountStep > 0)
            {
                if (party.path.Count == 0)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        fields[6, i].greanColor();
                    }
                }
                else
                {

                    for (int i = 0; i < 6; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            fields[j, i].clearColor();
                        }
                    }


                    Field last = party.path.Last();


                    //влево
                    if (last.dto.leftBarrier == Barrier.None ||
                        (last.dto.leftBarrier == Barrier.Wall && party.isPresentMagic(4)) ||
                        (last.dto.leftBarrier == Barrier.Black && party.isPresentMagic(4)) ||
                        (last.dto.leftBarrier == Barrier.River && party.isPresentMagic(3)))
                    {
                        if (last.dto.x > 0)
                        {
                            Field left = fields[last.dto.y, last.dto.x - 1];
                            if (left.dto.rightBarrier == Barrier.None ||
                                (left.dto.rightBarrier == Barrier.Wall && party.isPresentMagic(4)) ||
                                (left.dto.rightBarrier == Barrier.Black && party.isPresentMagic(4)) ||
                                (left.dto.rightBarrier == Barrier.River && party.isPresentMagic(3)))
                            {
                                left.greanColor();
                            }
                        }
                        else if (last.dto.x == 0 && (last.dto.y == 3 || last.dto.y == 5))
                        {
                            Field left = fields[last.dto.y, 5];
                            left.greanColor();
                        }
                    }

                    //впрово
                    if (last.dto.rightBarrier == Barrier.None ||
                        (last.dto.rightBarrier == Barrier.Wall && party.isPresentMagic(4)) ||
                        (last.dto.rightBarrier == Barrier.Black && party.isPresentMagic(4)) ||
                        (last.dto.rightBarrier == Barrier.River && party.isPresentMagic(3)))
                    {
                        if (last.dto.x < 5)
                        {
                            Field right = fields[last.dto.y, last.dto.x + 1];
                            if (right.dto.leftBarrier == Barrier.None ||
                                (right.dto.leftBarrier == Barrier.Wall && party.isPresentMagic(4)) ||
                                (right.dto.leftBarrier == Barrier.Black && party.isPresentMagic(4)) ||
                                (right.dto.leftBarrier == Barrier.River && party.isPresentMagic(3)))
                            {
                                right.greanColor();
                            }
                        }
                        else if (last.dto.x == 5 || (last.dto.y == 3 || last.dto.y == 5))
                        {
                            Field right = fields[last.dto.y, 0];
                            right.greanColor();
                        }
                    }

                    //вверх
                    if (last.dto.topBarrier == Barrier.None ||
                        (last.dto.topBarrier == Barrier.Wall && party.isPresentMagic(4)) ||
                        (last.dto.topBarrier == Barrier.Black && party.isPresentMagic(4)) ||
                        (last.dto.topBarrier == Barrier.River && party.isPresentMagic(3)))
                    {
                        if (last.dto.y > 0)
                        {
                            Field top = fields[last.dto.y - 1, last.dto.x];
                            if (top.dto.downBarrier == Barrier.None ||
                                (top.dto.downBarrier == Barrier.Wall && party.isPresentMagic(4)) ||
                                (top.dto.downBarrier == Barrier.Black && party.isPresentMagic(4)) ||
                                (top.dto.downBarrier == Barrier.River && party.isPresentMagic(3)))
                            {
                                top.greanColor();
                            }
                        }

                    }

                    //вниз
                    if (last.dto.downBarrier == Barrier.None ||
                        (last.dto.downBarrier == Barrier.Wall && party.isPresentMagic(4)) ||
                        (last.dto.downBarrier == Barrier.Black && party.isPresentMagic(4)) ||
                        (last.dto.downBarrier == Barrier.River && party.isPresentMagic(3)))
                    {
                        if (last.dto.y < 6)
                        {
                            Field down = fields[last.dto.y + 1, last.dto.x];
                            if (down.dto.topBarrier == Barrier.None ||
                                (down.dto.topBarrier == Barrier.Wall && party.isPresentMagic(4)) ||
                                (down.dto.topBarrier == Barrier.Black && party.isPresentMagic(4)) ||
                                (down.dto.topBarrier == Barrier.River && party.isPresentMagic(3)))
                            {
                                down.greanColor();
                            }
                        }

                    }

                }
            }
            else
            {

                if (MainWindow.currentCountLevel > 0 || MainWindow.currentCountMagicPart > 0 || MainWindow.currentCountDeadMonstersFotArtifact > 0)
                {
                    diceGrid.IsEnabled = false;
                    selectDiceButton.IsEnabled = false;
                }
                else
                {
                    if (!deadMonsterFromAbbility)
                    {
                        deleteCurrentDic();
                    }
                }

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        fields[j, i].clearColor();
                    }
                }
            }

        }

        //отрисовка мировозрени я в окне
        public void drawOutlook()
        {
            party.GetHeroes().ForEach(hero =>
            {
                if (hero.outlook == Outlook.Black)
                {
                    ((CheckBox)this.FindName(hero.getControlOutlookName())).IsChecked = true;
                }
            });

        }

        //отрисовка уровня героев
        public void drawLevel()
        {
            party.GetHeroes().ForEach(hero =>
            {
                for (int i = 0; i < hero.level; i++)
                {
                    ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + (i + 1))).IsChecked = true;
                }
            });

        }

        //нанесение урона
        public void damage(int damage)
        {

            for (int i = 1; i <= damage; i++)
            {
                PartyPotion partyPotion = party.getFirstPotionWithFreeCell();
                if (partyPotion != null)
                {
                    ((CheckBox)this.FindName("potion_" + (party.potions.IndexOf(partyPotion) + 1) + "_" + (3 - partyPotion.freeCell))).IsChecked = true;
                }
                else
                {
                    if(1 + party.blood <= 25)
                    {
                        ((CheckBox)this.FindName("blood_" + (1 + party.blood))).IsChecked = true;
                    }
                }

                party.damage(1);
            }

            if (party.isDeadPaty)
            {
                cbox_party_dead.IsChecked = true;
            }

            MessageBox.Show("здоровье стало: " + (party.hp - party.blood));
        }

        //ролучение уровня за комнату
        public void levelUpFromMove()
        {
            currentCountLevel++;
            diceGrid.IsEnabled = false;
            selectDiceButton.IsEnabled = false;
            buttonDiceGenereted.IsEnabled = false;
            party.GetHeroes().ForEach(hero =>
            {
                if (hero.level + 1 < 7)
                {
                    ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + (hero.level + 1))).IsEnabled = true;
                }
            });

            if (party.hp < 24)
            {
                selectDiceButton.IsEnabled = false;
                diceGrid.IsEnabled = false;
            }

            MessageBox.Show("Повышение уровня");

        }

        public bool levelFromDice = false;
        //выбор действия поднятия уровня
        private void LevelUpButton_Click(object sender, RoutedEventArgs e)
        {
            levelFromDice = true;
            LevelUpButton.IsEnabled = false;
            currentCountLevel++;
            diceGrid.IsEnabled = false;
            selectDiceButton.IsEnabled = false;
            ButtonCreateMagic.IsEnabled = false;
            ButtonCreatePotion.IsEnabled = false;
            TwoMoveButton.IsEnabled = false;

            party.GetHeroes().ForEach(hero =>
            {
                if (hero.getNumberDiceForLevel() == selectDicesIsCurrentRound.Last().number ||
                    selectDicesIsCurrentRound.Last().number == 9 ||
                    (party.wizard.level > 3 && selectDicesIsCurrentRound.Last().type.ToString() == hero.type.ToString()))
                {
                    if (hero.level + 1 < 7)
                    {
                        ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + (hero.level + 1))).IsEnabled = true;
                    }
                }
            });

        }

        //выбор нового уровня у персонажа
        private void cbox_Level_Checked(object sender, RoutedEventArgs e)
        {

            if (currentCountLevel > 0)
            {
                CheckBox checkBox = (CheckBox)sender;
                checkBox.IsChecked = true;
                string name = checkBox.Name;
                party.GetHeroes().ForEach(hero =>
                {

                    if (name.Contains(hero.type.ToString().ToLower()))
                    {
                        hero.level++;
                        if (hero.level == 4 && hero.type == HeroClassType.Plut)
                        {
                            MessageBox.Show("Ваш плут достиг 4 уровня, и где то раздобыл алмаз");
                            addDiamond(new Diamond("За уровень плута"));
                        }

                        if (hero.level == 4 && hero.type == HeroClassType.Warrior)
                        {
                            party.addDamageWithBosses += 1;
                        }

                        if (hero.level == 5 && party.isAddMagicFrom5Level)
                        {
                            MessageBox.Show("Ваш герой достиг 5 уровня, получите часть предмета");
                            currentCountMagicPart++;
                            highlightCreateMagic(null);
                        }
                        addHp();
                        currentCountLevel--;
                    }

                });
                party.GetHeroes().ForEach(hero =>
                {

                    for (int i = 1; i <= 6; i++)
                    {
                        ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + i)).IsEnabled = false;
                    }

                });

                if (currentCountLevel > 0)
                {
                    party.GetHeroes().ForEach(hero =>
                    {
                        if ((hero.level + 1) < 7)
                        {
                            ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + (hero.level + 1))).IsEnabled = true;
                        }
                    });
                }

                if (currentCountLevel == 0 && currentCountMagicPart == 0 && currentCountStep == 0)
                {

                    if (selectDicesIsCurrentRound.Count() == 3)
                    {
                        buttonDiceGenereted.IsEnabled = true;
                    }
                    selectDiceButton.IsEnabled = true;
                    diceGrid.IsEnabled = true;
                }
            }
            else if (currentCountLevelUpByAbbility > 0)
            {
                CheckBox checkBox = (CheckBox)sender;
                checkBox.IsChecked = true;
                string name = checkBox.Name;
                party.GetHeroes().ForEach(hero =>
                {

                    if (name.Contains(hero.type.ToString().ToLower()))
                    {
                        hero.level++;
                        addHp();
                        currentCountLevelUpByAbbility--;
                    }

                });

                party.GetHeroes().ForEach(hero =>
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + i)).IsEnabled = false;
                    }

                });


                if (currentCountLevelUpByAbbility > 0)
                {
                    hieghtingLevelUpByAbbility();
                }
            }

            if (isStart && selectDicesIsCurrentRound.Count>0)
            {
                if (currentCountLevel == 0)
                {

                    if (levelFromDice)
                    {
                        levelFromDice = false;
                        deleteCurrentDic();
                    }
                    else if (currentCountMagicPart == 0 && currentCountStep == 0)
                    {
                        deleteCurrentDic();
                    }
                }
            }
        }

        //удаление текущего кубика из пула
        private void deleteCurrentDic()
        {
            if (isStart)
            {

                for (int i = 0; i < 6; i++)
                {
                    Rectangle rectangle = ((Rectangle)this.FindName("dice" + (i + 1) + "_pic"));

                    if (!selectDicesIsCurrentRound.Contains(rectangle.Tag as Dice) && ((Dice)rectangle.Tag)!=null && ((Dice)rectangle.Tag).number < 10)
                    {
                        rectangle.IsEnabled = true;
                    }

                }

                selectDicesIsCurrentRound.Last().rectangle.Stroke = null;
                selectDicesIsCurrentRound.Last().rectangle.Fill = null;
                selectDicesIsCurrentRound.Last().rectangle.IsEnabled = false;

                currentCountDice--;

                if (selectDicesIsCurrentRound.Count() == 3)
                {
                    if (round == 3 || round == 6 || round == 8)
                    {
                        Boss boss = null;
                        if (round == 3)
                        {
                            boss = quest.bosses[0];
                        }
                        else if (round == 6)
                        {
                            boss = quest.bosses[1];
                        }
                        else if (round == 8)
                        {
                            boss = quest.bosses[2];
                        }

                        BossFightWindows.boss = boss;
                        new BossFightWindows().ShowDialog();


                        if (boss.isDead)
                        {
                            int damageParty = party.getDamageByBoss(boss);
                            int xp = 0;
                            if (damageParty >= boss.lastDamage.heroLevel)
                            {
                                xp = boss.lastDamage.xp;

                                if (!party.isIgnoreDamageFromBoss)
                                {
                                    for (int i = 0; i < boss.lastDamage.damage; i++)
                                    {
                                        damage(1);
                                    }
                                }
                            }
                            else if (damageParty >= boss.middleDamage.heroLevel)
                            {
                                xp = boss.middleDamage.xp;

                                if (!party.isIgnoreDamageFromBoss)
                                {
                                    for (int i = 0; i < boss.middleDamage.damage; i++)
                                    {
                                        damage(1);
                                    }
                                }
                            }
                            else if (damageParty >= boss.firstDamage.heroLevel)
                            {
                                xp = boss.firstDamage.xp;
                                if (!party.isIgnoreDamageFromBoss)
                                {
                                    for (int i = 0; i < boss.firstDamage.damage; i++)
                                    {
                                        damage(1);
                                    }
                                }
                            }


                            if (round == 3)
                            {
                                party.xpBoss1 = xp;
                                label_boss_1_xp.Content = xp;
                                boss_1_rec.Opacity = 0.3;
                            }
                            else if (round == 6)
                            {
                                party.xpBoss2 = xp;
                                label_boss_2_xp.Content = xp;
                                boss_2_rec.Opacity = 0.3;

                            }
                            else if (round == 8)
                            {
                                party.xpBoss3 = xp;
                                label_boss_3_xp.Content = xp;
                                boss_3_rec.Opacity = 0.3;

                            }

                         
                           

                        }
                        else
                        {
                            if (round == 3)
                            {
                                party.xpBoss1 = -boss.minusXp;
                                label_boss_1_xp.Content = -boss.minusXp;
                                boss_1_rec.Opacity = 0.3;
                            }
                            else if (round == 6)
                            {
                                party.xpBoss2 = -boss.minusXp;
                                label_boss_2_xp.Content = -boss.minusXp;
                                boss_2_rec.Opacity = 0.3;
                            }
                            else if (round == 8)
                            {
                                party.xpBoss3 = -boss.minusXp;
                                label_boss_3_xp.Content = -boss.minusXp;
                                boss_3_rec.Opacity = 0.3;
                            }
                        }

                        boss.hideDiamonds.ForEach(d => {
                            for (int i = 0; i < 6; i++)
                            {
                                for (int j = 0; j < 7; j++)
                                {
                                    Field f = fields[j, i];
                                    if(f.dto.prey!=null && f.dto.prey is Diamond)
                                    {
                                        if(((Diamond)f.dto.prey).name == d.name)
                                        {
                                            f.dto.prey = null;
                                            f.preyFieldAction.Fill = Field.opacityMaskKrestBrush;
                                            f.preyField.Opacity = 1;
                                        }
                                    }
                                }
                            }

                        });
                            //todo убрать алмазы
                    }

                    if (round == 8)
                    {
                        new TotalXP().ShowDialog();

                    }
                    buttonDiceGenereted.IsEnabled = true;

                    for (int i = 0; i < 6; i++)
                    {
                        Rectangle rectangle = ((Rectangle)this.FindName("dice" + (i + 1) + "_pic"));
                        rectangle.Stroke = null;
                        rectangle.Fill = null;
                        rectangle.IsEnabled = false;
                    }
                }

                selectDiceButton.IsEnabled = true;
                diceGrid.IsEnabled = true;
                LevelUpButton.IsEnabled = false;
                TwoMoveButton.IsEnabled = false;
                ButtonCreatePotion.IsEnabled = false;
                ButtonCreateMagic.IsEnabled = false;


                //проверка выполняния миссий
                compliteMissions();
            }
        }

        //проверка выполняния миссий
        public void compliteMissions()
        {
            for (int i = 1; i <= 3; i++)
            {
                compliteMission(quest.missions[i - 1], i);
            }


            if (quest.roundMissionComplete1>0)
            {
                mission_1_rec.Opacity = 0.3;
            }
            if (quest.roundMissionComplete2 > 0)
            {
                mission_2_rec.Opacity = 0.3;
            }
            if (quest.roundMissionComplete3 > 0)
            {
                mission_3_rec.Opacity = 0.3;
            }

        }

        public void compliteMission(int number, int index)
        {
            int r = -1;

            if(index == 1)
            {
                r = quest.roundMissionComplete1;
            }
            else if (index == 2)
            {
                r = quest.roundMissionComplete2;
            }
            else if (index == 3)
            {
                r = quest.roundMissionComplete3;
            }

            if (r == -1)
            {
                switch (number)
                {
                    case 1:
                        {
                            if (party.path.FindAll(f => f.dto.one != null || f.dto.two != null || f.dto.three != null).Count() == 3)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (party.GetHeroes().FindAll(f => f.level >= 5).Count() >= 2)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    case 3:
                        {
                            int[] columns = new int[6] { 0, 0, 0, 0, 0, 0 };

                            party.path.ForEach(row =>
                            {
                                columns[row.dto.x] += 1;
                            });

                            int rr = columns.OfType<int>().ToList().FindAll(x => x == 7).Count();
                            if (rr >= 1)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    case 4:
                        {
                            int room = 0;

                            party.path.ForEach(row =>
                            {
                                if ((row.dto.x == 0 && row.dto.y == 0) ||
                                (row.dto.x == 0 && row.dto.y == 6) ||
                                (row.dto.x == 5 && row.dto.y == 0) ||
                                (row.dto.x == 5 && row.dto.y == 6))
                                {
                                    room++;
                                }
                            });

                            if (room >=2)
                            {
                                r = this.round;
                            }

                            break;
                        }
                    case 5:
                        {
                            int one = party.deadMonsters.FindAll(m=>m.heroClass.level==3).Count();
                            int two = party.deadMonsters.FindAll(m => m.heroClass.level == 4).Count();
                            int three = party.deadMonsters.FindAll(m => m.heroClass.level == 5).Count();

                            if(one >1 && two>0 && three > 0)
                            {
                                r = this.round;
                            }
                            
                            break;
                        }
                    case 6:
                        {
                            if (party.magics.FindAll(m => m.countPart == 2).Count() >= 4)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    case 7:
                        {
                            int[] rows = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

                            party.path.ForEach(row =>
                            {
                                rows[row.dto.y] += 1;
                            });

                            int rr = rows.OfType<int>().ToList().FindAll(x => x == 6).Count();
                            if (rr >=1)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    case 8:
                        {

                            if (party.path.Count() >= 15)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    case 9:
                        {
                            if (party.diamonds.Count() >= 4)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    case 10:
                        {
                            if(party.GetHeroes().FindAll(h=>h.level>=4).Count() >= 3)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    case 11:
                        {
                            if (party.GetHeroes().FindAll(h => h.level == 6).Count() >= 1)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    case 12:
                        {
                            if (party.deadMonsters.Count() >= 6)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    case 13:
                        {
                            if (party.potions.Count() >= 8)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    case 14:
                        {
                            if (party.GetHeroes().FindAll(h => h.level >= 3).Count() ==4)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    case 15:
                        {
                            if(party.hp >= 12)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    case 16:
                        {

                            List<FieldDto> f = new List<FieldDto>();
                            party.path.FindAll(fff => fff.dto.trap).ForEach(ff =>
                            {
                               if( f.FindAll(dto=>dto.x == ff.dto.x && dto.y == ff.dto.y).Count() == 0)
                                {
                                    f.Add(ff.dto);
                                }
                            });
                            if (f.Count() >= 5)
                            {
                                r = this.round;
                            }
                            break;
                        }
                    defaut: break;
                }


                if (index == 1)
                {
                  quest.roundMissionComplete1 = r;
                }
                else if (index == 2)
                {
                    quest.roundMissionComplete2 = r;
                }
                else if (index == 3)
                {
                    quest.roundMissionComplete3 = r;
                }
            }
        }

        private List<Dice> diceGenereted()
        {
            int diceScull = 0;
            int diceKlever = 0;
            List<Dice> dices = new List<Dice>();
            for (int i = 0; i < 6; i++)
            {
                Dice genereteDice = Dice.fromNumber(random.Next(12));
                if (genereteDice.type == DiceType.Scull)
                {
                    diceScull++;
                }

                if (genereteDice.type == DiceType.Klever)
                {
                    diceKlever++;
                }

                if (diceKlever > 2 || diceScull > 2)
                {
                    MessageBox.Show("Переброска кубиков");
                    currentCountDice = 0;
                    return diceGenereted();
                }
                dices.Add(genereteDice);

            }

            return dices;
        }

        //бросок кубиков
        private void buttonDiceGenereted_Click(object sender, RoutedEventArgs e)
        {
            if (round < 9)
            {
                round++;

                this.Title = "Dungeon Paper Раудн: " + round;
                selectDicesIsCurrentRound.Clear();
                buttonDiceGenereted.IsEnabled = false;
                selectDiceButton.IsEnabled = true;
                diceGrid.IsEnabled = true;
                currentCountDice = 3;

                List<Dice> dices = diceGenereted();

                int damage = 0;
                for (int i = 0; i < 6; i++)
                {
                    Dice genereteDice = dices.ElementAt(i);

                    Rectangle rectangle = ((Rectangle)this.FindName("dice" + (i + 1) + "_pic"));
                    rectangle.IsEnabled = true;
                    generateDices[i] = genereteDice;
                    generateDices[i].rectangle = rectangle;
                    ImageBrush brush = new ImageBrush();
                    brush.ImageSource = generateDices[i].getPath();
                    rectangle.Fill = brush;
                    rectangle.Tag = generateDices[i];

                    if (generateDices[i].number > 9)
                    {
                        if (party.cleric.level < 4)
                        {
                            damage++;
                        }
                        rectangle.IsEnabled = false;
                        rectangle.Stroke = brushRed;
                        rectangle.StrokeThickness = 4;
                    }
                }

                if (party.cleric.level < 4)
                {
                    if (party.isIgnoreOneSkull)
                    {
                        damage--;
                    }
                    if (damage > 0)
                    {
                        for (int i = 0; i < damage; i++)
                        {
                            this.damage(1);
                        }
                    }
                }
            }
        }

       //получение жизни
        public void addHp()
        {
            party.addHp();
            ((CheckBox)this.FindName("hp_" + party.hp)).IsChecked = true;
        }

        //наведение на кубик
        private void dice_pic_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                Rectangle r = ((Rectangle)this.FindName("dice" + (i + 1) + "_pic"));
                if (r.Stroke == brushGreen)
                {
                    r.Stroke = null;
                }
            }

            Rectangle rectangle = (Rectangle)sender;
            rectangle.Stroke = brushGreen;
            rectangle.StrokeThickness = 4;
            currentDice = rectangle.Tag as Dice;

        }


        public bool addPotion(int count)
        {
            bool isNotDeleteDice = false;
            for (int i = 1; i <= count; i++)
            {
                if (i + party.potions.Count() < 13)
                {
                    ((CheckBox)this.FindName("potion_" + (i + party.potions.Count()))).IsChecked = true;

                }
            }

            if (party.potions.Count() < 4 && party.potions.Count() + count >= 4)
            {
                MessageBox.Show("Вы сварили 4 зелья, за это вы получаете уровень");
                levelUpFromMove();
                isNotDeleteDice = true;
            }
            else if (party.potions.Count() < 8 && party.potions.Count() + count >= 8)
            {
                MessageBox.Show("Вы сварили 8 зелий, за это вы получаете часть магического предмета");
                currentCountMagicPart++;
                highlightCreateMagic(null);
                isNotDeleteDice = true;

            }
            else if (party.potions.Count() < 12 && party.potions.Count() + count >= 12)
            {
                MessageBox.Show("Вы сварили 12 зелий, за это вы получаете алмаз");
                addDiamond(new Diamond("За 12 зелий"));
            }

            for (int i = 1; i <= count; i++)
            {
                if (1 + party.potions.Count() < 13)
                {
                    party.addPotion(1);

                }
            }
            return isNotDeleteDice;

        }

        public void addDiamond(Diamond diamond)
        {

            if (party.diamonds.Count() < 10)
            {
                ((Rectangle)this.FindName("Diamond_" + (party.diamonds.Count() + 1))).Fill.Opacity = 1;

                party.addDiamond(diamond);

                if (party.isAddPotionFromDiamand)
                {
                    addPotion(1);
                }
                if (party.diamonds.Count() == 2 || party.diamonds.Count() == 10)
                {
                    currentCountMagicPart++;
                    highlightCreateMagic(null);
                }
                if (party.diamonds.Count() == 4 || party.diamonds.Count() == 8)
                {
                    addPotion(1);
                }
                if (party.diamonds.Count() == 6)
                {
                    levelUpFromMove();
                }
            }

        }

        //выбор кубика
        private void selectDiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentDice != null)
            {

                diceGrid.IsEnabled = false;
                selectDiceButton.IsEnabled = false;
                selectDicesIsCurrentRound.Add(currentDice);

                this.Title = "Dungeon Paper Раудн: " + round + "Кубик номер: " + selectDicesIsCurrentRound.Count();

                ImageBrush brush = new ImageBrush();
                brush.ImageSource = currentDice.getPath();

                ((Rectangle)this.FindName("dice_" + round + "_" + selectDicesIsCurrentRound.Count())).Fill = brush;

                for (int i = 0; i < 6; i++)
                {
                    ((Rectangle)this.FindName("dice" + (i + 1) + "_pic")).IsEnabled = false;
                }

                //три сапога
                if (selectDicesIsCurrentRound.Last().number == 0)
                {
                    currentCountStep = 3;
                    highlightWhereToGo();
                }
                else
                {
                    TwoMoveButton.IsEnabled = true;

                    if (party.potions.Count < 12)
                    {
                        ButtonCreatePotion.IsEnabled = true;
                    }

                    //клевер
                    if (selectDicesIsCurrentRound.Last().number == 9)
                    {
                        if (party.GetHeroes().Find(h => h.level < 6) != null)
                        {
                            LevelUpButton.IsEnabled = true;
                        }
                        ButtonCreateMagic.IsEnabled = true;
                    }

                    //проверка выбора кнопки поднять уровень
                    if (selectDicesIsCurrentRound.Last().number > 0 && selectDicesIsCurrentRound.Last().number < 9)
                    {

                        if ((party.wizard.level > 3 || party.warrior.outlook == Outlook.White) && party.warrior.level < 6 && selectDicesIsCurrentRound.Last().number == 1)
                        {
                            LevelUpButton.IsEnabled = true;
                        }
                        if ((party.wizard.level > 3 || party.warrior.outlook == Outlook.Black) && party.warrior.level < 6 && selectDicesIsCurrentRound.Last().number == 5)
                        {
                            LevelUpButton.IsEnabled = true;
                        }

                        if ((party.wizard.level > 3 || party.wizard.outlook == Outlook.White) && party.wizard.level < 6 && selectDicesIsCurrentRound.Last().number == 2)
                        {
                            LevelUpButton.IsEnabled = true;
                        }
                        if ((party.wizard.level > 3 || party.wizard.outlook == Outlook.Black) && party.wizard.level < 6 && selectDicesIsCurrentRound.Last().number == 6)
                        {
                            LevelUpButton.IsEnabled = true;
                        }

                        if ((party.wizard.level > 3 || party.cleric.outlook == Outlook.White) && party.cleric.level < 6 && selectDicesIsCurrentRound.Last().number == 3)
                        {
                            LevelUpButton.IsEnabled = true;
                        }
                        if ((party.wizard.level > 3 || party.cleric.outlook == Outlook.Black) && party.cleric.level < 6 && selectDicesIsCurrentRound.Last().number == 7)
                        {
                            LevelUpButton.IsEnabled = true;
                        }

                        if ((party.wizard.level > 3 || party.plut.outlook == Outlook.White) && party.plut.level < 6 && selectDicesIsCurrentRound.Last().number == 4)
                        {
                            LevelUpButton.IsEnabled = true;
                        }
                        if ((party.wizard.level > 3 || party.plut.outlook == Outlook.Black) && party.plut.level < 6 && selectDicesIsCurrentRound.Last().number == 8)
                        {
                            LevelUpButton.IsEnabled = true;
                        }

                    }

                    //проверка выбора кнопки ковать предмет
                    if (selectDicesIsCurrentRound.Last().number > 0 && selectDicesIsCurrentRound.Last().number < 9)
                    {
                        List<PartyMagic> magics = party.magics;

                        if (magics.Count == 0)
                        {
                            ButtonCreateMagic.IsEnabled = true;
                        }
                        else
                        {
                            if (selectDicesIsCurrentRound.Last().type > DiceType.Move3 && selectDicesIsCurrentRound.Last().type < DiceType.Klever)
                            {
                                PartyMagic findMagic = null;

                                foreach (PartyMagic magic in magics)
                                {
                                    if (magic.number == (int)selectDicesIsCurrentRound.Last().type)
                                    {
                                        findMagic = magic;
                                        break;
                                    }
                                }

                                if (findMagic == null)
                                {
                                    ButtonCreateMagic.IsEnabled = true;
                                }
                                else if (findMagic.countPart < 2)
                                {
                                    ButtonCreateMagic.IsEnabled = true;
                                }
                            }



                            for (int i = 5; i < 9; i++)
                            {
                                PartyMagic findMagic = null;

                                foreach (PartyMagic magic in magics)
                                {
                                    if (magic.number == i)
                                    {
                                        findMagic = magic;
                                        break;
                                    }
                                }

                                if (findMagic == null)
                                {
                                    ButtonCreateMagic.IsEnabled = true;
                                }
                                else if (findMagic.countPart < 2)
                                {
                                    ButtonCreateMagic.IsEnabled = true;
                                }

                            }
                        }
                    }
                }

                currentDice = null;
            }
        }

        private void ButtonCreatePotion_Click(object sender, RoutedEventArgs e)
        {
            int count = party.isAddPotionFromCreatePOtionByDice ? 3 : 2;
            bool isNotDeleteDice = addPotion(count);
            if (!isNotDeleteDice)
            {
                deleteCurrentDic();
            }
        }

        private void ButtonCreateMagic_Click(object sender, RoutedEventArgs e)
        {
            Dice dice = selectDicesIsCurrentRound.Last();
            currentCountMagicPart++;
            highlightCreateMagic(dice);
            ButtonCreateMagic.IsEnabled = false;
            ButtonCreatePotion.IsEnabled = false;
            TwoMoveButton.IsEnabled = false;
            LevelUpButton.IsEnabled = false;
            magicFromDice = true;
        }

        public void highlightCreateMagic(Dice dice)
        {
            if (currentCountMagicPart > 0)
            {

                selectDiceButton.IsEnabled = false;
                diceGrid.IsEnabled = false;
                LevelUpButton.IsEnabled = false;
                TwoMoveButton.IsEnabled = false;
                List<PartyMagic> magics = party.magics;

                if (dice == null)
                {
                    //подсветить любой предмет
                    for (int i = 1; i < 9; i++)
                    {
                        PartyMagic findMagic = null;

                        foreach (PartyMagic magic in magics)
                        {
                            if (magic.number == i)
                            {
                                findMagic = magic;
                                break;
                            }
                        }

                        if (findMagic == null)
                        {
                            ((CheckBox)this.FindName("magic_" + i + "_" + 1)).IsEnabled = true;
                        }
                        else if (findMagic.countPart < 2)
                        {
                            ((CheckBox)this.FindName("magic_" + i + "_" + (1 + findMagic.countPart))).IsEnabled = true;
                        }


                    }
                }
                else
                {
                    if (magics.Count == 0)
                    {
                        if (dice.type == DiceType.Klever)
                        {
                            for (int i = 1; i < 9; i++)
                            {
                                ((CheckBox)this.FindName("magic_" + i + "_" + 1)).IsEnabled = true;
                            }
                        }
                        else
                        {
                            ((CheckBox)this.FindName("magic_" + (int)dice.type + "_" + 1)).IsEnabled = true;
                            for (int i = 5; i < 9; i++)
                            {
                                ((CheckBox)this.FindName("magic_" + i + "_" + 1)).IsEnabled = true;
                            }
                        }


                    }
                    else
                    {

                        if (dice.type > DiceType.Move3 && dice.type <= DiceType.Klever)
                        {
                            PartyMagic findMagic = null;

                            foreach (PartyMagic magic in magics)
                            {
                                if (magic.number == (int)dice.type || dice.type == DiceType.Klever)
                                {
                                    findMagic = magic;

                                    if (findMagic.countPart < 2)
                                    {
                                        ((CheckBox)this.FindName("magic_" + findMagic.number + "_" + (1 + findMagic.countPart))).IsEnabled = true;
                                    }
                                }
                            }
                            for (int i = 1; i < 5; i++)
                            {
                                if (dice.type == DiceType.Klever)
                                {
                                    if (magics.Find(m => m.number == i) == null)
                                    {
                                        ((CheckBox)this.FindName("magic_" + i + "_" + 1)).IsEnabled = true;
                                    }
                                }
                                else
                                {
                                    if (findMagic == null)
                                    {
                                        ((CheckBox)this.FindName("magic_" + (int)dice.type + "_" + 1)).IsEnabled = true;

                                    }
                                }
                            }

                        }


                        for (int i = 5; i < 9; i++)
                        {
                            PartyMagic findMagic = null;

                            foreach (PartyMagic magic in magics)
                            {
                                if (magic.number == i)
                                {
                                    findMagic = magic;
                                    break;
                                }
                            }

                            if (findMagic == null)
                            {
                                ((CheckBox)this.FindName("magic_" + i + "_" + 1)).IsEnabled = true;
                            }
                            else if (findMagic.countPart < 2)
                            {
                                ((CheckBox)this.FindName("magic_" + i + "_" + (1 + findMagic.countPart))).IsEnabled = true;
                            }

                        }

                    }
                }
            }
        }

        public bool magicFromDice = false;


        //подсветка могстров для убийства по артефакту
        private void hieghtingdeadMonsterFromArtifact()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    fields[j, i].clearColor();
                }
            }

            //подсветить монстра с локаций
            if (currentCountDeadMonstersFotArtifact > 0)
            {

                foreach (Field field in gridFields.Children)
                {
                    if (field.dto.monster != null && !field.dto.monster.isDead)
                    {
                        field.redColor();
                    }
                }
            }
        }

        public void deadMonsterFromArtifact(Monster monster)
        {
            currentCountDeadMonstersFotArtifact--;
            monster.isDead = true;
            party.deadMonsters.Add(monster);

            if (party.deadMonsters.Count % 3 == 0 && party.isAddPotionFromDeadn3Monster)
            {
                addPotion(1);
            }

            ((CheckBox)this.FindName("Monster_" + party.deadMonsters.Count())).IsChecked = true;

            if (currentCountDeadMonstersFotArtifact == 0)
            {
                {
                    highlightWhereToGo();
                }
            }

            if(currentCountDeadMonstersFotArtifact == 0 && deadMonsterFromAbbility)
            {
                deadMonsterFromAbbility = false;
            }
        }


        public void deadMonster(Monster monster)
        {
            int damage = 0;
            int level = 0;
            switch (monster.heroClass.type)
            {
                case HeroClassType.Warrior:
                    level = party.warrior.level;
                    break;
                case HeroClassType.Wizard:
                    level = party.wizard.level;
                    break;
                case HeroClassType.Cleric:
                    level = party.cleric.level;
                    break;
                case HeroClassType.Plut:
                    level = party.plut.level;
                    break;

            }
            //урон от монстра
            damage = level + party.addDamageWithMonsters >= monster.heroClass.level ? 0 : monster.heroClass.level - (level + party.addDamageWithMonsters);
            for (int i = 1; i <= damage; i++)
            {
                this.damage(1);
            }

            monster.isDead = true;
            party.deadMonsters.Add(monster);

            ((CheckBox)this.FindName("Monster_" + party.deadMonsters.Count())).IsChecked = true;

            if (party.deadMonsters.Count % 3 == 0 && party.isAddPotionFromDeadn3Monster)
            {
                addPotion(1);
            }
        }

        private void magic_Checked(object sender, RoutedEventArgs e)
        {
            if (currentCountMagicPart > 0)
            {
                CheckBox checkBox = (CheckBox)sender;
                checkBox.IsChecked = true;

                string name = checkBox.Name.Replace("magic_", "");

                if (name.EndsWith("_1"))
                {
                    party.addMagic(new PartyMagic(int.Parse(name[0].ToString()), 1));
                    currentCountMagicPart--;
                }
                else
                {
                    party.magics.ForEach(magic =>
                    {

                        if (magic.number == int.Parse(name[0].ToString()))
                        {
                            magic.create();

                            if (magic.number == 6)
                            {
                                addPotion(3);
                            }

                            if (magic.number == 8)
                            {
                                levelUpFromMove();
                            }

                            if (magic.number == 2)
                            {
                                currentCountDeadMonstersFotArtifact = 2;
                                hieghtingdeadMonsterFromArtifact();
                            }

                            if (magic.number == 1)
                            {
                                party.addDamageWithBosses += 3;
                            }

                            if (magic.number == 7)
                            {
                                party.addDamageWithMonsters += 1;
                            }

                            currentCountMagicPart--;
                        }
                    });
                }


                for (int i = 1; i <= 8; i++)
                {
                    ((CheckBox)this.FindName("magic_" + i + "_1")).IsEnabled = false;
                    ((CheckBox)this.FindName("magic_" + i + "_2")).IsEnabled = false;
                }

                if (currentCountMagicPart > 0)
                {
                    highlightCreateMagic(null);
                }
                else
                {

                    if (selectDicesIsCurrentRound.Count() == 3)
                    {
                        buttonDiceGenereted.IsEnabled = true;
                    }
                    if ((currentCountLevel == 0 && !magicFromDice && currentCountStep == 0 && currentCountDeadMonstersFotArtifact == 0) ||
                        (magicFromDice && currentCountDeadMonstersFotArtifact == 0))
                    {
                        selectDiceButton.IsEnabled = true;
                        diceGrid.IsEnabled = true;
                        deleteCurrentDic();

                        if (magicFromDice)
                        {
                            magicFromDice = false;
                        }

                    }
                }
            }else if (currentCountMagicsByAbbility > 0)
            {
                CheckBox checkBox = (CheckBox)sender;
                checkBox.IsChecked = true;

                string name = checkBox.Name.Replace("magic_", "");

                if (name.EndsWith("_1"))
                {
                    party.addMagic(new PartyMagic(int.Parse(name[0].ToString()), 1));
                    currentCountMagicsByAbbility--;
                }

                for (int i = 1; i <= 8; i++)
                {
                    ((CheckBox)this.FindName("magic_" + i + "_1")).IsEnabled = false;
                    ((CheckBox)this.FindName("magic_" + i + "_2")).IsEnabled = false;
                }

                hieghtingMagicsByAbbility();
            }
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            new WindowsStart().ShowDialog();

            fieldDtos = Quests.getQuest(quest.questNumber);

            ImageBrush imageBrushm1 = new ImageBrush();
            imageBrushm1.ImageSource =
                new BitmapImage(new Uri(@"Resources\monstr_" + quest.bosses[0].number + ".jpg", UriKind.Relative));
            boss_1_rec.Fill = imageBrushm1;

            ImageBrush imageBrushm2 = new ImageBrush();
            imageBrushm2.ImageSource =
                new BitmapImage(new Uri(@"Resources\monstr_" + quest.bosses[1].number + ".jpg", UriKind.Relative));
            boss_2_rec.Fill = imageBrushm2;

            ImageBrush imageBrushm3 = new ImageBrush();
            imageBrushm3.ImageSource =
                new BitmapImage(new Uri(@"Resources\monstr_" + quest.bosses[2].number + ".jpg", UriKind.Relative));
            boss_3_rec.Fill = imageBrushm3;

            ImageBrush imageBrush1 = new ImageBrush();
            imageBrush1.ImageSource =
                new BitmapImage(new Uri(@"Resources\mission_" + quest.missions[0] + ".jpg", UriKind.Relative));
            mission_1_rec.Fill = imageBrush1;

            ImageBrush imageBrush2 = new ImageBrush();
            imageBrush2.ImageSource =
                new BitmapImage(new Uri(@"Resources\mission_" + quest.missions[1] + ".jpg", UriKind.Relative));
            mission_2_rec.Fill = imageBrush2;

            ImageBrush imageBrush3 = new ImageBrush();
            imageBrush3.ImageSource =
                new BitmapImage(new Uri(@"Resources\mission_" + quest.missions[2] + ".jpg", UriKind.Relative));
            mission_3_rec.Fill = imageBrush3;


            ImageBrush imageBrush4 = new ImageBrush();
            imageBrush4.ImageSource =
                new BitmapImage(new Uri(@"Resources\user_mission_" + quest.selectMission + ".png", UriKind.Relative));
            userMission_rec.Fill = imageBrush4;

            ImageBrush imageBrush6 = new ImageBrush();
            imageBrush6.ImageSource =
                new BitmapImage(new Uri(@"Resources\ability_" + quest.selectAbility + ".png", UriKind.Relative));
            abbility_rec.Fill = imageBrush6;


            gridFields.Children.Clear();
            gridFields.RowDefinitions.Clear();
            gridFields.ColumnDefinitions.Clear();

            drawField();

            if (!isStart)
            {
                party = new Party();
                addHp();
                addHp();
                addHp();
                addHp();
                //TwoMoveButton.IsEnabled = true;
                //LevelUpButton.IsEnabled = true;
                buttonDiceGenereted.IsEnabled = true;

                if (quest.selectMission == 1 ||
                    quest.selectMission == 5 ||
                    quest.selectMission == 8 ||
                    quest.selectMission == 9 ||
                    quest.selectMission == 11 ||
                    quest.selectMission == 12 ||
                    quest.selectMission == 13 ||
                    quest.selectMission == 15)
                {
                    party.warrior = new HeroClass(HeroClassType.Warrior, 1, Outlook.Black);
                }
                else
                {
                    party.warrior = new HeroClass(HeroClassType.Warrior, 1, Outlook.White);
                }

                if (quest.selectMission == 3 ||
                    quest.selectMission == 4 ||
                    quest.selectMission == 5 ||
                    quest.selectMission == 7 ||
                    quest.selectMission == 10 ||
                    quest.selectMission == 12 ||
                    quest.selectMission == 16)
                {
                    party.wizard = new HeroClass(HeroClassType.Wizard, 1, Outlook.Black);
                }
                else
                {
                    party.wizard = new HeroClass(HeroClassType.Wizard, 1, Outlook.White);
                }

                if (quest.selectMission == 1 ||
                   quest.selectMission == 2 ||
                   quest.selectMission == 3 ||
                   quest.selectMission == 6 ||
                   quest.selectMission == 7 ||
                   quest.selectMission == 11 ||
                   quest.selectMission == 14 ||
                   quest.selectMission == 15 ||
                   quest.selectMission == 16)
                {
                    party.cleric = new HeroClass(HeroClassType.Cleric, 1, Outlook.Black);
                }
                else
                {
                    party.cleric = new HeroClass(HeroClassType.Cleric, 1, Outlook.White);
                }

                if (quest.selectMission == 2 ||
                   quest.selectMission == 4 ||
                   quest.selectMission == 6 ||
                   quest.selectMission == 8 ||
                   quest.selectMission == 9 ||
                   quest.selectMission == 10 ||
                   quest.selectMission == 13 ||
                   quest.selectMission == 14)
                {
                    party.plut = new HeroClass(HeroClassType.Plut, 1, Outlook.Black);
                }
                else
                {
                    party.plut = new HeroClass(HeroClassType.Plut, 1, Outlook.White);
                }

                drawLevel();
                drawOutlook();

               

                addAbbilityActionAfterNewGame();

                newGameButton.IsEnabled = false;
                isStart = true;
            }
        }
        public bool deadMonsterFromAbbility = false;
        private void addAbbilityActionAfterNewGame()
        {
            if (quest.selectAbility == 1)
            {
                currentCountMagicsByAbbility = 2;
                hieghtingMagicsByAbbility();
            }
            else if (quest.selectAbility == 3)
            {
                addPotion(2);
            }
            else if (quest.selectAbility == 4)
            {
                party.isIgnoreTrap = true;
            }
            else if (quest.selectAbility == 5)
            {
                party.isIgnoreOneSkull = true;
            }
            else if (quest.selectAbility == 6)
            {
                currentCountLevelUpByAbbility = 2;
                hieghtingLevelUpByAbbility();
            }
            else if (quest.selectAbility == 7)
            {
                party.isAddMoveFromRiver = true;
            }
            else if (quest.selectAbility == 8)
            {
                party.isAddPotionFromDiamand = true;
            }
            else if (quest.selectAbility == 9)
            {
                deadMonsterFromAbbility = true;
                currentCountDeadMonstersFotArtifact = 2;
                hieghtingdeadMonsterFromArtifact();
            }
            else if (quest.selectAbility == 10)
            {
                party.isIgnoreDamageFromBoss = true;
            }
            else if (quest.selectAbility == 11)
            {
                party.isAddMagicFrom5Level = true;
            }
            else if (quest.selectAbility == 12)
            {
                party.isAddPotionFromDeadn3Monster = true;
            }
            else if (quest.selectAbility == 13)
            {
                hieghtingDiamondFromArtifact();
            }
            else if (quest.selectAbility == 14)
            {
                party.isAddPotionFromCreatePOtionByDice = true;
            }
            else if (quest.selectAbility == 15)
            {
                party.addDamageWithBosses += 2;
            }
            else if (quest.selectAbility == 16)
            {
                party.isAddMagicFromDeadn3Monster = true;
            }
        }

        //подсветка алмаза для похищения по абилке
        public void hieghtingDiamondFromArtifact()
        {

            foreach (Field field in gridFields.Children)
            {
                if (field.dto.prey != null && field.dto.prey is Diamond)
                {
                    field.yellowColor();
                }
            }

        }
        //подсветить уровни по абилке №6
        public void hieghtingLevelUpByAbbility()
        {
            if (currentCountLevelUpByAbbility > 0)
            {
                party.GetHeroes().ForEach(hero =>
                {
                    if (hero.level == 1)
                    {
                        ((CheckBox)this.FindName(hero.getPrefixControlLevelName() + 2)).IsEnabled = true;
                    }
                });
            }
        }

        public void hieghtingMagicsByAbbility()
        {
            if (currentCountMagicsByAbbility > 0)
            {
                //подсветить любой предмет
                for (int i = 1; i < 9; i++)
                {
                    PartyMagic findMagic = null;

                    foreach (PartyMagic magic in party.magics)
                    {
                        if (magic.number == i)
                        {
                            findMagic = magic;
                            break;
                        }
                    }

                    if (findMagic == null)
                    {
                        ((CheckBox)this.FindName("magic_" + i + "_" + 1)).IsEnabled = true;
                    }
                }
            }
        }


        private void rec_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            zoomRec.Opacity = 1;
            zoomRec.Fill = ((Rectangle)sender).Fill;
            Canvas.SetZIndex(zoomRec, 1);
        }

        private void zoomRec_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            zoomRec.Opacity = 1;
            zoomRec.Fill = null;
            Canvas.SetZIndex(zoomRec, 1000);
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

           
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            ScaleTransform.ScaleX = e.NewSize.Height / 765;
            ScaleTransform.ScaleY = e.NewSize.Height / 765;

        }
    }
}
