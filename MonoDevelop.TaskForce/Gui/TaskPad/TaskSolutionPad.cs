// 
// TaskSolutionPad.cs
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
using MonoDevelop.Ide.Gui.Pads;
using MonoDevelop.TaskForce.Utilities;
using MonoDevelop.TaskForce.Data;





namespace MonoDevelop.TaskForce.Gui.TaskPad
{

	/// <summary>
	/// Container class for the task solution pad
	/// </summary>
	public class TaskSolutionPad : TreeViewPad
	{
		LogUtil log;
		public TaskSolutionPad ()
		{
			log = new LogUtil ("TaskSolutionPad");
			TaskForceMain.Instance.TaskPad = this;
		}

		public override void Initialize (MonoDevelop.Ide.Gui.Components.NodeBuilder[] builders, MonoDevelop.Ide.Gui.Components.TreePadOption[] options, string contextMenuPath)
		{
			// Create a temporary data provider
			base.Initialize (builders, options, contextMenuPath);
			ProviderData providerData = new ProviderData ();
			


			treeView.AddChild (providerData);
			providerData.provider.InitializeProvider (providerData);
		}
	}
}
