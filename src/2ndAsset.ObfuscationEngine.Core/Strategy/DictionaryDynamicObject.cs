/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

using Solder.Framework.Utilities;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	[Serializable]
	public class DictionaryDynamicObject : DynamicObject, INotifyPropertyChanged
	{
		#region Constructors/Destructors

		public DictionaryDynamicObject()
			: this(new Dictionary<string, object>())
		{
		}

		public DictionaryDynamicObject(IDictionary<string, object> dictionary)
		{
			this.dictionary = dictionary;
		}

		#endregion

		#region Fields/Constants

		private readonly IDictionary<string, object> dictionary;

		#endregion

		#region Properties/Indexers/Events

		private event PropertyChangedEventHandler PropertyChanged;

		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				this.PropertyChanged += value;
			}
			remove
			{
				this.PropertyChanged -= value;
			}
		}

		private IDictionary<string, object> Dictionary
		{
			get
			{
				return this.dictionary;
			}
		}

		#endregion

		#region Methods/Operators

		public override IEnumerable<string> GetDynamicMemberNames()
		{
			foreach (string key in this.Dictionary.Keys)
				yield return key;
		}

		private void OnAllPropertiesChanged()
		{
			this.OnPropertyChanged(null);
		}

		private void OnPropertyChanged(string propertyName)
		{
			if ((object)this.PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			if (this.Dictionary.TryGetValue(binder.Name, out result))
				return true;

			return base.TryGetMember(binder, out result);
		}

		public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			object value;
			Delegate method;

			if (!this.Dictionary.TryGetValue(binder.Name, out value))
				return base.TryInvokeMember(binder, args, out result);

			method = value as Delegate;

			if ((object)method == null)
				return base.TryInvokeMember(binder, args, out result);

			result = method.DynamicInvoke(args);
			return true;
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			object thisValue;

			if (this.Dictionary.TryGetValue(binder.Name, out thisValue))
			{
				if (!DataTypeFascade.Instance.ObjectsEqualValueSemantics(thisValue, value))
				{
					this.Dictionary.Remove(binder.Name);

					if ((object)value != null)
						this.Dictionary.Add(binder.Name, value);

					this.OnPropertyChanged(binder.Name);
				}
			}
			else
			{
				this.Dictionary.Add(binder.Name, value);
				this.OnPropertyChanged(binder.Name);
			}

			return true;
		}

		#endregion
	}
}