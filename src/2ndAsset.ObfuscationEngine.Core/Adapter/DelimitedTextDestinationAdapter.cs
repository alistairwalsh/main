/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.IO;

using TextMetal.Middleware.Common.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Support;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter
{
	public sealed class DelimitedTextDestinationAdapter : Adapter, IDestinationAdapter, IDelimitedTextAdapter
	{
		#region Constructors/Destructors

		public DelimitedTextDestinationAdapter()
		{
		}

		#endregion

		#region Fields/Constants

		private RecordTextWriter delimitedTextWriter;
		private IEnumerable<MetaColumn> upstreamMetadata;

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

		public IEnumerable<MetaColumn> UpstreamMetadata
		{
			set
			{
				this.upstreamMetadata = value;
			}
			protected get
			{
				return this.upstreamMetadata;
			}
		}

		#endregion

		#region Methods/Operators

		public override void Initialize(ObfuscationConfiguration configuration)
		{
			DelimitedTextSpec effectiveDelimitedTextSpec;

			base.Initialize(configuration);

			if ((object)configuration == null)
				throw new ArgumentNullException("configuration");

			if ((object)configuration.DestinationAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration"));

			if ((object)configuration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration"));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(configuration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath"));

			if ((object)configuration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec == null &&
				((object)configuration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration == null ||
				(object)configuration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec == null))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "[Source/Destination]AdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec"));

			if ((object)configuration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec != null)
			{
				effectiveDelimitedTextSpec = configuration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec;

				if (effectiveDelimitedTextSpec.HeaderSpecs.Count <= 0 &&
					(object)configuration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs != null)
					effectiveDelimitedTextSpec.HeaderSpecs.AddRange(configuration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs);
			}
			else
				effectiveDelimitedTextSpec = configuration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec;

			if ((object)effectiveDelimitedTextSpec == null ||
				effectiveDelimitedTextSpec.HeaderSpecs.Count <= 0)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "[Source/Destination]AdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs"));

			this.DelimitedTextWriter = new DelimitedTextWriter(new StreamWriter(File.Open(configuration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath, FileMode.Create, FileAccess.Write, FileShare.None)), effectiveDelimitedTextSpec);
		}

		public void Next(TableConfiguration configuration)
		{
			// do nothing
		}

		public void PushData(TableConfiguration configuration, IEnumerable<IDictionary<string, object>> sourceDataEnumerable)
		{
			if ((object)configuration == null)
				throw new ArgumentNullException("configuration");

			if ((object)sourceDataEnumerable == null)
				throw new ArgumentNullException("sourceDataEnumerable");

			this.DelimitedTextWriter.WriteRecords(sourceDataEnumerable);
		}

		public override void Terminate()
		{
			if ((object)this.DelimitedTextWriter != null)
			{
				this.DelimitedTextWriter.Flush();
				this.DelimitedTextWriter.Dispose();
			}

			this.DelimitedTextWriter = null;

			base.Terminate();
		}

		#endregion
	}
}