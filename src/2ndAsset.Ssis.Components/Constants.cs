/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

namespace _2ndAsset.Ssis.Components
{
	public static class Constants
	{
		#region Fields/Constants

		public const int COMPONENT_CURRENT_VERSION = 1;
		public const string COMPONENT_DESCRIPTION = "2ndAsset Obfuscation Strategy Transform";
		public const string COMPONENT_EMAIL = "info@2ndasset.com";
		public const string COMPONENT_ICON_RESOURCE_NAME = "_2ndAsset.Ssis.Components.Icons.2ndAssetSuite.ico";
		public const string COMPONENT_INPUT_DEFAULT_NAME = "Obfuscation Strategy Input";
		public const string COMPONENT_NAME = "Obfuscation Strategy Transform";
		public const string COMPONENT_OUTPUT_DEFAULT_NAME = "Obfuscation Strategy Output";
		public const string COMPONENT_OUTPUT_ERROR_NAME = "Obfuscation Strategy Error Output";
		public const string COMPONENT_PROP_DESC_DEBUGGER_LAUNCH = "A value indicating whether to launch the debugger at component initialization time.";
		public const string COMPONENT_PROP_DESC_DICTIONARY_CONFIGURATION = "JSON serialized dictionary configuration data.";
		public const string COMPONENT_PROP_DESC_SIGN_HASH_MULTIPLIER = "Sign hash multiplier.";
		public const string COMPONENT_PROP_DESC_SIGN_HASH_SEED = "Sign hash seed.";
		public const string COMPONENT_PROP_DESC_VALUE_HASH_MULTIPLIER = "Value hash multiplier.";
		public const string COMPONENT_PROP_DESC_VALUE_HASH_SEED = "Value hash seed.";
		public const string COMPONENT_PROP_NAME_DEBUGGER_LAUNCH = "DebuggerLaunch";
		public const string COMPONENT_PROP_NAME_DICTIONARY_CONFIGURATION = "DictionaryConfiguration";
		public const string COMPONENT_PROP_NAME_SIGN_HASH_MULTIPLIER = "SignHashMultiplier";
		public const string COMPONENT_PROP_NAME_SIGN_HASH_SEED = "SignHashSeed";
		public const string COMPONENT_PROP_NAME_VALUE_HASH_MULTIPLIER = "ValueHashMultiplier";
		public const string COMPONENT_PROP_NAME_VALUE_HASH_SEED = "ValueHashSeed";
		public const string COMPONENT_PROP_TYPE_CONV_AQTN_DICTIONARY_CONFIGURATION = "_2ndAsset.Ssis.Components.UI.ReadonlyTypeConverter, 2ndAsset.Ssis.Components.UI, Version=0.1.0.0, Culture=neutral, PublicKeyToken=36f631e1fe773b98, processorArchitecture=MSIL";
		public const string COMPONENT_PROP_UI_EDIT_AQTN_DICTIONARY_CONFIGURATION = "_2ndAsset.Ssis.Components.UI.DictConfFormEditor, 2ndAsset.Ssis.Components.UI, Version=0.1.0.0, Culture=neutral, PublicKeyToken=36f631e1fe773b98, processorArchitecture=MSIL";
		public const string COMPONENT_RUNTIMECONNECTION_DESC_DICTIONARY = "Dictionary connection.";
		public const int COMPONENT_RUNTIMECONNECTION_IDX_DESTINATION = 0;
		public const int COMPONENT_RUNTIMECONNECTION_IDX_SOURCE = 0;
		public const string COMPONENT_RUNTIMECONNECTION_NAME_DICTIONARY = "DictionaryConnection";
		public const string COMPONENT_UI_AQTN = "_2ndAsset.Ssis.Components.UI.ObfuscationStrategyTransformUI, 2ndAsset.Ssis.Components.UI, Version=0.1.0.0, Culture=neutral, PublicKeyToken=36f631e1fe773b98, processorArchitecture=MSIL";
		public const string INPUTCOLUMN_PROP_DESC_DICTIONARY_REFERENCE = "Dictionary reference.";
		public const string INPUTCOLUMN_PROP_DESC_IS_COLUMN_NULLABLE = "Is column nullable.";
		public const string INPUTCOLUMN_PROP_DESC_MASKING_EXTENT = "Masking extent.";
		public const string INPUTCOLUMN_PROP_DESC_OBFUSCATION_STRATEGY = "Obfuscation strategy.";
		public const string INPUTCOLUMN_PROP_DESC_VARIANCE_EXTENT = "Variance extent.";
		public const string INPUTCOLUMN_PROP_NAME_DICTIONARY_REFERENCE = "DictionaryReference";
		public const string INPUTCOLUMN_PROP_NAME_EXTENT_VALUE = "ExtentValue";
		public const string INPUTCOLUMN_PROP_NAME_IS_COLUMN_NULLABLE = "IsColumnNullable";
		public const string INPUTCOLUMN_PROP_NAME_OBFUSCATION_STRATEGY = "ObfuscationStrategy";

		#endregion
	}
}