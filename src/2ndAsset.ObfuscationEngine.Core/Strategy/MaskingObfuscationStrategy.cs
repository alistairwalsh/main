/*
	Copyright �2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Text;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Strategies;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	/// <summary>
	/// Returns an alternate value that is a +/- (%) mask of the original value.
	/// DATA TYPE: string
	/// </summary>
	public sealed class MaskingObfuscationStrategy : ObfuscationStrategy<MaskingObfuscationStrategyConfiguration>
	{
		#region Constructors/Destructors

		public MaskingObfuscationStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		private static object GetMask(double maskFactor, object value)
		{
			StringBuilder buffer;
			Type valueType;
			string _value;

			if ((int)(maskFactor * 100) > 100)
				throw new ArgumentOutOfRangeException("maskFactor");

			if ((int)(maskFactor * 100) == 000)
				throw new ArgumentOutOfRangeException("maskFactor");

			if ((int)(maskFactor * 100) < -100)
				throw new ArgumentOutOfRangeException("maskFactor");

			if ((object)value == null)
				return null;

			valueType = value.GetType();

			if (valueType != typeof(String))
				return null;

			_value = (String)value;

			if (DataTypeFascade.Instance.IsWhiteSpace(_value))
				return _value;

			_value = _value.Trim();

			buffer = new StringBuilder(_value);

			if (Math.Sign(maskFactor) == 1)
			{
				for (int index = 0; index < (int)Math.Round((double)_value.Length * maskFactor); index++)
					buffer[index] = '*';
			}
			else if (Math.Sign(maskFactor) == -1)
			{
				for (int index = _value.Length - (int)Math.Round((double)_value.Length * Math.Abs(maskFactor)); index < _value.Length; index++)
					buffer[index] = '*';
			}
			else
				throw new InvalidOperationException("maskFactor");

			return buffer.ToString();
		}

		protected override object CoreGetObfuscatedValue(IOxymoronEngine oxymoronEngine, ColumnConfiguration<MaskingObfuscationStrategyConfiguration> columnConfiguration, IMetaColumn metaColumn, object columnValue)
		{
			object value;
			double maskingFactor;

			if ((object)columnConfiguration == null)
				throw new ArgumentNullException("columnConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)columnConfiguration.ObfuscationStrategySpecificConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "ObfuscationStrategyConfiguration"));

			maskingFactor = (columnConfiguration.ObfuscationStrategySpecificConfiguration.MaskingPercentValue.GetValueOrDefault() / 100.0);

			value = GetMask(maskingFactor, columnValue);

			return value;
		}

		#endregion
	}
}