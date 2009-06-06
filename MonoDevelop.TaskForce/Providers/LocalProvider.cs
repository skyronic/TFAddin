// 
// LocalProvider.cs
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
using MonoDevelop.TaskForce.Gui.TaskView;
using MonoDevelop.TaskForce.Utilities;
using MonoDevelop.Ide.Gui;


namespace MonoDevelop.TaskForce.Providers
{
	
	
	public class LocalProvider : TaskProvider
	{
		
		public LocalProvider()
		{
		}
		
		public override void CreateTask ()
		{
			// Create a NewTaskView
			NewTaskView viewContent = new NewTaskView();
			
			// subscribe to the task created event
			viewContent.TaskCreationFinished += ViewContentTaskCreationFinished;
			
			// Open the viewcontent in the IDE 
			IdeApp.Workbench.OpenDocument((viewContent as IViewContent), true);
			
			
			
		}

		void ViewContentTaskCreationFinished (TaskData newTask)
		{
			// we have the new task which we should pass on to the provider
			providerNode.AddChild(newTask);
		}

		public override void EditTaskContent (ref TaskData task)
		{
			throw new System.NotImplementedException();
		}

		public override void NewTaskContent (ref TaskData task)
		{
			throw new System.NotImplementedException();
		}

		public override void ViewTaskContent (TaskData task)
		{
			throw new System.NotImplementedException();
		}

	}
}
