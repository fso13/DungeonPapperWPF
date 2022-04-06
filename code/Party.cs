using System.Collections.Generic;
using System.Linq;
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

        public int xpBoss1 = 0;
        public int xpBoss2 = 0;
        public int xpBoss3 = 0;

        public List<PartyPotion> potions = new List<PartyPotion>();
        public List<PartyMagic> magics = new List<PartyMagic>();
        public List<Diamond> diamonds = new List<Diamond>();

        public List<Field> path = new List<Field>();
        public List<Monster> deadMonsters = new List<Monster>();
        public List<Boss> bosses = new List<Boss>();
        public int addDamageWithMonsters = 0;
        public int addDamageWithBosses = 0;
        public bool isIgnoreTrap;//игнорировать урон от ловушек
        public bool isIgnoreOneSkull;//игнорировать один череп на кубах
        public bool isAddMoveFromRiver;//+1 к шагу за переход через реки
        public bool isAddPotionFromDiamand;//получать зелья за алмазы
        public bool isIgnoreDamageFromBoss;//игнор урона от боссов
        public bool isAddMagicFrom5Level;//получать часть предмета за 5 уровень героя
        public bool isAddPotionFromDeadn3Monster;//получать зелье за каждого третьего убитого монстра
        public bool isAddMagicFromDeadn3Monster;//получать xfcnm ghtlvtnf за каждого третьего убитого монстра
        public bool isAddPotionFromCreatePOtionByDice;//получать зелье за варку зелья по кубику

        public List<HeroClass> GetHeroes()
        {
            return new List<HeroClass>() { warrior, wizard, cleric, plut };
        }

        public int getDamageByBoss(Boss boss)
        {
            int damage = 0;

            GetHeroes().ForEach(hero =>
            {
                damage += hero.level;
                if(boss.heroClassType == hero.type)
                {
                    damage += hero.level;
                }
            }) ;
            damage += addDamageWithBosses;
            return damage;
        }

        public void damage(int damage)
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
