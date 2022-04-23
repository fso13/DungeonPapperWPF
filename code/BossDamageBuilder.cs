namespace DungeonPapperWPF.code
{
    /// <summary>
    /// Builder for the class <see cref="BossDamage">BossDamage</see>
    /// <summary>
    public class BossDamageBuilder
    {
        private int damage;
        private int xp;
        private int heroLevel;

        /// <summary>
        /// Create a new instance for the <see cref="BossDamageBuilder">BossDamageBuilder</see>
        /// <summary>
        public BossDamageBuilder()
        {
            Reset();
        }

        /// <summary>
        /// Reset all properties' to the default value
        /// <summary>
        /// <returns>Returns the <see cref="BossDamageBuilder">BossDamageBuilder</see> with the properties reseted</returns>
        public BossDamageBuilder Reset()
        {
            damage = default;
            xp = default;
            heroLevel = default;

            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="int"/> for the property <paramref name="damage">damage</paramref>
        /// <summary>
        /// <param name="damage">A value of type <typeparamref name="int"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="BossDamageBuilder">BossDamageBuilder</see> with the property <paramref name="damage">damage</paramref> defined</returns>
        public BossDamageBuilder WithDamage(int damage)
        {
            this.damage = damage;
            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="int"/> for the property <paramref name="xp">xp</paramref>
        /// <summary>
        /// <param name="xp">A value of type <typeparamref name="int"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="BossDamageBuilder">BossDamageBuilder</see> with the property <paramref name="xp">xp</paramref> defined</returns>
        public BossDamageBuilder WithXp(int xp)
        {
            this.xp = xp;
            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="int"/> for the property <paramref name="heroLevel">heroLevel</paramref>
        /// <summary>
        /// <param name="heroLevel">A value of type <typeparamref name="int"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="BossDamageBuilder">BossDamageBuilder</see> with the property <paramref name="heroLevel">heroLevel</paramref> defined</returns>
        public BossDamageBuilder WithHeroLevel(int heroLevel)
        {
            this.heroLevel = heroLevel;
            return this;
        }

        /// <summary>
        /// Build a class of type <see cref="BossDamage">BossDamage</see> with all the defined values
        /// <summary>
        /// <returns>Returns a <see cref="BossDamage">BossDamage</see> class</returns>
        public BossDamage Build()
        {
            return new BossDamage(damage, xp, heroLevel);
        }
    }
}