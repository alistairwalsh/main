/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Adapter;
using _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV;
using _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities;

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

		private string adapterAqtn;
		private AdoNetAdapterConfiguration adoNetAdapterConfiguration;
		private DelimitedTextAdapterConfiguration delimitedTextAdapterConfiguration;

		#endregion

		#region Properties/Indexers/Events

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

		public AdoNetAdapterConfiguration AdoNetAdapterConfiguration
		{
			get
			{
				return this.adoNetAdapterConfiguration;
			}
			set
			{
				this.EnsureParentOnPropertySet(this.adoNetAdapterConfiguration, value);
				this.adoNetAdapterConfiguration = value;
			}
		}

		public DelimitedTextAdapterConfiguration DelimitedTextAdapterConfiguration
		{
			get
			{
				return this.delimitedTextAdapterConfiguration;
			}
			set
			{
				this.EnsureParentOnPropertySet(this.delimitedTextAdapterConfiguration, value);
				this.delimitedTextAdapterConfiguration = value;
			}
		}

		#endregion

		#region Methods/Operators

		public Type GetAdapterType()
		{
			Type sourceAdapterType;

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterAqtn))
				return null;

			sourceAdapterType = Type.GetType(this.AdapterAqtn, false);

			return sourceAdapterType;
		}

		public override IEnumerable<Message> Validate()
		{
			return this.Validate(null);
		}

		public IEnumerable<Message> Validate(string context)
		{
			List<Message> messages;
			Type type;

			messages = new List<Message>();

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterAqtn))
				messages.Add(NewError(string.Format("{0} adapter AQTN is required.", context)));
			else
			{
				type = this.GetAdapterType();

				if ((object)type == null)
					messages.Add(NewError(string.Format("{0} adapter failed to load type from AQTN.", context)));
				else if (typeof(INullAdapter).IsAssignableFrom(type))
				{
					// do nothing
				}
				else if (typeof(IDelimitedTextAdapter).IsAssignableFrom(type))
				{
					if ((object)this.DelimitedTextAdapterConfiguration == null)
						messages.Add(NewError(string.Format("{0} adapter DelimitedTextAdapterConfiguration section missing.", context)));
					else
						messages.AddRange(this.DelimitedTextAdapterConfiguration.Validate(context));
				}
				else if (typeof(IAdoNetAdapter).IsAssignableFrom(type))
				{
					if ((object)this.AdoNetAdapterConfiguration == null)
						messages.Add(NewError(string.Format("{0} adapter AdoNetAdapterConfiguration section missing.", context)));
					else
						messages.AddRange(this.AdoNetAdapterConfiguration.Validate(context));
				}
				else
					messages.Add(NewError(string.Format("{0} adapter an unrecognized type via AQTN.", context)));
			}

			return messages;
		}

		#endregion
	}
}