using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonPapperWPF.code
{
    public class BossDamage
    {
        private int damage { get; set; }
        private int xp { get; set; }
        private int heroLevel { get; set; }

        public BossDamage(int damage, int xp, int heroLevel)
        {
            this.damage = damage;
            this.xp = xp;
            this.heroLevel = heroLevel;
        }
    }
}
