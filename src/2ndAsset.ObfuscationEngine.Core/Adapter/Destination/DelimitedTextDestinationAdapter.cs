/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.IO;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Support;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Destination
{
	public class DelimitedTextDestinationAdapter : DestinationAdapter, IDelimitedTextAdapter
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

		protected override void CoreInitialize(ObfuscationConfiguration obfuscationConfiguration)
		{
			DelimitedTextSpec effectiveDelimitedTextSpec;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.DestinationAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration"));

			if ((object)obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration"));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath"));

			if ((object)obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec == null &&
				((object)obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration == null ||
				(object)obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec == null))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "[Source/Destination]AdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec"));

			if ((object)obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec != null)
			{
				effectiveDelimitedTextSpec = obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec;

				if (effectiveDelimitedTextSpec.HeaderSpecs.Count <= 0 &&
					(object)obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs != null)
					effectiveDelimitedTextSpec.HeaderSpecs.AddRange(obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs);
			}
			else
				effectiveDelimitedTextSpec = obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec;

			if ((object)effectiveDelimitedTextSpec == null ||
				effectiveDelimitedTextSpec.HeaderSpecs.Count <= 0)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "[Source/Destination]AdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs"));

			this.DelimitedTextWriter = new DelimitedTextWriter(new StreamWriter(File.Open(obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath, FileMode.Create, FileAccess.Write, FileShare.None)), effectiveDelimitedTextSpec);
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