/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace _2ndAsset.Ssis.Components
{
#if DEBUG
	public static class NopConstants
	{
		#region Fields/Constants

		public const int COMPONENT_CURRENT_VERSION = 1;
		public const string COMPONENT_DESCRIPTION = "2ndAsset No-op Transform";
		public const string COMPONENT_EMAIL = "dpbullington@gmail.com";
		public const string COMPONENT_ICON_RESOURCE_NAME = "_2ndAsset.Ssis.Components.Icons.2ndAssetSuite.ico";
		public const string COMPONENT_INPUT_DEFAULT_NAME = "No-op Input";
		public const string COMPONENT_NAME = "No-op Transform";
		public const string COMPONENT_OUTPUT_DEFAULT_NAME = "No-op Output";
		public const string COMPONENT_OUTPUT_ERROR_NAME = "No-op Error Output";
		public const string COMPONENT_PROP_DESC_DEBUGGER_LAUNCH = "A value indicating whether to launch the debugger at component initialization time.";
		public const string COMPONENT_PROP_NAME_DEBUGGER_LAUNCH = "DebuggerLaunch";
		public const string COMPONENT_RUNTIMECONNECTION_DESC_DICTIONARY = "Dictionary connection.";
		public const string COMPONENT_RUNTIMECONNECTION_NAME_DICTIONARY = "DictionaryConnection";
		public const string COMPONENT_UI_AQTN = "_2ndAsset.Ssis.Components.UI.NopTransformUI, 2ndAsset.Ssis.Components.UI, Version=0.1.0.0, Culture=neutral, PublicKeyToken=36f631e1fe773b98, processorArchitecture=MSIL";

		#endregion
	}
#endif
}