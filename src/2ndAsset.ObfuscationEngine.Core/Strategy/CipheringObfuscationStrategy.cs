/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Strategies;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	/// <summary>
	/// Returns an alternate value that is a binary encryption of the original value.
	/// DATA TYPE: string
	/// </summary>
	public sealed class CipheringObfuscationStrategy : ObfuscationStrategy<ObfuscationStrategyConfiguration>
	{
		#region Constructors/Destructors

		public CipheringObfuscationStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		private static object GetCipher(string sharedSecret, object value)
		{
			const string INIT_VECTOR = "0123456701234567";
			const int KEY_SIZE = 256;

			byte[] initVectorBytes;
			byte[] plainTextBytes;
			ICryptoTransform encryptor;
			byte[] keyBytes;
			byte[] cipherTextBytes;
			Type valueType;
			string _value;

			if ((object)sharedSecret == null)
				throw new ArgumentNullException("sharedSecret");

			if ((object)value == null)
				return null;

			valueType = value.GetType();

			if (valueType != typeof(String))
				return null;

			_value = (String)value;

			if (DataTypeFascade.Instance.IsWhiteSpace(_value))
				return _value;

			_value = _value.Trim();

			initVectorBytes = Encoding.UTF8.GetBytes(INIT_VECTOR);
			plainTextBytes = Encoding.UTF8.GetBytes(_value);

#if !CLR_35
			using (
#endif
				PasswordDeriveBytes password = new PasswordDeriveBytes(sharedSecret, null)
#if CLR_35
			;
#endif
#if !CLR_35
				)
#endif
			{
				// support 3.5 and above
				keyBytes = password.GetBytes(KEY_SIZE / 8);
			}

			using (RijndaelManaged symmetricKey = new RijndaelManaged())
			{
				symmetricKey.Mode = CipherMode.CBC;
				encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
					{
						cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
						cryptoStream.FlushFinalBlock();
						cipherTextBytes = memoryStream.ToArray();
					}
				}
			}

			return Encoding.UTF8.GetString(cipherTextBytes);
		}

		protected override object CoreGetObfuscatedValue(IOxymoronEngine oxymoronEngine, ColumnConfiguration<ObfuscationStrategyConfiguration> columnConfiguration, IMetaColumn metaColumn, object columnValue)
		{
			long signHash, valueHash;
			object value;
			string sharedSecret;

			if ((object)columnConfiguration == null)
				throw new ArgumentNullException("columnConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			signHash = this.GetSignHash(oxymoronEngine, columnValue);
			valueHash = this.GetValueHash(oxymoronEngine, null, columnValue);
			sharedSecret = ((valueHash <= 0 ? 1 : valueHash) * (signHash % 2 == 0 ? -1 : 1)).ToString("X");

			value = GetCipher(sharedSecret, columnValue);

			return value;
		}

		#endregion
	}
}