using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonPapperWPF.code;
using DungeonPapperWPF;

namespace DungeonPapperWPF
{
	/// <summary>
	/// Builder for the class <see cref="Boss">Boss</see>
	/// <summary>
	public class BossBuilder
	{
		private int number;
		private string name;
		private int level;
		private HeroClassType heroClassType;
		private BossDamage firstDamage;
		private BossDamage middleDamage;
		private BossDamage lastDamage;
		private Prey prey;
		private int minusXp;
		private List<Diamond> hideDiamonds;

		/// <summary>
		/// Create a new instance for the <see cref="BossBuilder">BossBuilder</see>
		/// <summary>
		public BossBuilder()
		{
			Reset();
		}

		/// <summary>
		/// Reset all properties' to the default value
		/// <summary>
		/// <returns>Returns the <see cref="BossBuilder">BossBuilder</see> with the properties reseted</returns>
		public BossBuilder Reset()
		{
			number = default;
			name = default;
			level = default;
			heroClassType = default;
			firstDamage = default;
			middleDamage = default;
			lastDamage = default;
			prey = default;
			minusXp = default;
			hideDiamonds = new List<Diamond>();

			return this;
		}

		/// <summary>
		/// Set a value of type <typeparamref name="int"/> for the property <paramref name="number">number</paramref>
		/// <summary>
		/// <param name="number">A value of type <typeparamref name="int"/> will the defined for the property</param>
		/// <returns>Returns the <see cref="BossBuilder">BossBuilder</see> with the property <paramref name="number">number</paramref> defined</returns>
		public BossBuilder WithNumber(int number)
		{
			this.number = number;
			return this;
		}

		/// <summary>
		/// Set a value of type <typeparamref name="string"/> for the property <paramref name="name">name</paramref>
		/// <summary>
		/// <param name="name">A value of type <typeparamref name="string"/> will the defined for the property</param>
		/// <returns>Returns the <see cref="BossBuilder">BossBuilder</see> with the property <paramref name="name">name</paramref> defined</returns>
		public BossBuilder WithName(string name)
		{
			this.name = name;
			return this;
		}

		/// <summary>
		/// Set a value of type <typeparamref name="int"/> for the property <paramref name="level">level</paramref>
		/// <summary>
		/// <param name="level">A value of type <typeparamref name="int"/> will the defined for the property</param>
		/// <returns>Returns the <see cref="BossBuilder">BossBuilder</see> with the property <paramref name="level">level</paramref> defined</returns>
		public BossBuilder WithLevel(int level)
		{
			this.level = level;
			return this;
		}

		/// <summary>
		/// Set a value of type <typeparamref name="HeroClassType"/> for the property <paramref name="heroClassType">heroClassType</paramref>
		/// <summary>
		/// <param name="heroClassType">A value of type <typeparamref name="HeroClassType"/> will the defined for the property</param>
		/// <returns>Returns the <see cref="BossBuilder">BossBuilder</see> with the property <paramref name="heroClassType">heroClassType</paramref> defined</returns>
		public BossBuilder WithHeroClassType(HeroClassType heroClassType)
		{
			this.heroClassType = heroClassType;
			return this;
		}

		/// <summary>
		/// Set a value of type <typeparamref name="BossDamage"/> for the property <paramref name="firstDamage">firstDamage</paramref>
		/// <summary>
		/// <param name="firstDamage">A value of type <typeparamref name="BossDamage"/> will the defined for the property</param>
		/// <returns>Returns the <see cref="BossBuilder">BossBuilder</see> with the property <paramref name="firstDamage">firstDamage</paramref> defined</returns>
		public BossBuilder WithFirstDamage(BossDamage firstDamage)
		{
			this.firstDamage = firstDamage;
			return this;
		}

		/// <summary>
		/// Set a value of type <typeparamref name="BossDamage"/> for the property <paramref name="middleDamage">middleDamage</paramref>
		/// <summary>
		/// <param name="middleDamage">A value of type <typeparamref name="BossDamage"/> will the defined for the property</param>
		/// <returns>Returns the <see cref="BossBuilder">BossBuilder</see> with the property <paramref name="middleDamage">middleDamage</paramref> defined</returns>
		public BossBuilder WithMiddleDamage(BossDamage middleDamage)
		{
			this.middleDamage = middleDamage;
			return this;
		}

		/// <summary>
		/// Set a value of type <typeparamref name="BossDamage"/> for the property <paramref name="lastDamage">lastDamage</paramref>
		/// <summary>
		/// <param name="lastDamage">A value of type <typeparamref name="BossDamage"/> will the defined for the property</param>
		/// <returns>Returns the <see cref="BossBuilder">BossBuilder</see> with the property <paramref name="lastDamage">lastDamage</paramref> defined</returns>
		public BossBuilder WithLastDamage(BossDamage lastDamage)
		{
			this.lastDamage = lastDamage;
			return this;
		}

		/// <summary>
		/// Set a value of type <typeparamref name="Prey"/> for the property <paramref name="prey">prey</paramref>
		/// <summary>
		/// <param name="prey">A value of type <typeparamref name="Prey"/> will the defined for the property</param>
		/// <returns>Returns the <see cref="BossBuilder">BossBuilder</see> with the property <paramref name="prey">prey</paramref> defined</returns>
		public BossBuilder WithPrey(Prey prey)
		{
			this.prey = prey;
			return this;
		}

		/// <summary>
		/// Set a value of type <typeparamref name="int"/> for the property <paramref name="minusXp">minusXp</paramref>
		/// <summary>
		/// <param name="minusXp">A value of type <typeparamref name="int"/> will the defined for the property</param>
		/// <returns>Returns the <see cref="BossBuilder">BossBuilder</see> with the property <paramref name="minusXp">minusXp</paramref> defined</returns>
		public BossBuilder WithMinusXp(int minusXp)
		{
			this.minusXp = minusXp;
			return this;
		}

		/// <summary>
		/// Set a value of type <typeparamref name="List<Diamond>"/> for the property <paramref name="hideDiamonds">hideDiamonds</paramref>
		/// <summary>
		/// <param name="hideDiamonds">A value of type <typeparamref name="List<Diamond>"/> will the defined for the property</param>
		/// <returns>Returns the <see cref="BossBuilder">BossBuilder</see> with the property <paramref name="hideDiamonds">hideDiamonds</paramref> defined</returns>
		public BossBuilder WithHideDiamonds(List<Diamond> hideDiamonds)
		{
			this.hideDiamonds = hideDiamonds;
			return this;
		}

		/// <summary>
		/// An item of type <typeparamref name="Diamond"/> will be added to the collection <c>HideDiamonds</c>
		/// <summary>
		/// <param name="item">A value of type <typeparamref name="Diamond"/> will the added to the collection</param>
		/// <returns>Returns the <see cref="BossBuilder">BossBuilder</see> with the collection <c>HideDiamonds</c> with one more item</returns>
		public BossBuilder WithHideDiamondsItem(Diamond item)
		{
			hideDiamonds.Add(item);
			return this;
		}

		/// <summary>
		/// Build a class of type <see cref="Boss">Boss</see> with all the defined values
		/// <summary>
		/// <returns>Returns a <see cref="Boss">Boss</see> class</returns>
		public Boss Build()
		{
			return new Boss
			{
				number = number,
				Name = name,
				level = level,
				heroClassType = heroClassType,
				firstDamage = firstDamage,
				middleDamage = middleDamage,
				lastDamage = lastDamage,
				prey = prey,
				minusXp = minusXp,
				hideDiamonds = hideDiamonds,
			};
		}
	}
}