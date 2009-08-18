// 
// TaskViewWidget.cs
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
using MonoDevelop.TaskForce.LocalProvider.CoreData;
using MonoDevelop.TaskForce.Data;
using MonoDevelop.TaskForce.Utilities;
using MonoDevelop.TaskForce.Gui.Components;

namespace MonoDevelop.TaskForce.LocalProvider.Gui
{


	[System.ComponentModel.ToolboxItem(true)]
	public partial class TaskViewWidget : Gtk.Bin
	{
		LogUtil log;
		public TaskCore TargetCore
		{get;set;}
		
		public TaskView TaskViewContent
		{get;set;}
		

		public TaskViewWidget ()
		{
			log = new LogUtil("TaskViewWidget");
			this.Build ();
			TargetCore = new TaskCore();
			
			this.ShowAll();
			nameEntry.Changed += FormContentChanged;
			descriptionTextView.Buffer.Changed += FormContentChanged;
			priorityCombo.Changed += FormContentChanged;
			activateButton.Clicked += ActivateCurrentTask;
		}

		void ActivateCurrentTask (object sender, EventArgs e)
		{
			// bubble event upwards
			TaskViewContent.ActivateCurrentTask();
		}

		void FormContentChanged (object sender, EventArgs e)
		{
			// Fire the changed event.
			this.Changed(this, new TaskGuiChangedEventArgs());
		}
		
		public void PopulateFromTaskData(TaskData _data)
		{
			// get the coredata
			TargetCore = _data.CoreDataObject as TaskCore;
			
			// Initialize the comments
			commentwidget21.Initialize(TargetCore.Comments);
			
			// Populate the other fields
			nameEntry.Text = TargetCore.Title;
			descriptionTextView.Buffer.Text = TargetCore.Description;
			try
			{
				priorityCombo.Active = TargetCore.Priority;
			}
			catch
			{
				log.ERROR("Priority wasn't loaded properly into the priority combo box");
			}
			
			commentwidget21.NewCommentAdded += OnNewCommentAdded;
			
			this.sessiondisplaywidget1.SetTaskData(_data);
		}

		void OnNewCommentAdded (CommentAddedEventArgs args)
		{
			TargetCore.Comments.Add(args.newComment);
			// TODO: Do we save here or what?			
		}
		
		
		/// <summary>
		/// Call this function to rereate the taskcore from the GUI
		/// </summary>
		public void ConvertToTaskCore()
		{
			TargetCore.Title = nameEntry.Text;
			TargetCore.Description = descriptionTextView.Buffer.Text;
			TargetCore.Priority = priorityCombo.Active;
			TargetCore.DueDate = DateTime.Now; // temporary
		}

		

		protected virtual void ActivateButtonClicked (object sender, System.EventArgs e)
		{
			TaskViewContent.ActivateCurrentTask();
		}
		
		public event EventHandler<TaskGuiChangedEventArgs> Changed;
	}
	
	
	[Serializable]
	public sealed class TaskGuiChangedEventArgs : EventArgs
	{
		public TaskGuiChangedEventArgs ()
		{
			
		}
	}
}
