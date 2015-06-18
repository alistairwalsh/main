/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

namespace _2ndAsset.ObfuscationEngine.Core.Support
{
	public class DisposableList<T> : List<T>, IDisposable
		where T : IDisposable
	{
		#region Constructors/Destructors

		public DisposableList()
		{
		}

		#endregion

		#region Methods/Operators

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (IDisposable disposable in this)
					disposable.Dispose();
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}