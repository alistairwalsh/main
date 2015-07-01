/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities
{
	public interface IAssemblyInformationFascade
	{
		#region Properties/Indexers/Events

		/// <summary>
		/// Gets the assembly version.
		/// </summary>
		string AssemblyVersion
		{
			get;
		}

		/// <summary>
		/// Gets the assembly company.
		/// </summary>
		string Company
		{
			get;
		}

		/// <summary>
		/// Gets the assembly configuration.
		/// </summary>
		string Configuration
		{
			get;
		}

		/// <summary>
		/// Gets the assembly copyright.
		/// </summary>
		string Copyright
		{
			get;
		}

		/// <summary>
		/// Gets the assembly description.
		/// </summary>
		string Description
		{
			get;
		}

		/// <summary>
		/// Gets the assembly informational version.
		/// </summary>
		string InformationalVersion
		{
			get;
		}

		/// <summary>
		/// Gets the assembly product.
		/// </summary>
		string Product
		{
			get;
		}

		/// <summary>
		/// Gets the assembly title.
		/// </summary>
		string Title
		{
			get;
		}

		/// <summary>
		/// Gets the assembly trademark.
		/// </summary>
		string Trademark
		{
			get;
		}

		/// <summary>
		/// Gets the assembly Win32 file version.
		/// </summary>
		string Win32FileVersion
		{
			get;
		}

		#endregion
	}
}