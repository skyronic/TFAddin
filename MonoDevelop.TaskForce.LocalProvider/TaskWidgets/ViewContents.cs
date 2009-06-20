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
		

		public override Widget Control {
					get {
						throw new System.NotImplementedException();
					}
				}

		public override string ContentName {
			get {
				return base.ContentName;
			}
			set {
				base.ContentName = value;
			}
		}

		public override bool IsFile {
			get {
				return base.IsFile;
			}
		}

		public override bool IsReadOnly {
			get {
				return base.IsReadOnly;
			}
		}

		public override void Dispose ()
		{
			base.Dispose();
		}
		
		public NewTaskView()
		{
			this.ContentName = "New Task";
			log = new LogUtil("NewTaskView");
		}
	}
		
}