/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers
{
	public sealed class MetadataSettingsSlaveController : SlaveController<IMetadataSettingsPartialView>
	{
		#region Constructors/Destructors

		public MetadataSettingsSlaveController()
		{
		}

		#endregion

		#region Methods/Operators

		public void ApplyDocumentToView(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.TableConfiguration != null &&
				(object)obfuscationConfiguration.TableConfiguration.ColumnConfigurations != null)
			{
				foreach (ColumnConfiguration columnConfiguration in obfuscationConfiguration.TableConfiguration.ColumnConfigurations)
					this.View.AddMetaColumnSpecView(columnConfiguration.ColumnName, columnConfiguration.ObfuscationStrategyAqtn);
			}
		}

		public void ApplyViewToDocument(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.TableConfiguration = obfuscationConfiguration.TableConfiguration ?? new TableConfiguration();

			foreach (IMetaColumnSpecListView metaColumnSpecView in this.View.MetaColumnSpecListViews)
			{
				ColumnConfiguration columnConfiguration;

				columnConfiguration = new ColumnConfiguration()
									{
										ColumnName = metaColumnSpecView.ColumnName ?? string.Empty,
										ObfuscationStrategyAqtn = metaColumnSpecView.ObfuscationStrategyAqtn
									};

				obfuscationConfiguration.TableConfiguration.ColumnConfigurations.Add(columnConfiguration);
			}
		}

		public override void InitializeView(IMetadataSettingsPartialView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);
		}

		public void RefreshUpstream()
		{
			this.EmitPresentationEvent(Constants.RefreshUpstreamMetadataColumnsEventUri, null);
		}

		#endregion
	}
}