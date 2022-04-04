using DungeonPapperWPF.code;
using System.Collections.Generic;

namespace DungeonPapperWPF
{

    public partial class Boss
    {
        public string Name { get; set; }
        public int level { get; set; } = 1;
        public HeroClassType heroClassType { get; set; }
        public BossDamage firstDamage { get; set; }
        public BossDamage middleDamage { get; set; }
        public BossDamage lastDamage { get; set; }
        public Prey prey { get; set; }
        public int minusXp { get; set; }
        public List<Diamond> hideDiamonds { get; set; } = new List<Diamond>();
    }
}