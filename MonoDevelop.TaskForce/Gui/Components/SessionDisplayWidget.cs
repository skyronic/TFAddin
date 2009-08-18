// 
// SessionDisplayWidget.cs
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
using MonoDevelop.TaskForce.Context;
namespace MonoDevelop.TaskForce.Gui.Components
{


	[System.ComponentModel.ToolboxItem(true)]
	public partial class SessionDisplayWidget : Gtk.Bin
	{
		ListStore sessionStore;

		public SessionDisplayWidget ()
		{
			this.Build ();
			
			TreeViewColumn startColumn = new TreeViewColumn();
			CellRendererText startRenderer = new CellRendererText();
			startColumn.Title = "Start";
			startColumn.PackStart(startRenderer, true);
			
			TreeViewColumn stopColumn = new TreeViewColumn();
			CellRendererText stopRenderer = new CellRendererText();
			stopColumn.Title = "Stop";
			stopColumn.PackStart(stopRenderer, true);
			
			TreeViewColumn durationColumn = new TreeViewColumn();
			CellRenderer durationRenderer = new CellRendererText();
			durationColumn.Title = "Duration";
			durationColumn.PackStart(durationRenderer, true);
			
			this.sessionView1.AppendColumn(startColumn);
			this.sessionView1.AppendColumn(stopColumn);
			this.sessionView1.AppendColumn(durationColumn);
			
			startColumn.AddAttribute(startRenderer, "text", 0);
			stopColumn.AddAttribute(stopRenderer, "text", 1);
			durationColumn.AddAttribute(durationRenderer, "text", 2);
			
			sessionStore = new Gtk.ListStore(typeof(string), typeof(string), typeof(string));
			sessionView1.Model = sessionStore;			
		}
		
		/// <summary>
		/// Takes a taskdata and populates the treeview's store with session's
		/// start time, end time, and duration
		/// </summary>
		/// <param name="data">
		/// A <see cref="TaskData"/>
		/// </param>
		public void SetTaskData(TaskData data)
		{
			try
			{
				foreach(TaskSession session in data.TaskContext.Sessions)
				{
					sessionStore.AppendValues(session.StartTime.ToString(), session.EndTime.ToString(), session.GetLength().ToString());				
				}
			}
			catch
			{
				
			}
		}
	}
}
