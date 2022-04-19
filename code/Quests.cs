using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonPapperWPF.code
{
    public class Quests
    {
        public class Quest
        {
            public Quest(int questNumber, string text, List<Boss> bosses)
            {
                this.questNumber = questNumber;
                this.text = text;
                this.bosses = bosses;
            }

            public int questNumber { get; set; }
            public string text { get; set; }
            public int selectAbility { get; set; }
            public int selectMission { get; set; }
            public List<int> missions { get; set; }
            public List<Boss> bosses { get; set; }


            public int roundMissionComplete1 = -1;
            public int roundMissionComplete2 = -1;
            public int roundMissionComplete3 = -1;
        }

        public static Boss STRAG = new BossBuilder().WithName("Страж")
            .WithNumber(12)
          .WithLevel(3)
          .WithHeroClassType(HeroClassType.Wizard)
            .WithPrey(new Xp(2))
            .WithMinusXp(5)
            .WithFirstDamage(new BossDamageBuilder()
                .WithXp(4)
                .WithDamage(5)
                .WithHeroLevel(18)
                .Build())
            .WithMiddleDamage(new BossDamageBuilder()
                .WithXp(6)
                .WithDamage(4)
                .WithHeroLevel(23)
                .Build())
            .WithLastDamage(new BossDamageBuilder()
                .WithXp(8)
                .WithDamage(3)
                .WithHeroLevel(28)
                .Build())
          .Build();

        public static Boss VAMP = new BossBuilder().WithName("Вампирша")
            .WithNumber(5)
          .WithLevel(2)
          .WithHeroClassType(HeroClassType.Cleric)
            .WithPrey(new Xp(2))
            .WithMinusXp(4)
            .WithFirstDamage(new BossDamageBuilder()
                .WithXp(3)
                .WithDamage(4)
                .WithHeroLevel(14)
                .Build())
            .WithMiddleDamage(new BossDamageBuilder()
                .WithXp(5)
                .WithDamage(3)
                .WithHeroLevel(18)
                .Build())
            .WithLastDamage(new BossDamageBuilder()
                .WithXp(7)
                .WithDamage(2)
                .WithHeroLevel(22)
                .Build())
            .WithHideDiamonds(new List<Diamond>() { new Diamond("F"), new Diamond("G") })
          .Build();

        public static Boss MINOTAVR = new BossBuilder().WithName("Минотавр")
            .WithNumber(2)
          .WithLevel(1)
          .WithHeroClassType(HeroClassType.Warrior)
            .WithPrey(new LevelUp())
            .WithMinusXp(3)
            .WithFirstDamage(new BossDamageBuilder()
                .WithXp(2)
                .WithDamage(3)
                .WithHeroLevel(9)
                .Build())
            .WithMiddleDamage(new BossDamageBuilder()
                .WithXp(4)
                .WithDamage(2)
                .WithHeroLevel(13)
                .Build())
            .WithLastDamage(new BossDamageBuilder()
                .WithXp(6)
                .WithDamage(1)
                .WithHeroLevel(17)
                .Build())
            .WithHideDiamonds(new List<Diamond>() { new Diamond("A"), new Diamond("D") })
          .Build();

        //минотавр, вампирша, страж
        static List<FieldDto> quest1 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(0).Withy(1).WithThree(STRAG).Build(),
            new FieldDtoBuilder().Withx(3).Withy(5).WithOne(MINOTAVR).Build(),

            new FieldDtoBuilder().Withx(4).Withy(2).WithDownBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).WithTwo(VAMP).Build(),
            new FieldDtoBuilder().Withx(4).Withy(3).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).Build(),

            new FieldDtoBuilder().Withx(1).Withy(2).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(1).Withy(3).WithRightBarrier(Barrier.Black).Build(),

            new FieldDtoBuilder().Withx(0).Withy(5).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(1).Withy(6).WithTopBarrier(Barrier.Black).Build(),

        };

        public static Boss HIMERA = new BossBuilder().WithName("Химера")
        .WithNumber(8)
      .WithLevel(2)
      .WithHeroClassType(HeroClassType.Plut)
        .WithPrey(new MagicThing())
        .WithMinusXp(4)
        .WithFirstDamage(new BossDamageBuilder()
            .WithXp(3)
            .WithDamage(4)
            .WithHeroLevel(14)
            .Build())
        .WithMiddleDamage(new BossDamageBuilder()
            .WithXp(5)
            .WithDamage(3)
            .WithHeroLevel(18)
            .Build())
        .WithLastDamage(new BossDamageBuilder()
            .WithXp(7)
            .WithDamage(2)
            .WithHeroLevel(22)
            .Build())
        .WithHideDiamonds(new List<Diamond>() { new Diamond("E"), new Diamond("H") })
      .Build();

        //минотавр, химера, страж
        static List<FieldDto> quest2 = new List<FieldDto>() {

            new FieldDtoBuilder().Withx(4).Withy(0).WithThree(STRAG).Build(),
            new FieldDtoBuilder().Withx(3).Withy(5).WithOne(MINOTAVR).Build(),

            new FieldDtoBuilder().Withx(3).Withy(2).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(4).Withy(2).WithTopBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(0).Withy(3).WithTopBarrier(Barrier.Black).WithTwo(HIMERA).Build(),
           new FieldDtoBuilder().Withx(1).Withy(3).WithTopBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(2).Withy(5).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).Build(),

        };

        public static Boss GOLEM = new BossBuilder().WithName("Голем")
                                            .WithNumber(3)
                                            .WithLevel(1)
                                            .WithHeroClassType(HeroClassType.Plut)
                                            .WithPrey(new Potion(2))
                                            .WithMinusXp(3)
                                            .WithFirstDamage(new BossDamageBuilder()
                                                .WithXp(2)
                                                .WithDamage(3)
                                                .WithHeroLevel(9)
                                                .Build())
                                            .WithMiddleDamage(new BossDamageBuilder()
                                                .WithXp(4)
                                                .WithDamage(2)
                                                .WithHeroLevel(13)
                                                .Build())
                                            .WithLastDamage(new BossDamageBuilder()
                                                .WithXp(6)
                                                .WithDamage(1)
                                                .WithHeroLevel(17)
                                                .Build())
                                            .WithHideDiamonds(new List<Diamond>() { new Diamond("B"), new Diamond("D") })
                                            .Build();

        //голем,вампирша,страж
        static List<FieldDto> quest3 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(3).Withy(3).WithTwo(VAMP).Build(),
            new FieldDtoBuilder().Withx(0).Withy(1).WithThree(STRAG).Build(),

            new FieldDtoBuilder().Withx(2).Withy(1).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(3).Withy(2).WithTopBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(0).Withy(3).WithTopBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(1).Withy(3).WithTopBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(3).Withy(4).WithRightBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(3).Withy(5).WithTopBarrier(Barrier.Black).WithOne(GOLEM).Build(),
           new FieldDtoBuilder().Withx(4).Withy(5).WithTopBarrier(Barrier.Black).Build(),

        };
        public static Boss GIDRA = new BossBuilder().WithName("Гидра")
            .WithNumber(10)
          .WithLevel(3)
          .WithHeroClassType(HeroClassType.Plut)
            .WithPrey(new Xp(2))
            .WithMinusXp(5)
            .WithFirstDamage(new BossDamageBuilder()
                .WithXp(4)
                .WithDamage(5)
                .WithHeroLevel(18)
                .Build())
            .WithMiddleDamage(new BossDamageBuilder()
                .WithXp(6)
                .WithDamage(4)
                .WithHeroLevel(23)
                .Build())
            .WithLastDamage(new BossDamageBuilder()
                .WithXp(8)
                .WithDamage(3)
                .WithHeroLevel(28)
                .Build())
          .Build();

        public static Boss VELIKAN = new BossBuilder().WithName("Великан")
            .WithNumber(7)
          .WithLevel(2)
          .WithHeroClassType(HeroClassType.Warrior)
            .WithPrey(new LevelUp())
            .WithMinusXp(4)
            .WithFirstDamage(new BossDamageBuilder()
                .WithXp(3)
                .WithDamage(4)
                .WithHeroLevel(14)
                .Build())
            .WithMiddleDamage(new BossDamageBuilder()
                .WithXp(5)
                .WithDamage(3)
                .WithHeroLevel(18)
                .Build())
            .WithLastDamage(new BossDamageBuilder()
                .WithXp(7)
                .WithDamage(2)
                .WithHeroLevel(22)
                .Build())
            .WithHideDiamonds(new List<Diamond>() { new Diamond("E"), new Diamond("F") })
          .Build();

        public static Boss MUMIA = new BossBuilder().WithName("Мумия")
            .WithNumber(1)
          .WithLevel(1)
          .WithHeroClassType(HeroClassType.Cleric)
            .WithPrey(new Xp(2))
            .WithMinusXp(3)
            .WithFirstDamage(new BossDamageBuilder()
                .WithXp(2)
                .WithDamage(3)
                .WithHeroLevel(9)
                .Build())
            .WithMiddleDamage(new BossDamageBuilder()
                .WithXp(4)
                .WithDamage(2)
                .WithHeroLevel(13)
                .Build())
            .WithLastDamage(new BossDamageBuilder()
                .WithXp(6)
                .WithDamage(1)
                .WithHeroLevel(17)
                .Build())
            .WithHideDiamonds(new List<Diamond>() { new Diamond("B"), new Diamond("C") })
          .Build();

        //мумия, великан, гидра
        static List<FieldDto> quest4 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(1).Withy(5).WithOne(MUMIA).Build(),
            new FieldDtoBuilder().Withx(4).Withy(0).WithThree(GIDRA).Build(),

            new FieldDtoBuilder().Withx(3).Withy(1).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(4).Withy(2).WithTopBarrier(Barrier.Black).WithTwo(VELIKAN).Build(),

           new FieldDtoBuilder().Withx(3).Withy(3).WithRightBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(3).Withy(4).WithRightBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(2).Withy(5).WithRightBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(2).Withy(6).WithRightBarrier(Barrier.Black).WithTopBarrier(Barrier.Black).Build(),

        };

        public static Boss DRAKON = new BossBuilder().WithName("Дракон")
                           .WithNumber(9)
                          .WithLevel(3)
                          .WithHeroClassType(HeroClassType.Warrior)
                            .WithPrey(new Xp(2))
                            .WithMinusXp(5)
                            .WithFirstDamage(new BossDamageBuilder()
                                .WithXp(4)
                                .WithDamage(5)
                                .WithHeroLevel(18)
                                .Build())
                            .WithMiddleDamage(new BossDamageBuilder()
                                .WithXp(6)
                                .WithDamage(4)
                                .WithHeroLevel(23)
                                .Build())
                            .WithLastDamage(new BossDamageBuilder()
                                .WithXp(8)
                                .WithDamage(3)
                                .WithHeroLevel(28)
                                .Build())
                          .Build();

        public static Boss LICH = new BossBuilder().WithName("Лич")
    .WithNumber(11)
  .WithLevel(3)
  .WithHeroClassType(HeroClassType.Cleric)
    .WithPrey(new Xp(2))
    .WithMinusXp(5)
    .WithFirstDamage(new BossDamageBuilder()
        .WithXp(4)
        .WithDamage(5)
        .WithHeroLevel(18)
        .Build())
    .WithMiddleDamage(new BossDamageBuilder()
        .WithXp(6)
        .WithDamage(4)
        .WithHeroLevel(23)
        .Build())
    .WithLastDamage(new BossDamageBuilder()
        .WithXp(8)
        .WithDamage(3)
        .WithHeroLevel(28)
        .Build())
  .Build();

        public static Boss ELEMENTAL = new BossBuilder().WithName("Элементаль")
            .WithNumber(6)
          .WithLevel(2)
          .WithHeroClassType(HeroClassType.Wizard)
            .WithPrey(new Potion(2))
            .WithMinusXp(4)
            .WithFirstDamage(new BossDamageBuilder()
                .WithXp(3)
                .WithDamage(4)
                .WithHeroLevel(14)
                .Build())
            .WithMiddleDamage(new BossDamageBuilder()
                .WithXp(5)
                .WithDamage(3)
                .WithHeroLevel(18)
                .Build())
            .WithLastDamage(new BossDamageBuilder()
                .WithXp(7)
                .WithDamage(2)
                .WithHeroLevel(22)
                .Build())
            .WithHideDiamonds(new List<Diamond>() { new Diamond("G"), new Diamond("H") })
          .Build();

        public static Boss GARGULIA = new BossBuilder().WithName("Гаргулья")
            .WithNumber(4)
          .WithLevel(1)
          .WithHeroClassType(HeroClassType.Wizard)
            .WithPrey(new MagicThing())
            .WithMinusXp(3)
            .WithFirstDamage(new BossDamageBuilder()
                .WithXp(2)
                .WithDamage(3)
                .WithHeroLevel(9)
                .Build())
            .WithMiddleDamage(new BossDamageBuilder()
                .WithXp(4)
                .WithDamage(2)
                .WithHeroLevel(13)
                .Build())
            .WithLastDamage(new BossDamageBuilder()
                .WithXp(6)
                .WithDamage(1)
                .WithHeroLevel(17)
                .Build())
            .WithHideDiamonds(new List<Diamond>() { new Diamond("A"), new Diamond("C") })
          .Build();



        //мумия, элементать, гидра
        static List<FieldDto> quest5 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(4).Withy(4).WithOne(MUMIA).Build(),
            new FieldDtoBuilder().Withx(2).Withy(0).WithThree(GIDRA).Build(),

            new FieldDtoBuilder().Withx(2).Withy(1).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(3).Withy(1).WithTopBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(0).Withy(3).WithTwo(ELEMENTAL).WithTopBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(1).Withy(3).WithTopBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(3).Withy(4).WithRightBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(3).Withy(5).WithTopBarrier(Barrier.Black).Build(),

        };


        //мумия, химера, дракон
        static List<FieldDto> quest6 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(4).Withy(4).WithOne(MUMIA).Build(),
            new FieldDtoBuilder().Withx(2).Withy(0).WithThree(DRAKON).Build(),

            new FieldDtoBuilder().Withx(3).Withy(0).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(3).Withy(1).WithTopBarrier(Barrier.Black).Build(),

            new FieldDtoBuilder().Withx(3).Withy(2).WithTopBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(4).Withy(2).WithTwo(HIMERA).WithTopBarrier(Barrier.Black).Build(),

            new FieldDtoBuilder().Withx(3).Withy(3).WithRightBarrier(Barrier.Black).WithTopBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(4).Withy(3).WithTopBarrier(Barrier.Black).Build(),

        };

        //гаргулья, химера, лич
        static List<FieldDto> quest7 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(4).Withy(4).WithOne(GARGULIA).WithTopBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(2).Withy(0).WithThree(LICH).Build(),
           new FieldDtoBuilder().Withx(0).Withy(3).WithTwo(HIMERA).WithTopBarrier(Barrier.Black).Build(),

            new FieldDtoBuilder().Withx(1).Withy(2).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(5).Withy(4).WithTopBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(4).Withy(3).WithRightBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(0).Withy(5).WithTopBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(1).Withy(5).WithTopBarrier(Barrier.Black).Build(),

        };

        //гаргулья, великан, лич
        static List<FieldDto> quest8 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(0).Withy(1).WithThree(LICH).Build(),
           new FieldDtoBuilder().Withx(0).Withy(3).WithTwo(VELIKAN).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(0).Withy(2).WithTopBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(1).Withy(2).WithTopBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(3).Withy(4).WithRightBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(3).Withy(5).WithRightBarrier(Barrier.Black).WithOne(GARGULIA).WithTopBarrier(Barrier.Black).Build(),
        };



        //гаргулья, великан, гидра
        static List<FieldDto> quest9 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(2).Withy(0).WithThree(LICH).Build(),

            new FieldDtoBuilder().Withx(2).Withy(1).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(2).Withy(2).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).Build(),

            new FieldDtoBuilder().Withx(3).Withy(3).WithTwo(VELIKAN).Build(),

            new FieldDtoBuilder().Withx(3).Withy(4).WithTopBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(4).Withy(4).WithTopBarrier(Barrier.Black).Build(),

            new FieldDtoBuilder().Withx(1).Withy(5).WithRightBarrier(Barrier.Black).WithOne(GARGULIA).Build(),
            new FieldDtoBuilder().Withx(1).Withy(6).WithTopBarrier(Barrier.Black).Build(),

        };


        //голем, элементаль, дракон
        static List<FieldDto> quest10 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(4).Withy(0).WithThree(DRAKON).Build(),

            new FieldDtoBuilder().Withx(3).Withy(1).WithTopBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(4).Withy(1).WithTopBarrier(Barrier.Black).Build(),

            new FieldDtoBuilder().Withx(4).Withy(2).WithTwo(ELEMENTAL).Build(),

            new FieldDtoBuilder().Withx(4).Withy(3).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).Build(),


            new FieldDtoBuilder().Withx(3).Withy(4).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(4).Withy(4).WithOne(GOLEM).Build(),

            new FieldDtoBuilder().Withx(3).Withy(5).WithTopBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(4).Withy(5).WithTopBarrier(Barrier.Black).Build(),


        };


        //голем, вампирша, дракон
        static List<FieldDto> quest11 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(2).Withy(0).WithThree(DRAKON).Build(),
            new FieldDtoBuilder().Withx(2).Withy(1).WithTopBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(3).Withy(1).WithTopBarrier(Barrier.Black).Build(),

            new FieldDtoBuilder().Withx(3).Withy(3).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).WithTwo(VAMP).Build(),
            new FieldDtoBuilder().Withx(4).Withy(3).WithTopBarrier(Barrier.Black).Build(),

            new FieldDtoBuilder().Withx(1).Withy(5).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).WithOne(GOLEM).Build(),
        };


        //минотавр, элементаль, лич
        static List<FieldDto> quest12 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(0).Withy(1).WithThree(LICH).Build(),

            new FieldDtoBuilder().Withx(2).Withy(3).WithTopBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(3).Withy(3).WithTopBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(4).Withy(2).WithTwo(ELEMENTAL).Build(),

            new FieldDtoBuilder().Withx(4).Withy(4).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).WithOne(MINOTAVR).Build(),

            new FieldDtoBuilder().Withx(3).Withy(5).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(3).Withy(6).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(4).Withy(6).WithTopBarrier(Barrier.Black).Build(),

        };


        public static FieldDto[,] getQuest(int index)
        {
            switch (index)
            {
                case 0: return getDefault();
                case 1: return merge(quest1);
                case 2: return merge(quest2);
                case 3: return merge(quest3);
                case 4: return merge(quest4);
                case 5: return merge(quest5);
                case 6: return merge(quest6);
                case 7: return merge(quest7);
                case 8: return merge(quest8);
                case 9: return merge(quest9);
                case 10: return merge(quest10);
                case 11: return merge(quest11);
                case 12: return merge(quest12);
                default: return getDefault();
            }

        }

        static FieldDto[,] merge(List<FieldDto> quest)
        {
            FieldDto[,] q1 = new FieldDto[7, 6];

            Array.Copy(getDefault(), 0, q1, 0, getDefault().Length);

            quest.ForEach(f =>
            {
                FieldDto dto = q1[f.y, f.x];

                if (f.leftBarrier != null)
                {
                    dto.leftBarrier = f.leftBarrier;
                }

                if (f.rightBarrier != null)
                {
                    dto.rightBarrier = f.rightBarrier;
                }

                if (f.topBarrier != null)
                {
                    dto.topBarrier = f.topBarrier;
                }

                if (f.downBarrier != null)
                {
                    dto.downBarrier = f.downBarrier;
                }

                if (f.one != null)
                {
                    dto.one = f.one;
                }
                if (f.two != null)
                {
                    dto.two = f.two;
                }
                if (f.three != null)
                {
                    dto.three = f.three;
                }

            });

            return q1;
        }




        static FieldDto[,] getDefault()
        {
            FieldDto[,] fieldDtos = new FieldDto[,]
      {

      //7
            {
                new FieldDtoBuilder().Withx(0).Withy(0).WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).WithPrey(new Potion(1)).Build(),
                new FieldDtoBuilder().Withx(1).Withy(0).WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.Wall).WithTrap(true).WithPrey(new Diamond("G")).Build(),
                new FieldDtoBuilder().Withx(2).Withy(0).WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().Withx(3).Withy(0).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).WithTrap(true).WithPrey(new LevelUp()).Build(),
                new FieldDtoBuilder().Withx(4).Withy(0).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().Withx(5).Withy(0).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.Wall).WithPrey(new Diamond("H")).WithMonster(new Monster(MonsterType.Skelet, new HeroClass(HeroClassType.Cleric,5))).Build()
            },

//6
        {
                new FieldDtoBuilder().Withx(0).Withy(1).WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.River).Build(),
                new FieldDtoBuilder().Withx(1).Withy(1).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).WithTrap(true).WithPrey(new MagicThing()).Build(),
                new FieldDtoBuilder().Withx(2).Withy(1).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithTrap(true).Build(),
                new FieldDtoBuilder().Withx(3).Withy(1).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.River).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithPrey(new Diamond("F")).WithMonster(new Monster(MonsterType.Ork, new HeroClass(HeroClassType.Warrior,5))).Build(),
                new FieldDtoBuilder().Withx(4).Withy(1).WithLeftBarrier(Barrier.River).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithTrap(true).WithPrey(new MagicThing()).Build(),
                new FieldDtoBuilder().Withx(5).Withy(1).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).WithTrap(true).Build()
                },

        //5
        {
                new FieldDtoBuilder().Withx(0).Withy(2).WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.River).WithDownBarrier(Barrier.None).WithPrey(new Diamond("D")).WithMonster(new Monster(MonsterType.Prizrak, new HeroClass(HeroClassType.Wizard,5))).Build(),
                new FieldDtoBuilder().Withx(1).Withy(2).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithTrap(true).Build(),
                new FieldDtoBuilder().Withx(2).Withy(2).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithTrap(true).Build(),
                new FieldDtoBuilder().Withx(3).Withy(2).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithTrap(true).WithPrey(new Potion(1)).Build(),
                new FieldDtoBuilder().Withx(4).Withy(2).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.River).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().Withx(5).Withy(2).WithLeftBarrier(Barrier.River).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.River).WithPrey(new Diamond("E")).WithMonster(new Monster(MonsterType.Goblin, new HeroClass(HeroClassType.Plut,5))).Build()
            },

        //4
        {
                new FieldDtoBuilder().Withx(0).Withy(3).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.River).Build(),
                new FieldDtoBuilder().Withx(1).Withy(3).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithPrey(new MagicThing()).Build(),
                new FieldDtoBuilder().Withx(2).Withy(3).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.Wall).WithPrey(new Diamond("C")).WithMonster(new Monster(MonsterType.Goblin, new HeroClass(HeroClassType.Plut,4))).Build(),
                new FieldDtoBuilder().Withx(3).Withy(3).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().Withx(4).Withy(3).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithPrey(new Potion(1)).WithMonster(new Monster(MonsterType.Prizrak, new HeroClass(HeroClassType.Wizard,4))).Build(),
                new FieldDtoBuilder().Withx(5).Withy(3).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.River).WithDownBarrier(Barrier.None).WithTrap(true).WithPrey(new LevelUp()).Build()
            },

        //3
        {
                new FieldDtoBuilder().Withx(0).Withy(4).WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.River).WithDownBarrier(Barrier.None).WithTrap(true).WithPrey(new LevelUp()).Build(),
                new FieldDtoBuilder().Withx(1).Withy(4).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithTrap(true).Build(),
                new FieldDtoBuilder().Withx(2).Withy(4).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).WithPrey(new Potion(1)).Build(),
                new FieldDtoBuilder().Withx(3).Withy(4).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithTrap(true).WithPrey(new LevelUp()).Build(),
                new FieldDtoBuilder().Withx(4).Withy(4).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().Withx(5).Withy(4).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.River).WithPrey(new Diamond("B")).WithMonster(new Monster(MonsterType.Ork, new HeroClass(HeroClassType.Warrior,4))).Build()
            },

        //2
        {
                new FieldDtoBuilder().Withx(0).Withy(5).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.River).WithPrey(new Diamond("A")).WithMonster(new Monster(MonsterType.Skelet, new HeroClass(HeroClassType.Cleric,4))).Build(),
                new FieldDtoBuilder().Withx(1).Withy(5).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().Withx(2).Withy(5).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithTrap(true).Build(),
                new FieldDtoBuilder().Withx(3).Withy(5).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.Wall).Build(),
                new FieldDtoBuilder().Withx(4).Withy(5).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithPrey(new LevelUp()).WithMonster(new Monster(MonsterType.Skelet, new HeroClass(HeroClassType.Cleric,3))).Build(),
                new FieldDtoBuilder().Withx(5).Withy(5).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.River).WithDownBarrier(Barrier.None).WithTrap(true).WithPrey(new MagicThing()).Build()
            },

        //1
        {
                new FieldDtoBuilder().Withx(0).Withy(6).WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.River).WithDownBarrier(Barrier.None).WithPrey(new Potion(2)).WithMonster(new Monster(MonsterType.Prizrak, new HeroClass(HeroClassType.Wizard,3))).Build(),
                new FieldDtoBuilder().Withx(1).Withy(6).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithPrey(new MagicThing()).WithMonster(new Monster(MonsterType.Ork, new HeroClass(HeroClassType.Warrior,3))).Build(),
                new FieldDtoBuilder().Withx(2).Withy(6).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithPrey(new Potion(1)).Build(),
                new FieldDtoBuilder().Withx(3).Withy(6).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).WithPrey(new Potion(1)).WithMonster(new Monster(MonsterType.Goblin, new HeroClass(HeroClassType.Plut,3))).Build(),
                new FieldDtoBuilder().Withx(4).Withy(6).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithPrey(new MagicThing()).WithTrap(true).Build(),
                new FieldDtoBuilder().Withx(5).Withy(6).WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithPrey(new Potion(1)).Build()
          }
      };
            return fieldDtos;
        }

    }
}
