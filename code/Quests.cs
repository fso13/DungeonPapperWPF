using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonPapperWPF.code
{
    public class Quests
    {
        //минотавр, вампирша, страж
        static List<FieldDto> quest1 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(4).Withy(2).WithDownBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(4).Withy(3).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).Build(),

            new FieldDtoBuilder().Withx(1).Withy(2).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(1).Withy(3).WithRightBarrier(Barrier.Black).Build(),

            new FieldDtoBuilder().Withx(0).Withy(5).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(1).Withy(6).WithTopBarrier(Barrier.Black).Build(),

        };

        //минотавр, химера, страж
        static List<FieldDto> quest2 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(3).Withy(2).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(4).Withy(2).WithTopBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(0).Withy(3).WithTopBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(1).Withy(3).WithTopBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(2).Withy(5).WithTopBarrier(Barrier.Black).WithRightBarrier(Barrier.Black).Build(),

        };

        //голем,вампирша,страж
        static List<FieldDto> quest3 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(2).Withy(1).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(3).Withy(2).WithTopBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(0).Withy(3).WithTopBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(1).Withy(3).WithTopBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(3).Withy(4).WithRightBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(3).Withy(5).WithTopBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(4).Withy(5).WithTopBarrier(Barrier.Black).Build(),

        };


        //мумия, великан, гидра
        static List<FieldDto> quest4 = new List<FieldDto>() {
            new FieldDtoBuilder().Withx(3).Withy(1).WithRightBarrier(Barrier.Black).Build(),
            new FieldDtoBuilder().Withx(4).Withy(2).WithTopBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(3).Withy(3).WithRightBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(3).Withy(4).WithRightBarrier(Barrier.Black).Build(),

           new FieldDtoBuilder().Withx(2).Withy(5).WithRightBarrier(Barrier.Black).Build(),
           new FieldDtoBuilder().Withx(2).Withy(6).WithRightBarrier(Barrier.Black).WithTopBarrier(Barrier.Black).Build(),

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

            });

            return q1;
        }




        static FieldDto[,] getDefault()
        {
            FieldDto[,] fieldDtos = new FieldDto[,]
      {

      //7
            {
                new FieldDtoBuilder().WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).WithPrey(new Potion(1)).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.Wall).WithTrap(true).WithPrey(new Diamond("G")).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).WithTrap(true).WithPrey(new LevelUp()).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.Wall).WithPrey(new Diamond("H")).WithMonster(new Monster(MonsterType.Skelet, new HeroClass(HeroClassType.Cleric,5))).Build()
            },

//6
        {
                new FieldDtoBuilder().WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.River).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).WithTrap(true).WithPrey(new MagicThing()).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithTrap(true).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.River).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithPrey(new Diamond("F")).WithMonster(new Monster(MonsterType.Ork, new HeroClass(HeroClassType.Warrior,5))).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.River).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).WithTrap(true).WithPrey(new MagicThing()).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).WithTrap(true).Build()
                },

        //5
        {
                new FieldDtoBuilder().WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.River).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.River).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.River).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.River).Build()
            },

        //4
        {
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.River).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.Wall).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.River).WithDownBarrier(Barrier.None).Build()
            },

        //3
        {
                new FieldDtoBuilder().WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.River).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.River).Build()
            },

        //2
        {
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.River).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.Wall).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.River).WithDownBarrier(Barrier.None).Build()
            },

        //1
        {
                new FieldDtoBuilder().WithLeftBarrier(Barrier.Wall).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.River).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.Wall).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.None).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build(),
                new FieldDtoBuilder().WithLeftBarrier(Barrier.None).WithRightBarrier(Barrier.Wall).WithTopBarrier(Barrier.None).WithDownBarrier(Barrier.None).Build()
          }
      };
            return fieldDtos;
        }

    }
}
