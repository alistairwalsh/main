/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
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
[assembly: AssemblyCopyright("©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington")]
[assembly: AssemblyDescription("CLOSED SOURCE, COMMERCIAL PRODUCT\r\nTHIS IS NOT OPEN SOURCE")]
[assembly: AssemblyTrademark("π")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("0.1.0.0")]
[assembly: AssemblyFileVersion("0.1.0.0")]
[assembly: AssemblyInformationalVersion("2015.06.01/preview")]
[assembly: AssemblyDelaySign(false)]
[assembly: ComVisible(false)]