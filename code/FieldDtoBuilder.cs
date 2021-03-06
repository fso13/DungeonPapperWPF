namespace DungeonPapperWPF
{
    /// <summary>
    /// Builder for the class <see cref="FieldDto">FieldDto</see>
    /// <summary>
    public class FieldDtoBuilder
    {
        private int x;
        private int y;
        private Barrier? leftBarrier;
        private Barrier? rightBarrier;
        private Barrier? topBarrier;
        private Barrier? downBarrier;
        private bool trap;
        private Monster monster;
        private Prey prey;
        private Boss one;
        private Boss two;
        private Boss three;

        /// <summary>
        /// Create a new instance for the <see cref="FieldDtoBuilder">FieldDtoBuilder</see>
        /// <summary>
        public FieldDtoBuilder()
        {
            Reset();
        }

        /// <summary>
        /// Reset all properties' to the default value
        /// <summary>
        /// <returns>Returns the <see cref="FieldDtoBuilder">FieldDtoBuilder</see> with the properties reseted</returns>
        public FieldDtoBuilder Reset()
        {
            x = default;
            y = default;
            leftBarrier = default;
            rightBarrier = default;
            topBarrier = default;
            downBarrier = default;
            trap = default;
            monster = default;
            prey = default;
            one = default;
            two = default;
            three = default;

            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="int"/> for the property <paramref name="x">x</paramref>
        /// <summary>
        /// <param name="x">A value of type <typeparamref name="int"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="FieldDtoBuilder">FieldDtoBuilder</see> with the property <paramref name="x">x</paramref> defined</returns>
        public FieldDtoBuilder Withx(int x)
        {
            this.x = x;
            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="int"/> for the property <paramref name="y">y</paramref>
        /// <summary>
        /// <param name="y">A value of type <typeparamref name="int"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="FieldDtoBuilder">FieldDtoBuilder</see> with the property <paramref name="y">y</paramref> defined</returns>
        public FieldDtoBuilder Withy(int y)
        {
            this.y = y;
            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="Barrier?"/> for the property <paramref name="leftBarrier">leftBarrier</paramref>
        /// <summary>
        /// <param name="leftBarrier">A value of type <typeparamref name="Barrier?"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="FieldDtoBuilder">FieldDtoBuilder</see> with the property <paramref name="leftBarrier">leftBarrier</paramref> defined</returns>
        public FieldDtoBuilder WithLeftBarrier(Barrier? leftBarrier)
        {
            this.leftBarrier = leftBarrier;
            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="Barrier?"/> for the property <paramref name="rightBarrier">rightBarrier</paramref>
        /// <summary>
        /// <param name="rightBarrier">A value of type <typeparamref name="Barrier?"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="FieldDtoBuilder">FieldDtoBuilder</see> with the property <paramref name="rightBarrier">rightBarrier</paramref> defined</returns>
        public FieldDtoBuilder WithRightBarrier(Barrier? rightBarrier)
        {
            this.rightBarrier = rightBarrier;
            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="Barrier?"/> for the property <paramref name="topBarrier">topBarrier</paramref>
        /// <summary>
        /// <param name="topBarrier">A value of type <typeparamref name="Barrier?"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="FieldDtoBuilder">FieldDtoBuilder</see> with the property <paramref name="topBarrier">topBarrier</paramref> defined</returns>
        public FieldDtoBuilder WithTopBarrier(Barrier? topBarrier)
        {
            this.topBarrier = topBarrier;
            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="Barrier?"/> for the property <paramref name="downBarrier">downBarrier</paramref>
        /// <summary>
        /// <param name="downBarrier">A value of type <typeparamref name="Barrier?"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="FieldDtoBuilder">FieldDtoBuilder</see> with the property <paramref name="downBarrier">downBarrier</paramref> defined</returns>
        public FieldDtoBuilder WithDownBarrier(Barrier? downBarrier)
        {
            this.downBarrier = downBarrier;
            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="bool"/> for the property <paramref name="trap">trap</paramref>
        /// <summary>
        /// <param name="trap">A value of type <typeparamref name="bool"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="FieldDtoBuilder">FieldDtoBuilder</see> with the property <paramref name="trap">trap</paramref> defined</returns>
        public FieldDtoBuilder WithTrap(bool trap)
        {
            this.trap = trap;
            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="Monster"/> for the property <paramref name="monster">monster</paramref>
        /// <summary>
        /// <param name="monster">A value of type <typeparamref name="Monster"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="FieldDtoBuilder">FieldDtoBuilder</see> with the property <paramref name="monster">monster</paramref> defined</returns>
        public FieldDtoBuilder WithMonster(Monster monster)
        {
            this.monster = monster;
            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="Prey"/> for the property <paramref name="prey">prey</paramref>
        /// <summary>
        /// <param name="prey">A value of type <typeparamref name="Prey"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="FieldDtoBuilder">FieldDtoBuilder</see> with the property <paramref name="prey">prey</paramref> defined</returns>
        public FieldDtoBuilder WithPrey(Prey prey)
        {
            this.prey = prey;
            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="Boss"/> for the property <paramref name="one">one</paramref>
        /// <summary>
        /// <param name="one">A value of type <typeparamref name="Boss"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="FieldDtoBuilder">FieldDtoBuilder</see> with the property <paramref name="one">one</paramref> defined</returns>
        public FieldDtoBuilder WithOne(Boss one)
        {
            this.one = one;
            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="Boss"/> for the property <paramref name="two">two</paramref>
        /// <summary>
        /// <param name="two">A value of type <typeparamref name="Boss"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="FieldDtoBuilder">FieldDtoBuilder</see> with the property <paramref name="two">two</paramref> defined</returns>
        public FieldDtoBuilder WithTwo(Boss two)
        {
            this.two = two;
            return this;
        }

        /// <summary>
        /// Set a value of type <typeparamref name="Boss"/> for the property <paramref name="three">three</paramref>
        /// <summary>
        /// <param name="three">A value of type <typeparamref name="Boss"/> will the defined for the property</param>
        /// <returns>Returns the <see cref="FieldDtoBuilder">FieldDtoBuilder</see> with the property <paramref name="three">three</paramref> defined</returns>
        public FieldDtoBuilder WithThree(Boss three)
        {
            this.three = three;
            return this;
        }

        /// <summary>
        /// Build a class of type <see cref="FieldDto">FieldDto</see> with all the defined values
        /// <summary>
        /// <returns>Returns a <see cref="FieldDto">FieldDto</see> class</returns>
        public FieldDto Build()
        {
            return new FieldDto
            {
                x = x,
                y = y,
                leftBarrier = leftBarrier,
                rightBarrier = rightBarrier,
                topBarrier = topBarrier,
                downBarrier = downBarrier,
                trap = trap,
                monster = monster,
                prey = prey,
                one = one,
                two = two,
                three = three
            };
        }
    }
}