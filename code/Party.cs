﻿using System;
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

        public List<Field> path = new List<Field>();


        public List<HeroClass> GetHeroes()
        {
            return new List<HeroClass>() { warrior, wizard, cleric, plut };
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

        public void addPotion(int count)
        {
            for (int i = 0; i < count; i++)
            {
                potions.Add(new PartyPotion());
            }

            MessageBox.Show("Найдено лечебное зелье, в количестве: " + count);
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

    }

    public class PartyPotion
    {
        public int freeCell = 2;
    }
}