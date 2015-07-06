/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Strategies;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	/// <summary>
	/// Returns un-obfuscated, original value.
	/// DATA TYPE: any
	/// </summary>
	public sealed class SurrogateKeyObfuscationStrategy : ObfuscationStrategy<ObfuscationStrategyConfiguration>
	{
		#region Constructors/Destructors

		public SurrogateKeyObfuscationStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		private static object GetSurrogateKey(long randomSeed, object value)
		{
			Random random;
			Op op;
			int val;

			Type valueType;
			Int64 _value;

			if ((object)value == null)
				return null;

			valueType = value.GetType();

			if (!typeof(Int64).IsAssignableFrom(valueType))
				return null;

			_value = value.ChangeType<Int64>();

			random = new Random((int)randomSeed);
			int max = random.Next(0, 99);

			// TODO - use lighweight dynmamic method here?
			for (int i = 0; i < max; i++)
			{
				op = (Op)random.Next(1, 4);

				val = random.Next(); // unbounded

				switch (op)
				{
					case Op.Add:
						_value += val;
						break;
					case Op.Sub:
						_value -= val;
						break;
					case Op.Mul:
						_value *= val;
						break;
					case Op.Div:
						if(val != 0)
							_value /= val;
						break;
					case Op.Mod:
						_value %= val;
						break;
					default:
						break;
				}
			}

			value = _value.ChangeType(valueType);
			return value;
		}

		protected override object CoreGetObfuscatedValue(IOxymoronEngine oxymoronEngine, ColumnConfiguration<ObfuscationStrategyConfiguration> columnConfiguration, IMetaColumn metaColumn, object columnValue)
		{
			long valueHash;
			object value;
			long randomSeed;
			
			if ((object)columnConfiguration == null)
				throw new ArgumentNullException("columnConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");
			
			valueHash = this.GetValueHash(oxymoronEngine, null, metaColumn.ColumnName);
			randomSeed = valueHash;

			// create a new repeatable yet random-ish math funcion using the random seed, then executes for column value 
			value = GetSurrogateKey(randomSeed, columnValue);

			return value;
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private enum Op
		{
			Add = 1,
			Sub,
			Mul,
			Div,
			Mod
		}

		#endregion
	}
}