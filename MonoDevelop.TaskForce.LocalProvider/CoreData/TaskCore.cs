// 
// TaskCore.cs
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
using System.Collections;
using System.Collections.Generic;


namespace MonoDevelop.TaskForce.LocalProvider.CoreData
{
	
	public struct Comment
	{
		public string subject;
		public string content;
		public DateTime postDate;
		public int TaskId;
	}
	
	public class TaskCore
	{
		int id;
		public int Id
		{get;set;
		}
		
		string title;
		public string Title
		{
			get
			{
				return title;
			}
			set
			{
				title = value;
			}
		}
		
		int priority;
		public int Priority;
		
		string description;
		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
			}
		}
		
		DateTime createDate;
		public DateTime CreateDate
		{
			get
			{
				return createDate;
			}
			set
			{
				createDate = value;
			}
		}
		
		DateTime dueDate;			
		public DateTime DueDate
		{
			get
			{
				return dueDate;
			}
			set
			{
				dueDate = value;
			}
		}
		
		public int Depends
		{
			get; set;
		}
		
		public List<Comment> Comments
		{
			get; set;
		}
		
		public void AddComment(Comment c)
		{
			Comments.Add(c);			
		}
		
		public TaskCore()
		{
			Comments = new List<Comment>();
		}
		
		public override string ToString ()
		{
			return string.Format("[TaskCore: Id={0}, Title={1}, Description={2}, CreateDate={3}, DueDate={4}, Depends={5}, Comments={6}]", Id, Title, Description, CreateDate, DueDate, Depends, Comments);
		}

	}
}
