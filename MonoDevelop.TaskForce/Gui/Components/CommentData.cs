// 
// CommentData.cs
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

namespace MonoDevelop.TaskForce.Gui.Components
{
	
	/// <summary>
	/// Contains the comment structure that will be used by the comment
	/// view widget.
	/// 
	/// TODO: Should I make this class abstract?
	/// </summary>
	public class CommentData
	{
		// the Id of the comment
		public int Id
		{	get;
			set;}
		// the task that this comment is associated with
		public int TaskId
		{	get;
			set;}
		// the Title of the comment
		public string Title
		{	get;
			set;}
		// The author of the comment.
		public string Author
		{	get;
			set;}
		// the content/message
		public string Content
		{	get;
			set;}
		// the time at which the comment was posted
		public DateTime PostDate
		{	get;
			set;}
		
		/// <summary>
		/// A simple way to generate placeholder comments for testing
		/// Making placeholders manually is frustruating.
		/// 
		/// The seed is used in author, content, etc which allows you to 
		/// differentiate between tasks without having to fill up all the propeties
		/// and repeat yourself.
		/// 
		/// DO NOT USE FOR RELEASE CODE
		/// </summary>
		/// <param name="seedString">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="seedInt">
		/// A <see cref="System.Int32"/>
		/// </param>
		public void SeedComment(string seedString, int seedInt)
		{
			this.Id = seedInt + 5;
			this.TaskId = seedInt + 100;
			
			this.Title = seedString + "_Title";
			this.Author = seedString + "_Author";
			this.Content = seedString + "_Content";
			
			this.PostDate = DateTime.Now + TimeSpan.FromDays(seedInt);
		}
		
		public CommentData()
		{
		}
		
		public CommentData(string seedString, int seedInt)
		{
			SeedComment(seedString, seedInt);
		}
		
		public override string ToString ()
		{
			return string.Format("[CommentData: Id={0}, TaskId={1}, Title={2}, Author={3}, Content={4}, PostDate={5}]",
			                     Id, TaskId, Title, Author, Content, PostDate);
		}

	}
}
