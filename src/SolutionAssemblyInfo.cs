/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Reflection;
using System.Runtime.InteropServices;

#if DEBUG

[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyCompany("2ndAsset.com")]
[assembly: AssemblyProduct("2ndAsset Suite")]
[assembly: AssemblyCopyright("©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington")]
[assembly: AssemblyDescription("Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php")]
[assembly: AssemblyTrademark("π")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("2015.07.29/beta")]
[assembly: AssemblyDelaySign(false)]
[assembly: ComVisible(false)]