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
		protected LogUtil log;		
		/*protected void AddCommentToGui(CommentData comment)
		{
			// create the expander which will contain this
			Expander container = new Gtk.Expander(comment.Author);
			
			// Create a HBox which will contain the comment entry and buttons
			VBox entryContainer = new VBox();
			
			// Create a VBox for the subject and comment body
			VBox commentVBox = new VBox();
			Label subjectLabel = new Label("Subject: " + comment.Title);
		 TextView commentView = new TextView();
			commentView.Buffer.Text = comment.Content;
			
			// Make sure that the comment view content is sufficiently large
			commentView.SetSizeRequest(1,100);
			HBox hbox_1 = new HBox();
			
			Button quoteButton = new Button("Quote");
			Button replyButton = new Button("Reply");
			
			// Subject | Quote | Reply
			hbox_1.PackStart(subjectLabel, true, false, 0);
			hbox_1.PackStart(quoteButton, true, false, 0);
			hbox_1.PackStart(replyButton, true, false, 0);
			
			// pack the subject and comment into it
			commentVBox.PackStart(hbox_1, false, true, 0);
			commentVBox.PackStart(commentView, true, true, 0);
			

			
			
			// Pack both the boxes togetherin the entryContainer			
			entryContainer.PackStart(commentVBox, true, false, 0);
			
			// Add the entry container to the main expander widget
			container.Add(entryContainer);
			
			// Add the main expander to the comment vbox
			// commentVBox.PackStart(container, false, false, 0); //WTF?
			// hook into the Expand event so that we can resize the widget
			container.Activated += ContainerActivated;
		}*/
		
		protected void AddCommentToGui(CommentData comment)
		{
			// Create a new HBox to hold the subject line and the reply buttons, content, etc
			Label subjectLabel = new Label(comment.Title);
			Button quoteButton = new Button("Quote");
			Button replyButton = new Button("Reply");
			
			quoteButton.Clicked += delegate(object sender, EventArgs e) {
				// Push the comment with little ">"s to the reply window
				this.AddCommentQuote(comment);
			};
			
			replyButton.Clicked += delegate(object sender, EventArgs e) {
				this.AddCommentQuote(comment);
				// set the reply title as "Re:" 
				subjectEntry.Text = "Re: " + comment.Title;
			};
			
			HBox subjectHBox = new HBox();
			subjectHBox.PackStart(subjectLabel, true, true, 0);
			subjectHBox.PackStart(quoteButton, false, false, 0);
			subjectHBox.PackStart(replyButton, false, false, 0);
			
			// Create the entry and the VBox to hold the comment together
			TextView iterCommentView = new TextView();
			iterCommentView.Buffer.Text = comment.Content;
			iterCommentView.HeightRequest = 100;
			iterCommentView.Editable = false;
			
			VBox iterVBox = new VBox();
			iterVBox.PackStart(subjectHBox, false, false,0);
			iterVBox.PackStart(iterCommentView, true, true, 0);
			
			// Create an expander
			Expander iterContainer = new Expander(comment.Author);
			
			iterContainer.Add(iterVBox);
			iterContainer.Activated += ContainerActivated;
			
			commentVBox.PackEnd(iterContainer, true, true, 0);
		}
		
		protected void AddCommentQuote(CommentData comment)
		{
			// Don't erase the existing comment no matter what.
			// construct a "quoted" reply from the comment
			string quoteString = "\n";
			quoteString += "On " + comment.PostDate.ToString() + ", " + comment.Author + " wrote:";
			string[] commentLines = comment.Content.Split('\n');
			foreach(string commentLine in commentLines)
			{
				quoteString += "> " + commentLine + "\n";
			}			
			commentTextView.Buffer.Text += quoteString;
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
			log = new LogUtil("CommentWidget2");
			
			log.SetHash(this);
			
			addCommentButton.Clicked += AddCommentButtonClicked;
		}

		void AddCommentButtonClicked (object sender, EventArgs e)
		{
			CommentData comment = new CommentData();
			
			// Re-create all the comment information
			comment.Title = subjectEntry.Text;
			comment.Author = "Me"; // TODO TODO TODO - change this
			
			comment.Content = commentTextView.Buffer.Text;
			comment.PostDate = DateTime.Now;
		}
		
		public void Initialize(List<CommentData> _comments)
		{
			Comments = _comments;
			
			foreach(CommentData comment in Comments)
			{
				this.AddCommentToGui(comment);
			}
			
			int somevalue = 10;
			for(int i = 0; i<10; i++)
			{
				somevalue = i + 10;
				Button x = new Button("Button number " + somevalue.ToString());
				x.Clicked += delegate(object sender, EventArgs e) {
					Console.WriteLine(somevalue.ToString() + "Was clicked");
				};
			}				
		}
	}
}
