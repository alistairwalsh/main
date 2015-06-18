/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core
{
	public struct HashResult
	{
		#region Fields/Constants

		private long signHash;
		private long valueHash;

		#endregion

		#region Properties/Indexers/Events

		public long SignHash
		{
			get
			{
				return this.signHash;
			}
			set
			{
				this.signHash = value;
			}
		}

		public long ValueHash
		{
			get
			{
				return this.valueHash;
			}
			set
			{
				this.valueHash = value;
			}
		}

		#endregion
	}
}