/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

namespace _2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool.Config
{
	public class FourPartName : ThreePartName
	{
		#region Constructors/Destructors

		public FourPartName()
		{
		}

		#endregion

		#region Fields/Constants

		private string serverName;

		#endregion

		#region Properties/Indexers/Events

		public string ServerName
		{
			get
			{
				return this.serverName;
			}
			set
			{
				this.serverName = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override string ToString()
		{
			return string.Format("[{0}].[{1}].[{2}].[{3}]", this.ServerName, this.DatabaseName, this.SchemaName, this.ObjectName);
		}

		#endregion
	}
}