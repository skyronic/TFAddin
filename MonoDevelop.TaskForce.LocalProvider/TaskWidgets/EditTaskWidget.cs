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
			this.Build();
			dataSeed = null;
		}
		
		public TaskData dataSeed
		{get;set;}
		
		public void PopulateValuesWithSeed()
		{
			// Check if the viewcontent has set the required data
			if(dataSeed == null)
			{
				throw new ArgumentNullException("The Data seed is null");
			}
			else
			{
				// get the core data
				TaskCore core = dataSeed.CoreDataObject as TaskCore;
				
				// Populate the GUI's data with this information
				this.taskNameEntry.Text = core.Title;
				this.taskDescText.Buffer.Text = core.Description;
			
				this.dueDateCal.Date = core.DueDate;
				this.prioritySpin.Value = core.Priority;
			}
		}
	}
}
