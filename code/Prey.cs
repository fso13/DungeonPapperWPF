using System;

namespace DungeonPapperWPF
{
    public interface Prey
    {

        Uri getPath();
    }

    public class Potion : Prey
    {
        public int count { get; set; } = 1;

        public Potion(int count)
        {
            this.count = count;
        }

        public Uri getPath()
        {
            return new Uri(@"Resources\Potion_"+count+".png", UriKind.Relative);
        }
    }

    public class Diamond : Prey
    {
        public string name { get; set; }

        public Diamond(string name)
        {
            this.name = name;
        }

        public Uri getPath()
        {
            return new Uri(@"Resources\Diamond_" + name.ToUpper() + ".png", UriKind.Relative);
        }
    }

    public class MagicThing : Prey
    {
        public Uri getPath()
        {
            return new Uri(@"Resources\MagicThing.png", UriKind.Relative);
        }
    }
    public class LevelUp : Prey
    {
        public Uri getPath()
        {
            return new Uri(@"Resources\LevelUp.png", UriKind.Relative);
        }
    }
}