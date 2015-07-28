/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Linq;

using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace _2ndAsset.Ssis.Components
{
	public abstract class CustomPropertyWrapper
	{
		#region Constructors/Destructors

		protected CustomPropertyWrapper(IDTSCustomPropertyCollection100 dtsCustomPropertyCollection100)
		{
			if ((object)dtsCustomPropertyCollection100 == null)
				throw new ArgumentNullException("dtsCustomPropertyCollection100");

			this.dtsCustomPropertyCollection100 = dtsCustomPropertyCollection100;
		}

		#endregion

		#region Fields/Constants

		private readonly IDTSCustomPropertyCollection100 dtsCustomPropertyCollection100;

		#endregion

		#region Properties/Indexers/Events

		protected IDTSCustomPropertyCollection100 DtsCustomPropertyCollection100
		{
			get
			{
				return this.dtsCustomPropertyCollection100;
			}
		}

		#endregion

		#region Methods/Operators

		protected T GetProperty<T>(string propertyName)
		{
			IDTSCustomProperty100 dtsCustomProperty100;

			if ((object)propertyName == null)
				throw new ArgumentNullException("propertyName");

			if (!this.HasProperty(propertyName))
				throw new InvalidOperationException("Aw snap.");

			dtsCustomProperty100 = this.DtsCustomPropertyCollection100[propertyName];

			return (object)dtsCustomProperty100.Value == null ? default(T) : (T)dtsCustomProperty100.Value;
		}

		protected bool HasProperty(string propertyName)
		{
			if ((object)propertyName == null)
				throw new ArgumentNullException("propertyName");

			return this.DtsCustomPropertyCollection100.Cast<IDTSCustomProperty100>().Any(c => c.Name == propertyName);
		}

		protected void LetProperty<T>(string propertyName, T propertyValue,
			string propertyDesc, bool propertyHide)
		{
			this.LetProperty<T>(propertyName, propertyValue, propertyDesc, propertyHide, typeof(T).AssemblyQualifiedName, null);
		}

		protected void LetProperty<T>(string propertyName, T propertyValue,
			string propertyDesc, bool propertyHide, string typeConverter, string uiTypeEditor)
		{
			IDTSCustomProperty100 dtsCustomProperty100;

			if ((object)propertyName == null)
				throw new ArgumentNullException("propertyName");

			if ((object)propertyDesc == null)
				throw new ArgumentNullException("propertyDesc");

			if (this.HasProperty(propertyName))
				throw new InvalidOperationException("Aw snap.");

			dtsCustomProperty100 = this.DtsCustomPropertyCollection100.New();
			dtsCustomProperty100.Name = propertyName;
			dtsCustomProperty100.Description = propertyDesc;
			dtsCustomProperty100.TypeConverter = typeConverter ?? string.Empty;
			dtsCustomProperty100.UITypeEditor = uiTypeEditor ?? string.Empty;
			dtsCustomProperty100.Value = propertyValue;
			dtsCustomProperty100.EncryptionRequired = propertyHide;
		}

		protected void NilProperty(string propertyName)
		{
			if ((object)propertyName == null)
				throw new ArgumentNullException("propertyName");

			if (this.HasProperty(propertyName))
				this.DtsCustomPropertyCollection100.RemoveObjectByIndex(propertyName);
		}

		protected void SetProperty<T>(string propertyName, T propertyValue)
		{
			IDTSCustomProperty100 dtsCustomProperty100;

			if ((object)propertyName == null)
				throw new ArgumentNullException("propertyName");

			if (!this.HasProperty(propertyName))
				throw new InvalidOperationException("Aw snap.");

			dtsCustomProperty100 = this.DtsCustomPropertyCollection100[propertyName];
			dtsCustomProperty100.Value = propertyValue;
		}

		#endregion
	}
}