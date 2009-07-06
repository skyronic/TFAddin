// 
// CommentWidget2.cs
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
using System.Collections.Generic;
using MonoDevelop.TaskForce.Utilities;


namespace MonoDevelop.TaskForce.Gui.Components
{


	[System.ComponentModel.ToolboxItem(true)]
	public partial class CommentWidget2 : Gtk.Bin
	{
		protected List<CommentData> Comments
		{get;set;}
		
		protected void AddCommentToGui(CommentData comment)
		{
			// create the expander which will contain this
			Expander container = new Gtk.Expander(comment.Author);
			
			// Create a HBox which will contain the comment entry and buttons
			HBox entryContainer = new HBox();
			
			// Create a VBox for the subject and comment body
			VBox vbox1 = new VBox();
			Label subjectLabel = new Label("Subject: " + comment.Title);
			TextView commentView = new TextView();
			commentView.Buffer.Text = comment.Content;
			
			// pack the subject and comment into it
			vbox1.PackStart(subjectLabel, true, false, 0);
			vbox1.PackStart(commentView, true, true, 0);
			
			// Create a vbox that contains the reply options
			VBox vbox2 = new VBox();
			Button quoteButton = new Button("Quote");
			Button replyButton = new Button("Reply");
			vbox2.PackStart(quoteButton, true, false, 0);
			vbox2.PackStart(replyButton, true, false, 0);
			
			// Pack both the boxes togetherin the entryContainer			
			entryContainer.PackStart(vbox1, true, true, 0);
			entryContainer.PackStart(vbox2, true, false, 0);
			
			// Add the entry container to the main expander widget
			container.Add(entryContainer);
			
			// Add the main expander to the comment vbox
			commentVBox.PackStart(container, false, false, 0);
			
			// hook into the Expand event so that we can resize the widget
			container.Activated += ContainerActivated;
		}

		void ContainerActivated (object sender, EventArgs e)
		{
			GLib.Timeout.Add(100, UpdateSize);
		}
		
		bool UpdateSize()
		{
			int w, h;
			this.GetSizeRequest(out w, out h);
			this.SetSizeRequest(w, h);
			return false;
		}		
		
		public CommentWidget2 ()
		{
			this.Build ();
		}
		
		public void Initialize(List<CommentData> _comments)
		{
			Comments = _comments;
			
			foreach(CommentData comment in Comments)
			{
				this.AddCommentToGui(comment);
			}
		}
	}
}
