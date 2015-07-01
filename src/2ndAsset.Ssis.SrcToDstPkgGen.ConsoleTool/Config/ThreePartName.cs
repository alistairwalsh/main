/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Commercial software distribution. May contain open source.
*/

using System;

namespace _2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool.Config
{
	public class ThreePartName : TwoPartName
	{
		#region Constructors/Destructors

		public ThreePartName()
		{
		}

		#endregion

		#region Fields/Constants

		private string databaseName;

		#endregion

		#region Properties/Indexers/Events

		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
			set
			{
				this.databaseName = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override string ToString()
		{
			return string.Format("[{0}].[{1}].[{2}]", this.DatabaseName, this.SchemaName, this.ObjectName);
		}

		public string ToString(bool includeDatabaseName)
		{
			if (!includeDatabaseName)
				return base.ToString();
			else
				return this.ToString();
		}

		#endregion
	}
}