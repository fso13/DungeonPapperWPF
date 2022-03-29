using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonPapperWPF.code
{
    public class HeroClass
    {
        public HeroClass(HeroClassType type, int level)
        {
            this.type = type;
            this.level = level;
        }

        public HeroClassType type { get; set; }
        public int level { get; set; } = 1;

        public Uri getPath()
        {
            return new Uri(@"Resources\" + type +"_"+ level + ".png", UriKind.Relative);
        }
    }

    public enum HeroClassType
    {
        Warrior,
        Wizard,
        Plut,
        Cleric
    }
}
