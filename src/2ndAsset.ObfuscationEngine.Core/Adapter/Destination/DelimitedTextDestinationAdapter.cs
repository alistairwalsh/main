/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;
using _2ndAsset.ObfuscationEngine.Core.Support;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Destination
{
	[AdapterSpecificConfiguration(SpecificConfigurationAqtn = "", UserControlAqtn = "_2ndAsset.ObfuscationEngine.UI.Controls.Adapters.DelimitedTextAdapterSettingsUserControl, 2ndAsset.ObfuscationEngine.UI")]
	public class DelimitedTextDestinationAdapter : DestinationAdapter<DelimitedTextAdapterConfiguration>, IDelimitedTextAdapter
	{
		#region Constructors/Destructors

		public DelimitedTextDestinationAdapter()
		{
		}

		#endregion

		#region Fields/Constants

		private RecordTextWriter delimitedTextWriter;

		#endregion

		#region Properties/Indexers/Events

		private RecordTextWriter DelimitedTextWriter
		{
			get
			{
				return this.delimitedTextWriter;
			}
			set
			{
				this.delimitedTextWriter = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreInitialize()
		{
			AdapterConfiguration<DelimitedTextAdapterConfiguration> sourceAdapterConfiguration;
			DelimitedTextSpec effectiveDelimitedTextSpec;

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextFilePath))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath"));

			sourceAdapterConfiguration = new AdapterConfiguration<DelimitedTextAdapterConfiguration>(((ObfuscationConfiguration)this.AdapterConfiguration.Parent).SourceAdapterConfiguration);

			if ((object)this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextSpec == null &&
				((object)sourceAdapterConfiguration == null ||
				(object)sourceAdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextSpec == null))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "[Source/Destination]...DelimitedTextSpec"));

			if ((object)this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextSpec != null)
			{
				effectiveDelimitedTextSpec = this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextSpec;

				if (effectiveDelimitedTextSpec.HeaderSpecs.Count <= 0 &&
					(object)sourceAdapterConfiguration != null &&
					(object)sourceAdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextSpec.HeaderSpecs != null)
					effectiveDelimitedTextSpec.HeaderSpecs.AddRange(sourceAdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextSpec.HeaderSpecs);
			}
			else
				effectiveDelimitedTextSpec = this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextSpec;

			if ((object)effectiveDelimitedTextSpec == null ||
				effectiveDelimitedTextSpec.HeaderSpecs.Count <= 0)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "[Source/Destination]...DelimitedTextSpec.HeaderSpecs"));

			this.DelimitedTextWriter = new DelimitedTextWriter(new StreamWriter(File.Open(this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextFilePath, FileMode.Create, FileAccess.Write, FileShare.None)), effectiveDelimitedTextSpec);
		}

		protected override void CorePushData(TableConfiguration tableConfiguration, IEnumerable<IDictionary<string, object>> sourceDataEnumerable)
		{
			if ((object)tableConfiguration == null)
				throw new ArgumentNullException("tableConfiguration");

			if ((object)sourceDataEnumerable == null)
				throw new ArgumentNullException("sourceDataEnumerable");

			this.DelimitedTextWriter.WriteRecords(sourceDataEnumerable);
		}

		protected override void CoreTerminate()
		{
			if ((object)this.DelimitedTextWriter != null)
			{
				this.DelimitedTextWriter.Flush();
				this.DelimitedTextWriter.Dispose();
			}

			this.DelimitedTextWriter = null;
		}

		#endregion
	}
}