/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using Solder.Framework;
using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Adapter;

namespace _2ndAsset.ObfuscationEngine.Core.Config
{
	public class AdapterConfiguration : ConfigurationObject
	{
		#region Constructors/Destructors

		public AdapterConfiguration()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly Dictionary<string, object> adapterSpecificConfiguration = new Dictionary<string, object>();
		private string adapterAqtn;

		#endregion

		#region Properties/Indexers/Events

		public Dictionary<string, object> AdapterSpecificConfiguration
		{
			get
			{
				return this.adapterSpecificConfiguration;
			}
		}

		public string AdapterAqtn
		{
			get
			{
				return this.adapterAqtn;
			}
			set
			{
				this.adapterAqtn = value;
			}
		}

		[JsonIgnore]
		public new IAdapterConfigurationDependency Parent
		{
			get
			{
				return (IAdapterConfigurationDependency)base.Parent;
			}
			set
			{
				base.Parent = value;
			}
		}

		#endregion

		#region Methods/Operators

		public TAdapter GetAdapterInstance<TAdapter>()
			where TAdapter : class, IAdapter
		{
			TAdapter instance;
			Type type;

			type = this.GetAdapterType();

			if ((object)type == null)
				return null;

			instance = (TAdapter)Activator.CreateInstance(type);

			return instance;
		}

		public virtual Type GetAdapterSpecificConfigurationType()
		{
			return null;
		}

		public Type GetAdapterType()
		{
			Type sourceAdapterType;

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterAqtn))
				return null;

			sourceAdapterType = Type.GetType(this.AdapterAqtn, false);

			return sourceAdapterType;
		}

		public virtual void ResetAdapterSpecificConfiguration()
		{
			this.AdapterSpecificConfiguration.Clear();
		}

		public override sealed IEnumerable<Message> Validate()
		{
			return this.Validate(null);
		}

		public virtual IEnumerable<Message> Validate(string adapterContext)
		{
			List<Message> messages;
			Type type;
			IAdapter adapter;

			messages = new List<Message>();

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterAqtn))
				messages.Add(NewError(string.Format("{0} adapter AQTN is required.", adapterContext)));
			else
			{
				type = this.GetAdapterType();

				if ((object)type == null)
					messages.Add(NewError(string.Format("{0} adapter failed to load type from AQTN.", adapterContext)));
				else if (typeof(IAdapter).IsAssignableFrom(type))
				{
					adapter = this.GetAdapterInstance<IAdapter>();

					if ((object)adapter == null)
						messages.Add(NewError(string.Format("{0} adapter failed to instatiate type from AQTN.", adapterContext)));
					else
						messages.AddRange(adapter.ValidateAdapterSpecificConfiguration(this, adapterContext));
				}
				else
					messages.Add(NewError(string.Format("{0} adapter loaded an unrecognized type via AQTN.", adapterContext)));
			}

			return messages;
		}

		#endregion
	}
}