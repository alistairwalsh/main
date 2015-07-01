/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Commercial software distribution. May contain open source.
*/

namespace _2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool.Config
{
	public class TwoPartName : OnePartName
	{
		#region Constructors/Destructors

		public TwoPartName()
		{
		}

		#endregion

		#region Fields/Constants

		private string schemaName;

		#endregion

		#region Properties/Indexers/Events

		public string SchemaName
		{
			get
			{
				return this.schemaName;
			}
			set
			{
				this.schemaName = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override string ToString()
		{
			return string.Format("[{0}].[{1}]", this.SchemaName, this.ObjectName);
		}

		#endregion
	}
}