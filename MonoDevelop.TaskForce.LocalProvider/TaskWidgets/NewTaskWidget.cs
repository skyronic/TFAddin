// 
// NewTaskWidget.cs
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
using Gtk;
using MonoDevelop.TaskForce.Data;
using MonoDevelop.TaskForce.LocalProvider.CoreData;
using MonoDevelop.TaskForce.Utilities;


namespace MonoDevelop.TaskForce.LocalProvider.TaskWidgets
{



	[System.ComponentModel.ToolboxItem(true)]
	public partial class NewTaskWidget : Gtk.Bin
	{
		// the provider to which the task will go to
		public ProviderData ProviderNode {
			get;
			set;
		}

		public NewTaskView ViewContent {
			get;
			set;
		}

		public bool NewTaskFlag {
			get;
			set;
		}

		protected LogUtil log;

		protected virtual void OnApplyButtonClicked (object sender, System.EventArgs e)
		{

			// Create a TaskCore object and populate with values from GUI
			TaskCore core = new TaskCore ();
			/*
			 * TODO: Why isn't this working? all the objects are defined in stetic
			core.Title = this.taskNameEntry.Text;
			core.Description = this.taskDescText.Buffer.Text;
			core.CreateDate = DateTime.Now;
			
			core.DueDate = this.dueDateCal.Date;
			core.Priority = this.prioritySpin.Value;*/
			core.Title = "Stub A";
			core.Description = "Stub B";
			core.CreateDate = DateTime.Now;
			core.DueDate = DateTime.Now;
			core.Priority = 10;

			log.INFO ("Added new TaskCore - " + core.ToString ());
			// create a taskdata object
			TaskData task = new TaskData ();

			// attach the new core object to the task
			task.CoreDataObject = core;

			// write the task data to the database
			DBHelper.AddTask (core);

			// set the title of the task node
			task.Label = core.Title;

			// Add the task to the provider's children and thus trigger
			// an update of the treeview
			ProviderNode.AddChild (task);

		}

		public NewTaskWidget ()
		{
			log = new LogUtil ();
			this.Build ();
		}
	}
}
