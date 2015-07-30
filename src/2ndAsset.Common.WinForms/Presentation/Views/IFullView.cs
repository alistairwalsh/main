/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using Solder.Framework;

namespace _2ndAsset.Common.WinForms.Presentation.Views
{
	public interface IFullView : IBaseView
	{
		#region Properties/Indexers/Events

		bool IsViewDirty
		{
			get;
		}

		string StatusText
		{
			get;
			set;
		}

		string ViewText
		{
			get;
			set;
		}

		#endregion

		#region Methods/Operators

		void CloseView(bool? result);

		//IFullView CreateView(Uri viewUri);

		//TFullView CreateView<TFullView>(Uri viewUri) where TFullView : IFullView;

		//void DestroyView(IFullView view);

		void RefreshView();

		bool ShowAlert(string text, Severity severity = Severity.None);

		TObject ShowAsync<TObject>(string text, Func<object, TObject> asyncCallback, object asyncParameter);

		bool? ShowAttempt(string text, bool ignorable, Severity severity = Severity.None);

		bool ShowConfirm(string text, Severity severity = Severity.None);

		bool? ShowMessages(IEnumerable<Message> messages, string text, bool cancelable);

		bool? ShowQuestion(string text, bool cancelable, Severity severity = Severity.None);

		bool ShowView(Uri viewUri);

		void ShowVoidAsync(string text, Action asyncCallback);

		bool TryGetOpenFilePath(out string filePath);

		bool TryGetSaveFilePath(out string filePath);

		#endregion
	}
}