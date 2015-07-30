/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace _2ndAsset.ObfuscationEngine.Core.Config
{
	public class ConfigurationObjectCollection<TConfigurationObject> : Collection<TConfigurationObject>, IConfigurationObjectCollection<TConfigurationObject>
		where TConfigurationObject : IConfigurationObject
	{
		#region Constructors/Destructors

		public ConfigurationObjectCollection(IConfigurationObject site)
		{
			if ((object)site == null)
				throw new ArgumentNullException("site");

			this.site = site;
		}

		#endregion

		#region Fields/Constants

		private readonly IConfigurationObject site;

		#endregion

		#region Properties/Indexers/Events

		[JsonIgnore]
		public new IConfigurationObject Site
		{
			get
			{
				return this.site;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Removes all elements from the collection.
		/// </summary>
		protected override void ClearItems()
		{
			foreach (TConfigurationObject item in this.Items)
			{
				item.Surround = null;
				item.Parent = null;
			}

			base.ClearItems();
		}

		/// <summary>
		/// Inserts an element into the collection at the specified index.
		/// </summary>
		/// <param name="index"> The zero-based index at which item should be inserted. </param>
		/// <param name="item"> The object to insert. The value can be null for reference types. </param>
		protected override void InsertItem(int index, TConfigurationObject item)
		{
			if ((object)item == null)
				throw new ArgumentNullException("item");

			item.Surround = this;
			item.Parent = this.Site;

			base.InsertItem(index, item);
		}

		/// <summary>
		/// Removes the element at the specified index of the collection.
		/// </summary>
		/// <param name="index"> The zero-based index of the element to remove. </param>
		protected override void RemoveItem(int index)
		{
			TConfigurationObject item;

			item = base[index];

			if ((object)item == null)
			{
				item.Surround = null;
				item.Parent = null;
			}

			base.RemoveItem(index);
		}

		/// <summary>
		/// Replaces the element at the specified index.
		/// </summary>
		/// <param name="index"> The zero-based index of the element to replace. </param>
		/// <param name="item"> The new value for the element at the specified index. The value can be null for reference types. </param>
		protected override void SetItem(int index, TConfigurationObject item)
		{
			if ((object)item == null)
				throw new ArgumentNullException("item");

			item.Surround = this;
			item.Parent = this.Site;

			base.SetItem(index, item);
		}

		#endregion
	}
}