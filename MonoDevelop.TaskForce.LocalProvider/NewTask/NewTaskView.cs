// 
// NewTaskView.cs
//  
// Author:
//       Anirudh Sanjeev <anirudh@anirudhsanjeev.org>
// 
// Copyright (c) 2009 Anirudh Sanjeev
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



namespace MonoDevelop.TaskForce.LocalProvider.NewTask
{
	
	
	public class NewTaskView : AbstractViewContent
	{
		protected NewTaskWidget widget;
		
		protected LogUtil log;
		
		public override Widget Control {
			get {
				return widget;
			}
		}
		
		public override void Load (string fileName)
		{
			Console.WriteLine("ALERT: Load callled");
		}
		
		public override string ContentName {
			get {
				return "New Task";
			}
			set {
				base.ContentName = value;
			}
		}

		public void SetProviderNode(ProviderData data)
		{
			widget.SetProviderNode(data);
			widget.SetView(this);
		}
		
		public NewTaskView() : base()
		{
			log = new LogUtil("NewTaskView");
			log.SetHash(this);
			
			log.DEBUG("Creating widget");
			widget = new NewTaskWidget();
			// subscribe to the event when a new task is created
			
			log.DEBUG("Creating delegate");
		}
		
		public override string StockIconId {
			get {
				return Gtk.Stock.Apply;
			}
		}
		
		public void Dispose ()
		{
			IdeApp.Workbench.GetDocument(this.ContentName).Close();
			if(widget != null)
			{
				widget.Destroy ();
			}
			base.Dispose();
		}
		
		


	}
}
