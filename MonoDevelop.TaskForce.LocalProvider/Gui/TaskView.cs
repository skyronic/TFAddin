// 
// TaskView.cs
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
using MonoDevelop.Core.Gui;


namespace MonoDevelop.TaskForce.LocalProvider.Gui
{
	enum CurrentRole
	{
		NewTask,
		EditTask,
		ViewTask
	}

	/// <summary>
	/// A single unified TaskView which can take on the role of 
	/// </summary>
	public class TaskView : AbstractViewContent
	{
		protected LogUtil log;
		protected TaskViewWidget taskViewWidget;

		public override Widget Control {
			get { return taskViewWidget; }
		}

		protected ProviderData providerNode;
		protected TaskData targetTask;


		CurrentRole Role;

		public void NewTaskRole (ProviderData _providerNode)
		{
			providerNode = _providerNode;
			Role = CurrentRole.NewTask;

			this.IsDirty = true;
		}

		public void EditTaskRole (ProviderData _providerNode, TaskData _target)
		{
			providerNode = _providerNode;
			targetTask = _target;

			taskViewWidget.PopulateFromTaskData (_target);
			Role = CurrentRole.EditTask;

			this.ContentName = _target.Label;
			this.IsDirty = false;
		}

		public override void Save ()
		{
			log.WARN ("Save () called");

			// re-construct the data from the widget
			taskViewWidget.ConvertToTaskCore ();
			this.ContentName = taskViewWidget.TargetCore.Title;

			if (Role == CurrentRole.EditTask) {
				// the task is already hooked up to the provider
				// just force an update of the TFStore for now
				targetTask.Label = taskViewWidget.TargetCore.Title;
				targetTask.TriggerUpdate ();
				TaskForceMain.Instance.StartTFStoreUpdate ();
			} else if (Role == CurrentRole.NewTask) {
				// create a new task
				TaskData data = new TaskData ();

				data.CoreDataObject = taskViewWidget.TargetCore;
				data.Label = taskViewWidget.TargetCore.Title;

				providerNode.AddChild (data);
				TaskForceMain.Instance.StartTFStoreUpdate ();

				Role = CurrentRole.EditTask;

				// the new task now becomes the target
				targetTask = data;
			}

			// Unset the IsDirry
			IsDirty = false;
		}





		public override void Load (string fileName)
		{
			log.WARN ("Function not implemented - load() - with filename - " + fileName);
		}

		public override bool IsFile {
			get { return false; }
		}

		public override bool IsReadOnly {
			get { return false; }
		}


		public TaskView ()
		{
			taskViewWidget = new TaskViewWidget ();
			taskViewWidget.TaskViewContent = this;
			log = new LogUtil ("TaskView");

			taskViewWidget.Changed += TaskViewWidgetChanged;

			// not dirty by default
			this.IsDirty = false;
			
			this.taskViewWidget.Destroyed += TaskViewWidgetDestroyed;
			
		}

		void TaskViewWidgetDestroyed (object sender, EventArgs e)
		{
			targetTask.EditWindowOpen = false;
		}


		public void ActivateCurrentTask ()
		{
			// only if it's editable task
			if (Role == CurrentRole.EditTask) {
				// activate the target task
				TaskForceMain.Instance.ActivateTask (targetTask);
			} else {
				//MessageService.ShowMessage("Unable to activate task", "Please create the task first and save it for activation");
				// Save the current task
				Save ();
				TaskForceMain.Instance.ActivateTask (targetTask);
			}
		}

		void TaskViewWidgetChanged (object sender, TaskGuiChangedEventArgs e)
		{
			this.IsDirty = true;
		}
	}
}
