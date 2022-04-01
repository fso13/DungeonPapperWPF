using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public List<PartyPotion> potions = new List<PartyPotion>();
        public List<PartyMagic> magics = new List<PartyMagic>();
        public List<Diamond> diamonds = new List<Diamond>();

        public List<Field> path = new List<Field>();


        public List<HeroClass> GetHeroes()
        {
            return new List<HeroClass>() { warrior, wizard, cleric, plut };
        }

        public void damage(int damage)
        {
            if (cleric.level < 4)
            {
                if (damage > 0)
                {
                    PartyPotion potion = getFirstPotionWithFreeCell();

                    if (potion == null)
                    {
                        blood += damage;
                    }
                    else
                    {
                        potion.freeCell--;
                        damage--;
                        this.damage(damage);
                    }
                }
            }
        }

        public void addHp()
        {
            hp++;
        }
        public void addDiamond(Diamond diamond)
        {
            diamonds.Add(diamond);
        }

        public void addPotion(int count)
        {
            for (int i = 0; i < count; i++)
            {
                potions.Add(new PartyPotion());
            }

            MessageBox.Show("Найдено лечебное зелье, в количестве: " + count);
        }

        public void addMagic(PartyMagic magic)
        {
            magics.Add(magic);
        }

        public PartyPotion getFirstPotionWithFreeCell()
        {
            for (int i = 0; i < potions.Count; i++)
            {
                if (potions.ElementAt(i).freeCell > 0)
                {
                    return potions.ElementAt(i);
                }
            }

            return null;
        }


        public bool isPresentMagic(int number)
        {
            PartyMagic pm = magics.FirstOrDefault(m => m.number == number && m.countPart == 2);
            return pm != null;
        }

    }

    public class PartyPotion
    {
        public int freeCell = 2;
    }

    public class PartyMagic
    {
        public int number;
        public int countPart;

        public PartyMagic(int number, int countPart)
        {
            this.number = number;
            this.countPart = countPart;
        }


        public void create()
        {
            countPart++;
        }
    }
}
