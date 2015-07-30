/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;
using System.Windows.Forms;

using Solder.Framework.Utilities;

namespace _2ndAsset.Common.WinForms.Controls
{
	public class BaseComboBox : ComboBox
	{
		#region Constructors/Destructors

		public BaseComboBox()
		{
		}

		#endregion

		#region Fields/Constants

		private bool isInputValid = true;
		private string valueType;

		#endregion

		#region Properties/Indexers/Events

		private bool IsInputValid
		{
			get
			{
				return this.isInputValid;
			}
			set
			{
				this.isInputValid = value;
			}
		}

		public override string Text
		{
			get
			{
				return (base.Text ?? string.Empty).ToUpper();
			}
			set
			{
				base.Text = (value ?? string.Empty).ToUpper();
			}
		}

		public string ValueType
		{
			get
			{
				return this.valueType;
			}
			set
			{
				this.valueType = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);

			// handle a strange TextBox anomoly per MSDN forums:			
			this.BeginInvoke((MethodInvoker)this.SelectAll);
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (char.IsLower(e.KeyChar))
				e.KeyChar = char.ToUpper(e.KeyChar);

			base.OnKeyPress(e);
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);

			this.CoreSetParentFormDirty(true);
		}

		protected override void OnSelectedItemChanged(EventArgs e)
		{
			base.OnSelectedItemChanged(e);

			this.CoreSetParentFormDirty(true);
		}

		protected override void OnSelectedValueChanged(EventArgs e)
		{
			base.OnSelectedValueChanged(e);

			this.CoreSetParentFormDirty(true);
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);

			this.CoreSetParentFormDirty(true);
		}

		protected override void OnValidating(CancelEventArgs e)
		{
			bool wasValid;
			bool isValid;

			base.OnValidating(e);

			wasValid = this.IsInputValid;

			if (this.CoreIsEmpty())
			{
				this.CoreInputValidation(true);
				this.IsInputValid = true;
			}
			else
			{
				if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.ValueType))
					isValid = this.IsInputValid = this.CoreIsValid();
				else
					isValid = this.IsInputValid = this.CoreIsValid(Type.GetType(this.ValueType, false) ?? typeof(string));

				if (!(!wasValid && !isValid)) // prevent weirdness
					this.CoreInputValidation(isValid);
			}

			e.Cancel = !this.IsInputValid;
		}

		#endregion
	}
}