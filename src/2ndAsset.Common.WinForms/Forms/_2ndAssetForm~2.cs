/*
	Copyright �2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

using TextMetal.Middleware.Common;
using TextMetal.Middleware.Common.Utilities;

using _2ndAsset.Common.WinForms.Presentation;

using Message = TextMetal.Middleware.Common.Message;

namespace _2ndAsset.Common.WinForms.Forms
{
	public class _2ndAssetForm<TFullView, TController> : _2ndAssetForm, IFullView
		where TFullView : class, IFullView
		where TController : class, IBaseController<TFullView>, new()
	{
		#region Constructors/Destructors

		public _2ndAssetForm()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly TController controller = new TController();
		private readonly IDictionary<Uri, Type> uriToControlTypes = new Dictionary<Uri, Type>();
		private object document;

		#endregion

		#region Properties/Indexers/Events

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TController Controller
		{
			get
			{
				return this.controller;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TFullView FullView
		{
			get
			{
				return ((object)this) as TFullView;
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

		protected override void CoreSetup()
		{
			base.CoreSetup();

			if ((object)this.FullView != null)
				this.Controller.InitializeView(this.FullView);
		}

		protected override void CoreShown()
		{
			base.CoreShown();

			if ((object)this.FullView != null)
				this.Controller.ReadyView();
		}

		protected override void CoreTeardown()
		{
			if ((object)this.FullView != null)
				this.Controller.TerminateView();

			base.CoreTeardown();
		}

		object IFullView.DispatchControllerAction(IPartialView partialView, Uri controllerActionUri, object context)
		{
			return this.Controller.DispatchAction(partialView, controllerActionUri, context);
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
			Type controlType;

			if ((object)viewUri == null)
				throw new ArgumentNullException("viewUri");

			if (!this.UriToControlTypes.TryGetValue(viewUri, out controlType))
				throw new InvalidOperationException(string.Format("{0}", viewUri));

			using (_2ndAssetForm form = (_2ndAssetForm)Activator.CreateInstance(controlType))
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