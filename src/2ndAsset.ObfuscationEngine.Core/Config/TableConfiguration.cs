/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using Solder.Framework;

namespace _2ndAsset.ObfuscationEngine.Core.Config
{
	public class TableConfiguration : ConfigurationObject
	{
		#region Constructors/Destructors

		public TableConfiguration()
		{
			this.columnConfigurations = new ConfigurationObjectCollection<ColumnConfiguration>(this);
		}

		#endregion

		#region Fields/Constants

		private readonly ConfigurationObjectCollection<ColumnConfiguration> columnConfigurations;

		#endregion

		#region Properties/Indexers/Events

		public ConfigurationObjectCollection<ColumnConfiguration> ColumnConfigurations
		{
			get
			{
				return this.columnConfigurations;
			}
		}

		[JsonIgnore]
		public new ObfuscationConfiguration Parent
		{
			get
			{
				return (ObfuscationConfiguration)base.Parent;
			}
			set
			{
				base.Parent = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override IEnumerable<Message> Validate()
		{
			List<Message> messages;
			int index;

			messages = new List<Message>();

			// check for duplicate columns
			var columnNameSums = this.ColumnConfigurations.GroupBy(c => c.ColumnName)
				.Select(cl => new
							{
								ColumnName = cl.First().ColumnName,
								Count = cl.Count()
							}).Where(cl => cl.Count > 1);

			if (columnNameSums.Any())
				messages.AddRange(columnNameSums.Select(c => NewError(string.Format("Table configuration with duplicate column configuration found: '{0}'.", c.ColumnName))).ToArray());

			index = 0;
			foreach (ColumnConfiguration columnConfiguration in this.ColumnConfigurations)
				messages.AddRange(columnConfiguration.Validate(index++));

			return messages;
		}

		#endregion
	}
}