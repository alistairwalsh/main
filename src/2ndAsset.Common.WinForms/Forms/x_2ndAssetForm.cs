/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

using Solder.Framework;
using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms.Presentation;

using Message = Solder.Framework.Message;

namespace _2ndAsset.Common.WinForms.Forms
{
	public class x_2ndAssetForm : _2ndAssetForm, IFullView
	{
		#region Constructors/Destructors

		public x_2ndAssetForm()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly IDictionary<Uri, Type> uriToControlTypes = new Dictionary<Uri, Type>();
		private object document;

		#endregion

		#region Properties/Indexers/Events

		public IFullView FullView
		{
			get
			{
				return this;
			}
		}

		bool IFullView.IsViewDirty
		{
			get
			{
				return this.CoreIsDirty;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected IDictionary<Uri, Type> UriToControlTypes
		{
			get
			{
				return this.uriToControlTypes;
			}
		}

		string IFullView.StatusText
		{
			get
			{
				return this.CoreStatus;
			}
			set
			{
				this.CoreStatus = value;
			}
		}

		string IFullView.ViewText
		{
			get
			{
				return this.CoreText;
			}
			set
			{
				this.CoreText = value;
			}
		}

		#endregion

		#region Methods/Operators

		private static MessageBoxIcon MapFromSeverity(Severity severity)
		{
			switch (severity)
			{
				case Severity.None:
					return MessageBoxIcon.None;
				case Severity.Information:
					return MessageBoxIcon.Information;
				case Severity.Warning:
					return MessageBoxIcon.Warning;
				case Severity.Error:
					return MessageBoxIcon.Error;
				case Severity.Hit:
					return MessageBoxIcon.Hand;
				case Severity.Debug:
					return MessageBoxIcon.Exclamation;
				default:
					return MessageBoxIcon.Stop;
			}
		}

		void IFullView.CloseView(bool? result)
		{
			if ((object)result != null)
				this.DialogResult = (bool)result ? DialogResult.OK : DialogResult.Cancel;

			this.Close(); // direct
		}

		/*TFullView IFullView.CreateView<TFullView>(Uri viewUri)
		{
			return (TFullView)this.FullView.CreateView(viewUri);
		}*/

		/*IFullView IFullView.CreateView(Uri viewUri)
		{
			IFullView view;

			if ((object)viewUri == null)
				throw new ArgumentNullException("viewUri");

			view = this.GetFormFromUri(viewUri);

			return view;
		}*/

		/*void IFullView.DestroyView(IFullView view)
		{
			using (x_2ndAssetForm form = (x_2ndAssetForm)view)
			{
				form.Close();
				// make sure its disposed
			}
		}*/

		object IFullView.DispatchControllerAction(IPartialView partialView, Uri controllerActionUri, object context)
		{
			return this.OnDispatchControllerAction(partialView, controllerActionUri, context);
		}

		private x_2ndAssetForm GetFormFromUri(Uri viewUri)
		{
			x_2ndAssetForm form;
			Type controlType;

			if ((object)viewUri == null)
				throw new ArgumentNullException("viewUri");

			if (!this.UriToControlTypes.TryGetValue(viewUri, out controlType))
				throw new InvalidOperationException(string.Format("{0}", viewUri));

			form = (x_2ndAssetForm)Activator.CreateInstance(controlType);

			return form;
		}

		protected virtual object OnDispatchControllerAction(IPartialView partialView, Uri controllerActionUri, object context)
		{
			throw new NotImplementedException(string.Format("OnDispatchControllerAction must be overriden."));
		}

		void IFullView.RefreshView()
		{
			this.CoreRefreshControlState();
		}

		bool IFullView.ShowAlert(string text, Severity severity = Severity.None)
		{
			DialogResult dialogResult;

			dialogResult = MessageBox.Show(this, text, ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product, MessageBoxButtons.OK, MapFromSeverity(severity));

			return dialogResult == DialogResult.OK;
		}

		TObject IFullView.ShowAsync<TObject>(string text, Func<object, TObject> asyncCallback, object asyncParameter)
		{
			DialogResult dialogResult;
			TObject asyncResult;
			bool asyncWasCanceled;
			Exception asyncExceptionOrNull;

			if ((object)asyncCallback == null)
				throw new ArgumentNullException("asyncCallback");

			this.FullView.StatusText = "Asynchronous operation started...";

			dialogResult = BackgroundTaskForm.Show<TObject>(this, text, o =>
																		{
																			//Thread.Sleep(1000);
																			return asyncCallback(asyncParameter);
																		}, null, out asyncWasCanceled, out asyncExceptionOrNull, out asyncResult);

			if (asyncWasCanceled || dialogResult == DialogResult.Cancel)
				this.Close(); // direct

			if ((object)asyncExceptionOrNull != null)
			{
				if (ExecutableApplicationFascade.Current.HookUnhandledExceptionEvents)
					ExecutableApplicationFascade.Current.ShowNestedExceptionsAndThrowBrickAtProcess(asyncExceptionOrNull);
				// should never reach this point
			}

			this.FullView.StatusText = "Asynchronous operation completed successfully.";

			return asyncResult;
		}

		bool? IFullView.ShowAttempt(string text, bool ignorable, Severity severity = Severity.None)
		{
			DialogResult dialogResult;

			dialogResult = MessageBox.Show(this, text, ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product, ignorable ? MessageBoxButtons.AbortRetryIgnore : MessageBoxButtons.RetryCancel, MapFromSeverity(severity));

			if (ignorable)
				return dialogResult == DialogResult.Abort ? null : (bool?)(dialogResult == DialogResult.Retry);
			else
				return dialogResult == DialogResult.Retry;
		}

		bool IFullView.ShowConfirm(string text, Severity severity = Severity.None)
		{
			DialogResult dialogResult;

			dialogResult = MessageBox.Show(this, text, ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product, MessageBoxButtons.OKCancel, MapFromSeverity(severity));

			return dialogResult == DialogResult.OK;
		}

		bool? IFullView.ShowMessages(IEnumerable<Message> messages, string text, bool cancelable)
		{
			DialogResult dialogResult;

			using (MessageForm messageForm = new MessageForm())
			{
				messageForm.CoreText = string.Format("Message List - {0} Studio", ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product);
				messageForm.Message = text;
				messageForm.Messages = messages;
				messageForm.IsCancelAllowed = cancelable;
				dialogResult = messageForm.ShowDialog(this);
			}

			return dialogResult == DialogResult.OK;
		}

		bool? IFullView.ShowQuestion(string text, bool cancelable, Severity severity = Severity.None)
		{
			DialogResult dialogResult;

			dialogResult = MessageBox.Show(this, text, ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product, cancelable ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo, MapFromSeverity(severity));

			if (cancelable)
				return dialogResult == DialogResult.Cancel ? null : (bool?)(dialogResult == DialogResult.Yes);
			else
				return dialogResult == DialogResult.Yes;
		}

		bool IFullView.ShowView(Uri viewUri)
		{
			DialogResult dialogResult;

			if ((object)viewUri == null)
				throw new ArgumentNullException("viewUri");

			using (x_2ndAssetForm form = this.GetFormFromUri(viewUri))
				dialogResult = form.ShowDialog(this);

			return dialogResult == DialogResult.OK;
		}

		void IFullView.ShowVoidAsync(string text, Action asyncCallback)
		{
			this.FullView.ShowAsync<object>(text, (p) =>
												{
													asyncCallback();
													return null;
												}, null);
		}

		bool IFullView.TryGetOpenFilePath(out string filePath)
		{
			DialogResult dialogResult;

			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Title = string.Format("Open - {0} Studio", ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product);
				openFileDialog.FileName = filePath = null;
				dialogResult = openFileDialog.ShowDialog(this);

				if (dialogResult != DialogResult.OK ||
					DataTypeFascade.Instance.IsNullOrWhiteSpace(openFileDialog.FileName))
					return false;

				filePath = Path.GetFullPath(openFileDialog.FileName);
				return true;
			}
		}

		bool IFullView.TryGetSaveFilePath(out string filePath)
		{
			DialogResult dialogResult;

			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.Title = string.Format("Save As - {0} Studio", ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product);
				saveFileDialog.FileName = filePath = null;
				dialogResult = saveFileDialog.ShowDialog(this);

				if (dialogResult != DialogResult.OK ||
					DataTypeFascade.Instance.IsNullOrWhiteSpace(saveFileDialog.FileName))
					return false;

				filePath = Path.GetFullPath(saveFileDialog.FileName);
				return true;
			}
		}

		#endregion
	}
}