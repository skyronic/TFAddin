// 
// ViewContents.cs
//  
// Author:
//       Anirudh Sanjeev <anirudh@anirudhsanjeev.org>
// 
// Copyright (c) 2009 Anirudh
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using MonoDevelop.Ide.Gui.Content;
using MonoDevelop.Ide.Gui;
using MonoDevelop.TaskForce.Data;
using MonoDevelop.TaskForce.Gui.TaskPad;
using MonoDevelop.TaskForce.Utilities;
using Gtk;

namespace MonoDevelop.TaskForce.LocalProvider.TaskWidgets
{
	public class NewTaskView : AbstractViewContent
	{
		protected LogUtil log;
		protected NewTaskWidget widget;

		public override Widget Control {
			get { return widget; }
		}

		public override string ContentName {
			get { return base.ContentName; }
			set { base.ContentName = value; }
		}

		public override bool IsFile {
			get { return base.IsFile; }
		}

		/// <summary>
		/// TODO: What the hell is this function supposed to do?
		/// </summary>
		/// <param name="fileName">
		/// A <see cref="System.String"/>
		/// </param>
		public override void Load (string fileName)
		{
			//throw new System.NotImplementedException ();
		}


		public override bool IsReadOnly {
			get { return base.IsReadOnly; }
		}

		public override void Dispose ()
		{
			base.Dispose ();
		}


		/// <summary>
		/// Don't run this class without specifying the provider node
		/// 
		/// No other constructor exists
		/// </summary>
		/// <param name="providerNode">
		/// A <see cref="ProviderData"/>
		/// </param>
		public NewTaskView (ProviderData providerNode)
		{
			this.ContentName = "New Task";
			log = new LogUtil ("NewTaskView");

			widget = new NewTaskWidget ();
			widget.ProviderNode = providerNode;
			widget.ViewContent = this;

			widget.ShowAll ();


		}
	}

	public class EditTaskView : AbstractViewContent
	{
		protected LogUtil log;
		protected EditTaskWidget widget;

		public override Widget Control {
			get { return widget; }
		}

		public override string ContentName {
			get { return base.ContentName; }
			set { base.ContentName = value; }
		}

		public override bool IsFile {
			get { return false; }
		}

		/// <summary>
		/// TODO: What the hell is this function supposed to do?
		/// </summary>
		/// <param name="fileName">
		/// A <see cref="System.String"/>
		/// </param>
		public override void Load (string fileName)
		{
			//throw new System.NotImplementedException ();
		}


		public override bool IsReadOnly {
			get { return base.IsReadOnly; }
		}

		public override void Dispose ()
		{
			base.Dispose ();
		}


		/// <summary>
		/// Don't run this class without specifying the provider node
		/// EDIT: and the TaskData or something like that.
		/// No other constructor exists
		/// </summary>
		/// <param name="providerNode">
		/// A <see cref="ProviderData"/>
		/// </param>
		/// <param name="seedTask">
		/// 
		/// </param>
		public EditTaskView (ProviderData providerNode, TaskData seedTask)
		{
			this.ContentName = "Edit Task";
			log = new LogUtil ("NewTaskView");

			widget = new EditTaskWidget ();
			widget.ProviderNode = providerNode;
			widget.ViewContent = this;
			widget.EditTarget = seedTask;
			widget.PopulateForm ();


			widget.ShowAll ();


		}
	}

}
