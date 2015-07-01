/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities
{
	/// <summary>
	/// Provides static helper and/or extension methods for strongly typed read access to an app.config or web.config file.
	/// </summary>
	public class AppConfigFascade : IAppConfigFascade
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the AppConfigFascade class.
		/// </summary>
		/// <param name="dataTypeFascade"> The data type instance to use. </param>
		public AppConfigFascade(IDataTypeFascade dataTypeFascade)
		{
			if ((object)dataTypeFascade == null)
				throw new ArgumentNullException("dataTypeFascade");

			this.dataTypeFascade = dataTypeFascade;
		}

		/// <summary>
		/// Initializes a new instance of the AppConfigFascade class.
		/// </summary>
		private AppConfigFascade()
			: this(Utilities.DataTypeFascade.Instance)
		{
		}

		#endregion

		#region Fields/Constants

		private const string APPCONFIG_ARGS_REGEX = @"-(" + APPCONFIG_ID_REGEX_UNBOUNDED + @"{0,63}):(.{0,})";
		private const string APPCONFIG_ID_REGEX_UNBOUNDED = @"[a-zA-Z_\.][a-zA-Z_\.0-9]";
		private const string APPCONFIG_PROPS_REGEX = @"(" + APPCONFIG_ID_REGEX_UNBOUNDED + @"{0,63})=(.{0,})";
		private static readonly IAppConfigFascade instance = new AppConfigFascade();
		private readonly IDataTypeFascade dataTypeFascade;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// Gets the regular expression pattern for arguments.
		/// </summary>
		public static string ArgsRegEx
		{
			get
			{
				return APPCONFIG_ARGS_REGEX;
			}
		}

		public static IAppConfigFascade Instance
		{
			get
			{
				return instance;
			}
		}

		/// <summary>
		/// Gets the regular expression pattern for properties.
		/// </summary>
		public static string PropsRegEx
		{
			get
			{
				return APPCONFIG_PROPS_REGEX;
			}
		}

		private IDataTypeFascade DataTypeFascade
		{
			get
			{
				return this.dataTypeFascade;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Gets the value of an app settings for the current application's default configuration. A ConfigurationErrorsException is thrown if the key does not exist.
		/// </summary>
		/// <typeparam name="TValue"> The type to convert the app settings value. </typeparam>
		/// <param name="key"> The key to get a value. </param>
		/// <returns> The app settings as type TValue. </returns>
		public TValue GetAppSetting<TValue>(string key)
		{
			string svalue;
			TValue ovalue;
			Type typeOfValue;

			if ((object)key == null)
				throw new ArgumentNullException("key");

			typeOfValue = typeof(TValue);
			svalue = ConfigurationManager.AppSettings[key];

			if ((object)svalue == null)
				throw new ConfigurationErrorsException(string.Format("Key '{0}' was not found in app.config file.", key));

			if (!this.DataTypeFascade.TryParse<TValue>(svalue, out ovalue))
				throw new ConfigurationErrorsException(string.Format("App.config key '{0}' value '{1}' is not a valid '{2}'.", key, svalue, typeOfValue.FullName));

			return ovalue;
		}

		/// <summary>
		/// Gets the value of an app settings for the current application's default configuration. A ConfigurationErrorsException is thrown if the key does not exist.
		/// </summary>
		/// <param name="valueType"> The type to convert the app settings value. </param>
		/// <param name="key"> The key to get a value. </param>
		/// <returns> The app settings as a string. </returns>
		public object GetAppSetting(Type valueType, string key)
		{
			string svalue;
			object ovalue;

			if ((object)valueType == null)
				throw new ArgumentNullException("valueType");

			if ((object)key == null)
				throw new ArgumentNullException("key");

			svalue = ConfigurationManager.AppSettings[key];

			if ((object)svalue == null)
				throw new ConfigurationErrorsException(string.Format("Key '{0}' was not found in app.config file.", key));

			if (!this.DataTypeFascade.TryParse(valueType, svalue, out ovalue))
				throw new ConfigurationErrorsException(string.Format("App.config key '{0}' value '{1}' is not a valid '{2}'.", key, svalue, valueType.FullName));

			return ovalue;
		}

		/// <summary>
		/// Gets the value of a connection provider for the current application's default configuration. A ConfigurationErrorsException is thrown if the name does not exist.
		/// </summary>
		/// <param name="name"> The name to get a value. </param>
		/// <returns> The connection provider. </returns>
		public string GetConnectionProvider(string name)
		{
			ConnectionStringSettings value;

			if ((object)name == null)
				throw new ArgumentNullException("name");

			value = ConfigurationManager.ConnectionStrings[name];

			if ((object)value == null)
				throw new ConfigurationErrorsException(string.Format("Connection string name '{0}' was not found in app.config file.", name));

			//if ((object)value.ConnectionString == null)
			//	throw new ConfigurationErrorsException(string.Format("Connection string name '{0}' was not found in app.config file.", name));

			return value.ProviderName;
		}

		/// <summary>
		/// Gets the value of a connection string for the current application's default configuration. A ConfigurationErrorsException is thrown if the name does not exist.
		/// </summary>
		/// <param name="name"> The name to get a value. </param>
		/// <returns> The connection string. </returns>
		public string GetConnectionString(string name)
		{
			ConnectionStringSettings value;

			if ((object)name == null)
				throw new ArgumentNullException("name");

			value = ConfigurationManager.ConnectionStrings[name];

			if ((object)value == null)
				throw new ConfigurationErrorsException(string.Format("Connection string name '{0}' was not found in app.config file.", name));

			//if ((object)value.ConnectionString == null)
			//	throw new ConfigurationErrorsException(string.Format("Connection string name '{0}' was not found in app.config file.", name));

			return value.ConnectionString;
		}

		/// <summary>
		/// Checks to see if an app settings key exists for the current application's default configuration.
		/// </summary>
		/// <param name="key"> The key to check. </param>
		/// <returns> A boolean value indicating the app setting key presence. </returns>
		public bool HasAppSetting(string key)
		{
			string value;

			if ((object)key == null)
				throw new ArgumentNullException("key");

			value = ConfigurationManager.AppSettings[key];

			return ((object)value != null);
		}

		/// <summary>
		/// Checks to see if a connection string name exists for the current application's default configuration.
		/// </summary>
		/// <param name="name"> The name to check. </param>
		/// <returns> A boolean value indicating the connection string name presence. </returns>
		public bool HasConnectionString(string name)
		{
			ConnectionStringSettings value;

			if ((object)name == null)
				throw new ArgumentNullException("name");

			value = ConfigurationManager.ConnectionStrings[name];

			return ((object)value != null);
		}

		/// <summary>
		/// Given a string array of command line arguments, this method will parse the arguments using a well know pattern match to obtain a loosely typed dictionary of key/multi-value pairs for use by applications.
		/// </summary>
		/// <param name="args"> The command line argument array to parse. </param>
		/// <returns> A loosely typed dictionary of key/multi-value pairs. </returns>
		public IDictionary<string, IList<string>> ParseCommandLineArguments(string[] args)
		{
			IDictionary<string, IList<string>> arguments;
			Match match;
			string key, value;
			IList<string> argumentValues;

			if ((object)args == null)
				throw new ArgumentNullException("args");

			arguments = new Dictionary<string, IList<string>>(StringComparer.InvariantCultureIgnoreCase);

			foreach (string arg in args)
			{
				match = Regex.Match(arg, ArgsRegEx, RegexOptions.IgnorePatternWhitespace);

				if ((object)match == null)
					continue;

				if (!match.Success)
					continue;

				if (match.Groups.Count != 3)
					continue;

				key = match.Groups[1].Value;
				value = match.Groups[2].Value;

				// key is required
				if (this.DataTypeFascade.IsNullOrWhiteSpace(key))
					continue;

				// val is required
				if (this.DataTypeFascade.IsNullOrWhiteSpace(value))
					continue;

				if (!arguments.ContainsKey(key))
					arguments.Add(key, new List<string>());

				argumentValues = arguments[key];

				// duplicate values are ignored
				if (argumentValues.Contains(value))
					continue;

				argumentValues.Add(value);
			}

			return arguments;
		}

		/// <summary>
		/// Given a string property, this method will parse the property using a well know pattern match to obtain an output key/value pair for use by applications.
		/// </summary>
		/// <param name="arg"> The property to parse. </param>
		/// <param name="key"> The output property key. </param>
		/// <param name="value"> The output property value. </param>
		/// <returns> A value indicating if the parse was successful or not. </returns>
		public bool TryParseCommandLineArgumentProperty(string arg, out string key, out string value)
		{
			Match match;
			string k, v;

			key = null;
			value = null;

			if ((object)arg == null)
				throw new ArgumentNullException("arg");

			match = Regex.Match(arg, PropsRegEx, RegexOptions.IgnorePatternWhitespace);

			if ((object)match == null)
				return false;

			if (!match.Success)
				return false;

			if (match.Groups.Count != 3)
				return false;

			k = match.Groups[1].Value;
			v = match.Groups[2].Value;

			// key is required
			if (this.DataTypeFascade.IsNullOrWhiteSpace(k))
				return false;

			// val is required
			if (this.DataTypeFascade.IsNullOrWhiteSpace(v))
				return false;

			key = k;
			value = v;
			return true;
		}

		#endregion
	}
}