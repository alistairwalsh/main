/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;

using _2ndAsset.Common.WinForms;

namespace _2ndAsset.ObfuscationEngine.UI.Views.Adapters
{
	public interface IAdoNetAdapterSettingsPartialView : IAdapterSpecificSettingsPartialView
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