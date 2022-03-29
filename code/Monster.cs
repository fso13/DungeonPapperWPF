using DungeonPapperWPF.code;
using System;

namespace DungeonPapperWPF
{
    public class Monster
    {
        public Monster(MonsterType monsterType, HeroClass heroClass)
        {
            this.monsterType = monsterType;
            this.heroClass = heroClass;
        }

        public MonsterType monsterType { get; set; }
        public HeroClass heroClass { get; set; }

        public Uri getPathMonster()
        {
            return new Uri(@"Resources\" + monsterType + ".png", UriKind.Relative);
        }

        public Uri getPathHeroLevel()
        {
            return heroClass.getPath();

        }
    }

    public enum MonsterType
    {
        Skelet,
        Prizrak,
        Ork,
        Goblin
    }
}