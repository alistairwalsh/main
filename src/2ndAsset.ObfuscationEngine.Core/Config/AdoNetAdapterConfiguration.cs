/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;

using Solder.Framework;
using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast.UoW;

namespace _2ndAsset.ObfuscationEngine.Core.Config
{
	public class AdoNetAdapterConfiguration : ConfigurationObject
	{
		#region Constructors/Destructors

		public AdoNetAdapterConfiguration()
		{
		}

		#endregion

		#region Fields/Constants

		private string connectionAqtn;
		private string connectionString;
		private string executeCommandText;
		private CommandType? executeCommandType;
		private string postExecuteCommandText;
		private CommandType? postExecuteCommandType;
		private string preExecuteCommandText;
		private CommandType? preExecuteCommandType;

		#endregion

		#region Properties/Indexers/Events

		public string ConnectionAqtn
		{
			get
			{
				return this.connectionAqtn;
			}
			set
			{
				this.connectionAqtn = value;
			}
		}

		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				this.connectionString = value;
			}
		}

		public string ExecuteCommandText
		{
			get
			{
				return this.executeCommandText;
			}
			set
			{
				this.executeCommandText = value;
			}
		}

		public CommandType? ExecuteCommandType
		{
			get
			{
				return this.executeCommandType;
			}
			set
			{
				this.executeCommandType = value;
			}
		}

		public string PostExecuteCommandText
		{
			get
			{
				return this.postExecuteCommandText;
			}
			set
			{
				this.postExecuteCommandText = value;
			}
		}

		public CommandType? PostExecuteCommandType
		{
			get
			{
				return this.postExecuteCommandType;
			}
			set
			{
				this.postExecuteCommandType = value;
			}
		}

		public string PreExecuteCommandText
		{
			get
			{
				return this.preExecuteCommandText;
			}
			set
			{
				this.preExecuteCommandText = value;
			}
		}

		public CommandType? PreExecuteCommandType
		{
			get
			{
				return this.preExecuteCommandType;
			}
			set
			{
				this.preExecuteCommandType = value;
			}
		}

		#endregion

		#region Methods/Operators

		public Type GetConnectionType()
		{
			Type connectionType;

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.ConnectionAqtn))
				return null;

			connectionType = Type.GetType(this.ConnectionAqtn, false);

			return connectionType;
		}

		public virtual IUnitOfWork GetUnitOfWork(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
		{
			Type dictionaryConnectionType;

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.ConnectionAqtn))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "AdoNetAdapterConfiguration.ConnectionAqtn"));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.ConnectionString))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "AdoNetAdapterConfiguration.ConnectionString"));

			dictionaryConnectionType = this.GetConnectionType();

			if ((object)dictionaryConnectionType == null)
				throw new InvalidOperationException(string.Format("Connect type failed to load: '{0}'.", this.ConnectionAqtn));

			return UnitOfWork.Create(dictionaryConnectionType, this.ConnectionString, false, isolationLevel);
		}

		public override IEnumerable<Message> Validate()
		{
			return this.Validate(null);
		}

		public IEnumerable<Message> Validate(string context)
		{
			List<Message> messages;

			messages = new List<Message>();

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.ConnectionAqtn))
				messages.Add(NewError(string.Format("Connection AQTN is required.")));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.ConnectionString))
				messages.Add(NewError(string.Format("Connection string is required.")));

			if ((object)this.ExecuteCommandType == null)
				messages.Add(NewError(string.Format("Execute command type is required.")));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.ExecuteCommandText))
				messages.Add(NewError(string.Format("Execute command text is required.")));

			return messages;
		}

		#endregion
	}
}