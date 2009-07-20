// 
// EditTaskWidget.cs
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
using MonoDevelop.TaskForce.Gui.Components;
namespace MonoDevelop.TaskForce.LocalProvider.TaskWidgets
{
	
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class EditTaskWidget : Gtk.Bin
	{
		public ProviderData ProviderNode
		{get;set;}
		
		public EditTaskView ViewContent
		{get;set;}
		
		public bool NewTaskFlag
		{get;set;}
		
		protected LogUtil log;
		public EditTaskWidget()
		{
			log = new LogUtil("LogUtil");
			
			this.Build();
			EditTarget = null;
		}
		
		public TaskData EditTarget
		{get;set;}
		
		
		
		public void PopulateForm()
		{
			// Check if the viewcontent has set the required data
			if(EditTarget == null)
			{
				throw new ArgumentNullException("The Data seed is null");
			}
			else
			{
				// get the core data
				TaskCore core = EditTarget.CoreDataObject as TaskCore;
				
				// Populate the GUI's data with this information
				this.taskNameEntry.Text = core.Title;
				this.taskDescText.Buffer.Text = core.Description;
			
				this.dueDateCal.Date = core.DueDate;
				this.prioritySpin.Value = core.Priority;
				
				// add the comments view and populate it
				commentWidget2.Initialize(core.Comments);
				
				// hook into the "new comment" event
				
				
			}
		}
		
		/// <summary>
		/// Triggered when the Reset button is clicked
		/// 
		/// Ignores all the changes and re-sets it to the existing edit target object
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnResetButtonClicked (object sender, System.EventArgs e)
		{
			PopulateForm();
		}
		
		/// <summary>
		/// Triggered when a new comment is added. Called by the event
		/// fired by CommentWidget or CommentWidget2
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="args">
		/// A <see cref="CommentAddedEventArgs"/>
		/// </param>
		protected virtual void OnNewCommentAdded(object sender, CommentAddedEventArgs args)
		{
			// get the new comment
			CommentData comment = args.newComment;
			
			TaskCore core = EditTarget.CoreDataObject as TaskCore;
			
			// update the database
			DBHelper.AddComment(core, comment);
			log.INFO("Added a new comment to the database");
		}

		/// <summary>
		/// Updates the TaskCore and the edit target
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnApplyButtonClicked (object sender, System.EventArgs e)
		{
			// get the core data
				TaskCore core = EditTarget.CoreDataObject as TaskCore;
				
				// Populate the GUI's data with this information
				core.Title = taskNameEntry.Text;
				core.Description = taskDescText.Buffer.Text;
			
				core.DueDate = dueDateCal.Date;
				core.Priority = (int)prioritySpin.Value;
			
			log.INFO("updating taskcore as: " + core.ToString());
			DBHelper.UpdateTask(core);
		}
	}
}
