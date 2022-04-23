namespace DungeonPapperWPF.code
{
    public class BossDamage
    {
        public int damage { get; set; }
        public int xp { get; set; }
        public int heroLevel { get; set; }

        public BossDamage(int damage, int xp, int heroLevel)
        {
            this.damage = damage;
            this.xp = xp;
            this.heroLevel = heroLevel;
        }
    }
}