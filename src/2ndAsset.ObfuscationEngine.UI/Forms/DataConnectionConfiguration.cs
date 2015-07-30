/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

using Microsoft.Data.ConnectionUI;

namespace _2ndAsset.ObfuscationEngine.UI.Forms
{
	/// <summary>
	/// Provide a default implementation for the storage of DataConnection Dialog UI configuration.
	/// </summary>
	public class DataConnectionConfiguration : IDataConnectionConfiguration
	{
		#region Constructors/Destructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="path"> Configuration file path. </param>
		public DataConnectionConfiguration(string path)
		{
			if (!String.IsNullOrEmpty(path))
				this.fullFilePath = Path.GetFullPath(Path.Combine(path, configFileName));
			else
				this.fullFilePath = Path.Combine(Environment.CurrentDirectory, configFileName);
			if (!String.IsNullOrEmpty(this.fullFilePath) && File.Exists(this.fullFilePath))
				this.xDoc = XDocument.Load(this.fullFilePath);
			else
			{
				this.xDoc = new XDocument();
				this.xDoc.Add(new XElement("ConnectionDialog", new XElement("DataSourceSelection")));
			}

			this.RootElement = this.xDoc.Root;
		}

		#endregion

		#region Fields/Constants

		private const string configFileName = @"DataConnection.xml";
		// Available data providers: 
		private IDictionary<string, DataProvider> dataProviders;
		// Available data sources:
		private IDictionary<string, DataSource> dataSources;
		private string fullFilePath = null;
		private XElement rootElement;
		private XDocument xDoc = null;

		#endregion

		#region Properties/Indexers/Events

		public XElement RootElement
		{
			get
			{
				return this.rootElement;
			}
			set
			{
				this.rootElement = value;
			}
		}

		#endregion

		#region Methods/Operators

		public static bool TryGetDatabaseConnection(ref Type connectionType, ref string connectionString)
		{
			DialogResult dialogResult;
			Type _connectionType = connectionType;

			using (DataConnectionDialog dataConnectionDialog = new DataConnectionDialog())
			{
				DataConnectionConfiguration dataConnectionConfiguration;

				dataConnectionConfiguration = new DataConnectionConfiguration(null);
				dataConnectionConfiguration.LoadConfiguration(dataConnectionDialog);
				//dataConnectionDialog.ConnectionString = connectionString ?? string.Empty;

				/*var useThisOne = dataConnectionDialog.DataSources.Where(ds => (object)ds.DefaultProvider != null && ds.DefaultProvider.TargetConnectionType == _connectionType).Select(ds => new { DataSource = ds, DataProvider = ds.DefaultProvider }).SingleOrDefault();

				if ((object)useThisOne != null)
				{
					dataConnectionDialog.SelectedDataProvider = useThisOne.DataProvider;
					dataConnectionDialog.SelectedDataSource = useThisOne.DataSource;
					dataConnectionDialog.ConnectionString = connectionString ?? string.Empty;
				}*/

				dialogResult = DataConnectionDialog.Show(dataConnectionDialog);

				if (dialogResult == DialogResult.OK)
				{
					connectionString = dataConnectionDialog.ConnectionString;

					if ((object)dataConnectionDialog.SelectedDataSource != null &&
						(object)dataConnectionDialog.SelectedDataSource.DefaultProvider != null)
						connectionType = dataConnectionDialog.SelectedDataSource.DefaultProvider.TargetConnectionType;
				}
			}

			return dialogResult == DialogResult.OK;
		}

		public string GetSelectedProvider()
		{
			try
			{
				XElement xElem = this.RootElement.Element("DataSourceSelection");
				XElement providerElem = xElem.Element("SelectedProvider");
				if (providerElem != null)
					return providerElem.Value as string;
			}
			catch
			{
				return null;
			}
			return null;
		}

		public string GetSelectedSource()
		{
			try
			{
				XElement xElem = this.RootElement.Element("DataSourceSelection");
				XElement sourceElem = xElem.Element("SelectedSource");
				if (sourceElem != null)
					return sourceElem.Value as string;
			}
			catch
			{
				return null;
			}
			return null;
		}

		public void LoadConfiguration(DataConnectionDialog dialog)
		{
			dialog.DataSources.Add(DataSource.SqlDataSource);
			//dialog.DataSources.Add(DataSource.SqlFileDataSource);
			//dialog.DataSources.Add(DataSource.OracleDataSource);
			dialog.DataSources.Add(DataSource.AccessDataSource);
			dialog.DataSources.Add(DataSource.OdbcDataSource);
			//dialog.DataSources.Add(SqlCe.SqlCeDataSource);

			dialog.UnspecifiedDataSource.Providers.Add(DataProvider.SqlDataProvider);
			//dialog.UnspecifiedDataSource.Providers.Add(DataProvider.OracleDataProvider);
			dialog.UnspecifiedDataSource.Providers.Add(DataProvider.OleDBDataProvider);
			dialog.UnspecifiedDataSource.Providers.Add(DataProvider.OdbcDataProvider);
			dialog.DataSources.Add(dialog.UnspecifiedDataSource);

			this.dataSources = new Dictionary<string, DataSource>();
			this.dataSources.Add(DataSource.SqlDataSource.Name, DataSource.SqlDataSource);
			//this.dataSources.Add(DataSource.SqlFileDataSource.Name, DataSource.SqlFileDataSource);
			//this.dataSources.Add(DataSource.OracleDataSource.Name, DataSource.OracleDataSource);
			this.dataSources.Add(DataSource.AccessDataSource.Name, DataSource.AccessDataSource);
			this.dataSources.Add(DataSource.OdbcDataSource.Name, DataSource.OdbcDataSource);
			//this.dataSources.Add(SqlCe.SqlCeDataSource.Name, SqlCe.SqlCeDataSource);
			this.dataSources.Add(dialog.UnspecifiedDataSource.DisplayName, dialog.UnspecifiedDataSource);

			this.dataProviders = new Dictionary<string, DataProvider>();
			this.dataProviders.Add(DataProvider.SqlDataProvider.Name, DataProvider.SqlDataProvider);
			//this.dataProviders.Add(DataProvider.OracleDataProvider.Name, DataProvider.OracleDataProvider);
			this.dataProviders.Add(DataProvider.OleDBDataProvider.Name, DataProvider.OleDBDataProvider);
			this.dataProviders.Add(DataProvider.OdbcDataProvider.Name, DataProvider.OdbcDataProvider);
			//this.dataProviders.Add(SqlCe.SqlCeDataProvider.Name, SqlCe.SqlCeDataProvider);

			DataSource ds = null;
			string dsName = this.GetSelectedSource();
			if (!String.IsNullOrEmpty(dsName) && this.dataSources.TryGetValue(dsName, out ds))
				dialog.SelectedDataSource = ds;

			DataProvider dp = null;
			string dpName = this.GetSelectedProvider();
			if (!String.IsNullOrEmpty(dpName) && this.dataProviders.TryGetValue(dpName, out dp))
				dialog.SelectedDataProvider = dp;
		}

		public void SaveConfiguration(DataConnectionDialog dcd)
		{
			if (dcd.SaveSelection)
			{
				DataSource ds = dcd.SelectedDataSource;
				if (ds != null)
				{
					if (ds == dcd.UnspecifiedDataSource)
						this.SaveSelectedSource(ds.DisplayName);
					else
						this.SaveSelectedSource(ds.Name);
				}
				DataProvider dp = dcd.SelectedDataProvider;
				if (dp != null)
					this.SaveSelectedProvider(dp.Name);

				this.xDoc.Save(this.fullFilePath);
			}
		}

		public void SaveSelectedProvider(string provider)
		{
			if (!String.IsNullOrEmpty(provider))
			{
				try
				{
					XElement xElem = this.RootElement.Element("DataSourceSelection");
					XElement sourceElem = xElem.Element("SelectedProvider");
					if (sourceElem != null)
						sourceElem.Value = provider;
					else
						xElem.Add(new XElement("SelectedProvider", provider));
				}
				catch
				{
				}
			}
		}

		public void SaveSelectedSource(string source)
		{
			if (!String.IsNullOrEmpty(source))
			{
				try
				{
					XElement xElem = this.RootElement.Element("DataSourceSelection");
					XElement sourceElem = xElem.Element("SelectedSource");
					if (sourceElem != null)
						sourceElem.Value = source;
					else
						xElem.Add(new XElement("SelectedSource", source));
				}
				catch
				{
				}
			}
		}

		#endregion
	}
}