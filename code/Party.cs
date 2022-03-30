using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonPapperWPF.code
{
    public class Party
    {
        public HeroClass warrior;
        public HeroClass wizard;
        public HeroClass cleric;
        public HeroClass plut;

        public int hp = 0;
        public int blood = 0;
        public int potion = 0;

        public List<Field> path = new List<Field>();


        public List<HeroClass> GetHeroes()
        {
            return new List<HeroClass>() { warrior, wizard, cleric, plut };
        }

        public void damage(int damage)
        {
            blood += damage;
        }

        public void addHp()
        {
            hp++;
        }

    }
}
