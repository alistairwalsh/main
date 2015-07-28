/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Hosting.Tool
{
	public interface IToolHost : IOxymoronHost
	{
		#region Methods/Operators

		void Host(string sourceFilePath);

		void Host(ObfuscationConfiguration obfuscationConfiguration, Action<long, bool, double> statusCallback);

		bool TryGetUpstreamMetadata(ObfuscationConfiguration obfuscationConfiguration, out IEnumerable<IMetaColumn> metaColumns);

		#endregion
	}
}