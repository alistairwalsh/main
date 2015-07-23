/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.ObfuscationEngine.UI
{
	public static class Constants
	{
		#region Fields/Constants

		public const string URI_ADAPTER_UPDATE_EVENT = "event://obfuscation/adapter/update";
		public const string URI_EXECUTE_OBFUSCATION_EVENT = "event://obfuscation/execute";
		public const string URI_REFRESH_UPSTREAM_METADATA_COLUMNS_EVENT = "event://obfuscation/metadata-settings/refresh-meta-column-specs";
		private static readonly Uri refreshUpstreamMetadataColumnsEventUri = new Uri(URI_REFRESH_UPSTREAM_METADATA_COLUMNS_EVENT);
		private static readonly Uri adapterUpdateEventUri = new Uri(URI_ADAPTER_UPDATE_EVENT);
		private static readonly Uri executeObfuscationEventUri = new Uri(URI_EXECUTE_OBFUSCATION_EVENT);

		#endregion

		#region Properties/Indexers/Events

		public static Uri AdapterUpdateEventUri
		{
			get
			{
				return adapterUpdateEventUri;
			}
		}

		public static Uri ExecuteObfuscationEventUri
		{
			get
			{
				return executeObfuscationEventUri;
			}
		}

		public static Uri RefreshUpstreamMetadataColumnsEventUri
		{
			get
			{
				return refreshUpstreamMetadataColumnsEventUri;
			}
		}

		#endregion
	}
}