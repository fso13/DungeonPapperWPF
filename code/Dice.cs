using System;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DungeonPapperWPF.code
{
    public class Dice
    {
        public Dice(DiceColor color, DiceType type, int number)
        {
            this.color = color;
            this.type = type;
            this.number = number;
        }

        public BitmapImage getPath()
        {
            return new BitmapImage(new Uri(@"Resources\dice_" + number + ".png", UriKind.Relative));
        }

        public static Dice fromNumber(int number)
        {
            switch (number)
            {
                case 0: return new Dice(DiceColor.Black, DiceType.Move3, number);
                case 1: return new Dice(DiceColor.White, DiceType.Warrior, number);
                case 2: return new Dice(DiceColor.White, DiceType.Wizard, number);
                case 3: return new Dice(DiceColor.White, DiceType.Cleric, number);
                case 4: return new Dice(DiceColor.White, DiceType.Plut, number);
                case 5: return new Dice(DiceColor.Black, DiceType.Warrior, number);
                case 6: return new Dice(DiceColor.Black, DiceType.Wizard, number);
                case 7: return new Dice(DiceColor.Black, DiceType.Cleric, number);
                case 8: return new Dice(DiceColor.Black, DiceType.Plut, number);
                case 9: return new Dice(DiceColor.White, DiceType.Klever, number);
                case 10: return new Dice(DiceColor.Black, DiceType.Scull, number);
                case 11: return new Dice(DiceColor.White, DiceType.Scull, number);
                default: return null;
            }
        }

        public int number { get; set; }
        public DiceColor color { get; set; }
        public DiceType type { get; set; }
        public Rectangle rectangle { get; set; }
    }

    public enum DiceType
    {
        Move3,
        Warrior,
        Wizard,
        Cleric,
        Plut,
        Klever,
        Scull
    }

    public enum DiceColor
    {
        White,
        Black
    }
}