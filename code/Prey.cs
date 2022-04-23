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
            return new Uri(@"Resources\Potion_" + count + ".png", UriKind.Relative);
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

    public class Xp : Prey
    {
        public Xp(int count)
        {
            this.count = count;
        }

        public int count { get; set; }

        public Uri getPath()
        {
            return null;
        }
    }

    public class LevelUp : Prey
    {
        public LevelUp()
        {
            count = 1;
        }

        public LevelUp(int count)
        {
            this.count = count;
        }

        public int count { get; set; } = 1;

        public Uri getPath()
        {
            return new Uri(@"Resources\LevelUp.png", UriKind.Relative);
        }
    }
}