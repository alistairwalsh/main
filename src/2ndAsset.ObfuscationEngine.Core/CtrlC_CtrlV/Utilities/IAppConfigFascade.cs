using System;
using System.Collections.Generic;

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities
{
	public interface IAppConfigFascade
	{
		#region Methods/Operators

		/// <summary>
		/// Gets the value of an app settings for the current application's default configuration. A ConfigurationErrorsException is thrown if the key does not exist.
		/// </summary>
		/// <typeparam name="TValue"> The type to convert the app settings value. </typeparam>
		/// <param name="key"> The key to get a value. </param>
		/// <returns> The app settings as type TValue. </returns>
		TValue GetAppSetting<TValue>(string key);

		/// <summary>
		/// Gets the value of an app settings for the current application's default configuration. A ConfigurationErrorsException is thrown if the key does not exist.
		/// </summary>
		/// <param name="valueType"> The type to convert the app settings value. </param>
		/// <param name="key"> The key to get a value. </param>
		/// <returns> The app settings as a string. </returns>
		object GetAppSetting(Type valueType, string key);

		/// <summary>
		/// Gets the value of a connection provider for the current application's default configuration. A ConfigurationErrorsException is thrown if the name does not exist.
		/// </summary>
		/// <param name="name"> The name to get a value. </param>
		/// <returns> The connection provider. </returns>
		string GetConnectionProvider(string name);

		/// <summary>
		/// Gets the value of a connection string for the current application's default configuration. A ConfigurationErrorsException is thrown if the name does not exist.
		/// </summary>
		/// <param name="name"> The name to get a value. </param>
		/// <returns> The connection string. </returns>
		string GetConnectionString(string name);

		/// <summary>
		/// Checks to see if an app settings key exists for the current application's default configuration.
		/// </summary>
		/// <param name="key"> The key to check. </param>
		/// <returns> A boolean value indicating the app setting key presence. </returns>
		bool HasAppSetting(string key);

		/// <summary>
		/// Checks to see if a connection string name exists for the current application's default configuration.
		/// </summary>
		/// <param name="name"> The name to check. </param>
		/// <returns> A boolean value indicating the connection string name presence. </returns>
		bool HasConnectionString(string name);

		/// <summary>
		/// Given a string array of command line arguments, this method will parse the arguments using a well know pattern match to obtain a loosely typed dictionary of key/multi-value pairs for use by applications.
		/// </summary>
		/// <param name="args"> The command line argument array to parse. </param>
		/// <returns> A loosely typed dictionary of key/multi-value pairs. </returns>
		IDictionary<string, IList<string>> ParseCommandLineArguments(string[] args);

		/// <summary>
		/// Given a string property, this method will parse the property using a well know pattern match to obtain an output key/value pair for use by applications.
		/// </summary>
		/// <param name="arg"> The property to parse. </param>
		/// <param name="key"> The output property key. </param>
		/// <param name="value"> The output property value. </param>
		/// <returns> A value indicating if the parse was successful or not. </returns>
		bool TryParseCommandLineArgumentProperty(string arg, out string key, out string value);

		#endregion
	}
}