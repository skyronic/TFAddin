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

// TODO: move the commenting arch somewher else
using MonoDevelop.TaskForce.Gui.Components;
using MonoDevelop.Core.Serialization;
using MonoDevelop.TaskForce.Data;

namespace MonoDevelop.TaskForce.LocalProvider.CoreData
{
	
	
	
	public class TaskCore : ICoreData
	{
		
		[ItemProperty]
		public int Id
		{get;set;}
		
		[ItemProperty]
		public string Title
		{get;set;}
		
		[ItemProperty]
		public int Priority
		{get;set;}
		
		[ItemProperty]
		public string Description
		{get;set;}
		
		[ItemProperty]
		public DateTime CreateDate
		{get;set;}
		
		[ItemProperty]
		public DateTime DueDate
		{get;set;}
		
		[ItemProperty]
		public int Depends
		{get;set;}
		
		
		
		
		[ItemProperty]
		public List<CommentData> Comments
		{get;set;}
		
		public void AddComment(CommentData c)
		{
			Comments.Add(c);			
		}
		
		public TaskCore()
		{
			Comments = new List<CommentData>();
		}
		
		public override string ToString ()
		{
			return string.Format("[TaskCore: Id={0}, Title={1}, Description={2}, CreateDate={3}, DueDate={4}, Depends={5}, Comments={6}]", Id, Title, Description, CreateDate, DueDate, Depends, Comments);
		}
		
		/// <summary>
		/// Seed the taskcore object with some junk information.
		/// 
		/// the information is not meant to be useful but different seeds
		/// can help differentiate between different objects and makes
		/// creation of stubs easier.
		/// </summary>
		/// <param name="seedString">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="seedInt">
		/// A <see cref="System.Int32"/>
		/// </param>
		public void SeedTaskCore(string seedString, int seedInt)
		{
			Title = seedString + "_Title";
			Description = seedString + "__Description";
			CreateDate = DateTime.Now;
			DueDate = DateTime.Now + TimeSpan.FromDays(seedInt + 4);
			this.Priority = seedInt + 10;
			
			Comments = new List<CommentData>();
			// create three comments and add them
			Comments.Add(new CommentData(seedString + "_author1", seedInt + 2));
			Comments.Add(new CommentData(seedString + "_author2", seedInt + 5));
			Comments.Add(new CommentData(seedString + "_author3", seedInt + 6));
			
		}
		
		/// <summary>
		/// Just seed the taskcore from the constructor
		/// this way we won't have to create a new object making stubbing easier.
		/// 
		/// Laziness for the win. er. I mean, Laziness FTW!
		/// </summary>
		/// <param name="seedString">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="seedInt">
		/// A <see cref="System.Int32"/>
		/// </param>
		public TaskCore(string seedString, int seedInt)
		{
			SeedTaskCore(seedString, seedInt);
		}

		#region ICoreData implementation
		public string SerializeToXML ()
		{
			// Get the serialized object and return the string
			return Utilities.Util.SerializeObjectToString(this);
		}
		
		public void DeSerialize (string serializedString)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		
		

	}
}
