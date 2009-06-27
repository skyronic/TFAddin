// 
// CommentWidget.cs
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
	public partial class CommentWidget : Gtk.Bin
	{
		
		private List<CommentData> Comments
		{
			get;
			set;
		}
		
		protected ListStore treeStore;
		protected LogUtil log;
		
		
		/// <summary>
		/// sets up the treeview model and store
		/// </summary>
		protected void PopulateTreeView()
		{
			treeStore = new ListStore(typeof(CommentData));
			// the header column, a single column for now
			TreeViewColumn headerColumn = new TreeViewColumn();
			headerColumn.Title = "Comment";
			
			// the header cell to show the comment header
			// not to be confused with the Tree's header.
			CellRendererText headerCell = new CellRendererText();
			headerColumn.PackStart(headerCell,true);
			
			// set the renderer
			headerColumn.SetCellDataFunc(headerCell, new TreeCellDataFunc(RenderCommentHeader));
			
			
			
			// Add the comments into the treestore
			foreach(CommentData comment in Comments)
			{
				log.DEBUG("Added a new comment: " + comment.ToString());
				treeStore.AppendValues(comment);
			}
			// Add the column and model to the tree
			commentTree.AppendColumn(headerColumn);
			commentTree.Model = treeStore;
			
			// handle a clicki
			commentTree.SelectionNotifyEvent += HandleSelectionNotifyEvent;
			commentTree.SelectionReceived += HandleSelectionReceived;
			commentTree.RowActivated += HandleRowActivated;
		}

		void HandleRowActivated(object o, RowActivatedArgs args)
		{
			// to retrieve the data
			TreeIter iter = new TreeIter();
			
			// get the iterator
			treeStore.GetIter(out iter, args.Path);
			
			CommentData comment = treeStore.GetValue(iter, 0) as CommentData;
			
			// set the contentview's text
			contentView.Buffer.Text = comment.Content;
			
			// enable the reply and the quote buttons
			replyButton.Sensitive = true;
			quoteButton.Sensitive = true;
		}

		void HandleSelectionNotifyEvent(object o, SelectionNotifyEventArgs args)
		{
			log.WARN("SelectionNotifyEvent");
		}

		void HandleSelectionReceived(object o, SelectionReceivedArgs args)
		{
			// get the comment
			log.WARN("Selection recieved");
			
		}
		
		private void RenderCommentHeader(TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
		{
			CommentData comment = model.GetValue(iter, 0) as CommentData;
			(cell as CellRendererText).Markup = String.Format("<b>{0}</b>  {1} \n <i>{2}</i>",comment.Author, comment.PostDate.ToString(), comment.Title);
	
		}
		
		public CommentWidget(List<CommentData> _comments)
		{
			log = new LogUtil("CommentWidget");
			this.Build();
			
			log.DEBUG("Recieved comments with size" + _comments.Count);
			Comments = _comments;
			replyButton.Sensitive = false;
			quoteButton.Sensitive = false;
			PopulateTreeView();
		}
	}
}