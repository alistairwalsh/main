/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Newtonsoft.Json;

using Solder.Framework;
using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast.UoW;

namespace _2ndAsset.ObfuscationEngine.Core.Config
{
	public class AdoNetCommandConfiguration : ConfigurationObject
	{
		#region Constructors/Destructors

		public AdoNetCommandConfiguration()
		{
			this.adoNetParameterConfigurations = new ConfigurationObjectCollection<AdoNetParameterConfiguration>(this);
		}

		#endregion

		#region Fields/Constants

		private readonly ConfigurationObjectCollection<AdoNetParameterConfiguration> adoNetParameterConfigurations;
		private CommandBehavior commandBehavior = CommandBehavior.Default;
		private bool commandPrepare;
		private string commandText;
		private int? commandTimeout;
		private CommandType? commandType;

		#endregion

		#region Properties/Indexers/Events

		public ConfigurationObjectCollection<AdoNetParameterConfiguration> AdoNetParameterConfigurations
		{
			get
			{
				return this.adoNetParameterConfigurations;
			}
		}

		public CommandBehavior CommandBehavior
		{
			get
			{
				return this.commandBehavior;
			}
			set
			{
				this.commandBehavior = value;
			}
		}

		public bool CommandPrepare
		{
			get
			{
				return this.commandPrepare;
			}
			set
			{
				this.commandPrepare = value;
			}
		}

		public string CommandText
		{
			get
			{
				return this.commandText;
			}
			set
			{
				this.commandText = value;
			}
		}

		public int? CommandTimeout
		{
			get
			{
				return this.commandTimeout;
			}
			set
			{
				this.commandTimeout = value;
			}
		}

		public CommandType? CommandType
		{
			get
			{
				return this.commandType;
			}
			set
			{
				this.commandType = value;
			}
		}

		[JsonIgnore]
		public new AdoNetAdapterConfiguration Parent
		{
			get
			{
				return (AdoNetAdapterConfiguration)base.Parent;
			}
			set
			{
				base.Parent = value;
			}
		}

		#endregion

		#region Methods/Operators

		public IEnumerable<IDbDataParameter> GetDbDataParameters(IUnitOfWork unitOfWork)
		{
			List<IDbDataParameter> dbDataParameters;
			IDbDataParameter dbDataParameter;

			if ((object)unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");

			dbDataParameters = new List<IDbDataParameter>();

			foreach (AdoNetParameterConfiguration adoNetParameterConfiguration in this.AdoNetParameterConfigurations)
			{
				dbDataParameter = unitOfWork.CreateParameter(adoNetParameterConfiguration.ParameterDirection, adoNetParameterConfiguration.ParameterDbType, adoNetParameterConfiguration.ParameterSize, adoNetParameterConfiguration.ParameterPrecision, adoNetParameterConfiguration.ParameterScale, adoNetParameterConfiguration.ParameterNullable, adoNetParameterConfiguration.ParameterName, adoNetParameterConfiguration.ParameterValue);
				dbDataParameters.Add(dbDataParameter);
			}

			return dbDataParameters.ToArray();
		}

		public override IEnumerable<Message> Validate()
		{
			return this.Validate(null);
		}

		public IEnumerable<Message> Validate(string context)
		{
			List<Message> messages;
			int index;

			messages = new List<Message>();

			if ((object)this.CommandType == null)
				messages.Add(NewError(string.Format("{0} command type is required.", context)));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.CommandText))
				messages.Add(NewError(string.Format("{0} command text is required.", context)));

			// check for duplicate columns
			var columnNameSums = this.AdoNetParameterConfigurations.GroupBy(c => c.ParameterName)
				.Select(cl => new
							{
								ParameterName = cl.First().ParameterName,
								Count = cl.Count()
							}).Where(cl => cl.Count > 1);

			if (columnNameSums.Any())
				messages.AddRange(columnNameSums.Select(c => NewError(string.Format("ADO.NET command configuration with duplicate ADO.NET parameter configuration found: '{0}'.", c.ParameterName))).ToArray());

			index = 0;
			foreach (AdoNetParameterConfiguration adoNetParameterConfiguration in this.AdoNetParameterConfigurations)
				messages.AddRange(adoNetParameterConfiguration.Validate(index++));

			return messages;
		}

		#endregion
	}
}