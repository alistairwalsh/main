/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Collections.Generic;

using TextMetal.Middleware.Common;

namespace _2ndAsset.ObfuscationEngine.Core.Config
{
	public interface IConfigurationObject
	{
		#region Properties/Indexers/Events

		IConfigurationObject Parent
		{
			get;
			set;
		}

		IConfigurationObjectCollection Surround
		{
			get;
			set;
		}

		#endregion

		#region Methods/Operators

		IEnumerable<Message> Validate();

		#endregion
	}
}