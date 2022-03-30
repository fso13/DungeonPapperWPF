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

        public HeroClass(HeroClassType type, int level, Outlook outlook) : this(type, level)
        {
            this.outlook = outlook;
        }

        public string getPrefixControlLevelName()
        {

            return "cbox_" + type.ToString().ToLower() + "Level";
        }


        public string getControlOutlookName()
        {
            return "cbox_" + type.ToString().ToLower() + "BlackWhite";
        }

        public int getNumberDiceForLevel()
        {
            if (outlook == Outlook.White && type==HeroClassType.Warrior)
            {
                return 1;
            }
            if (outlook == Outlook.White && type == HeroClassType.Wizard)
            {
                return 2;
            }
            if (outlook == Outlook.White && type == HeroClassType.Cleric)
            {
                return 3;
            }
            if (outlook == Outlook.White && type == HeroClassType.Plut)
            {
                return 4;
            }
            if (outlook == Outlook.Black && type == HeroClassType.Warrior)
            {
                return 5;
            }
            if (outlook == Outlook.Black && type == HeroClassType.Wizard)
            {
                return 6;
            }
            if (outlook == Outlook.Black && type == HeroClassType.Cleric)
            {
                return 7;
            }
            if (outlook == Outlook.Black && type == HeroClassType.Plut)
            {
                return 8;
            }

            return 0;
        }

        public HeroClassType type { get; set; }
        public int level { get; set; } = 1;
        public Outlook outlook { get; set; }

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

    public enum Outlook
    {
        Black,
        White
    }
}
