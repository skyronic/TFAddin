// 
// CategoryData.cs
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
using MonoDevelop.TaskForce.Utilities;


namespace MonoDevelop.TaskForce.Data
{


	public class CategoryData : NodeData
	{
		public override NodeType nodeType {
			get { return NodeType.Category; }
		}
		public object CoreDataObject {
			get;
			set;
		}

		public CategoryData () : base()
		{

		}

		public override bool CanMakeChild (NodeData childData)
		{
			return this.CanMakeChild (childData.nodeType);
		}

		public override bool CanMakeChild (NodeType childType)
		{
			if (childType == NodeType.Task) {
				return true;
			}
			if (childType == NodeType.Category) {
				return true;
			}

			return false;
		}

		public override void PostSerializeHook ()
		{
			throw new System.NotImplementedException ();
		}


	}
}
