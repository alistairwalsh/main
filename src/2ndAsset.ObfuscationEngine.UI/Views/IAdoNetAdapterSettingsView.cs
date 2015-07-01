/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;

using _2ndAsset.Common.WinForms;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IAdoNetAdapterSettingsView : ISpecificAdapterSettingsView
	{
		#region Properties/Indexers/Events

		IEnumerable<IListItem<CommandType?>> CommandTypes
		{
			set;
		}

		string ConnectionString
		{
			get;
			set;
		}

		Type ConnectionType
		{
			get;
			set;
		}

		IEnumerable<IListItem<Type>> ConnectionTypes
		{
			set;
		}

		string ExecuteCommandText
		{
			get;
			set;
		}

		CommandType? ExecuteCommandType
		{
			get;
			set;
		}

		bool IsCommandTypeReadOnly
		{
			get;
			set;
		}

		bool IsConnectionTypeReadOnly
		{
			get;
			set;
		}

		string PostExecuteCommandText
		{
			get;
			set;
		}

		CommandType? PostExecuteCommandType
		{
			get;
			set;
		}

		string PreExecuteCommandText
		{
			get;
			set;
		}

		CommandType? PreExecuteCommandType
		{
			get;
			set;
		}

		#endregion
	}
}