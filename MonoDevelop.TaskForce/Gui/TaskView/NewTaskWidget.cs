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
using MonoDevelop.TaskForce.Data;
using MonoDevelop.TaskForce.Utilities;
using Gtk;


namespace MonoDevelop.TaskForce.Gui.TaskView
{
	
	
	public delegate void TaskProcessedDelegate(TaskData newTask);
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class NewTaskWidget : Gtk.Bin
	{
		protected LogUtil log;
		Gtk.HBox hbox1;
		Gtk.Label label1;
		Gtk.Entry entry1;
		Gtk.Button button1;
		
		
		public NewTaskWidget() : base()
		{
			log = new LogUtil("NewTaskWidget");
			
			log.SetHash(this);
			// start building the GUI manually :(
			hbox1 = new Gtk.HBox();
			label1 = new Label();
			button1 = new Button();
			entry1 = new Entry();
			
			this.Add(hbox1);
			hbox1.Add(label1);
			hbox1.Add(entry1);
			hbox1.Add(button1);
			
			
			log.DEBUG("Creating the new task widget");
			label1.Text = "Enter task name";
			button1.Label = "Finish";
			
			button1.Clicked += Button1Clicked;
			log.DEBUG("Building");
			this.ShowAll();
			
			log.DEBUG("Finished building"); // TODO: Why isn't the widget being displayed

		}

		/// <summary>
		/// This is triggered when the button is clicked and the task name should
		/// be set.
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="EventArgs"/>
		/// </param>
		void Button1Clicked (object sender, EventArgs e)
		{
			log.DEBUG("Finish button clicked");
			TaskData newTask = new TaskData();
			
			// set the label and the name parameters of the data
			newTask.data["label"] = entry1.Text;
			newTask.data["text"] = entry1.Text;
			
			OnTaskCreated(newTask);
			
		}
		
		// create an event that will be fired after the task created
		public event TaskProcessedDelegate OnTaskCreated;
		
		
	}
}
