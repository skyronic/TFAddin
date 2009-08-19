// 
// TaskData.cs
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
using MonoDevelop.TaskForce.Utilities;
using MonoDevelop.TaskForce.Context;

using MonoDevelop.Core;
using MonoDevelop.Core.Serialization;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
namespace MonoDevelop.TaskForce.Data
{


	public class TaskData : NodeData
	{

		public override NodeType nodeType {
			get { return NodeType.Task; }
		}

		// The core data object which contains the provider specific data


		[ItemProperty()]
		public ContextData TaskContext {
			get;
			private set;
		}

		public TaskData () : base()
		{
			TaskContext = new ContextData ();
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

			return false;
		}

		/// <summary>
		/// to be fired by TaskForceMain only
		/// </summary>
		public void OnTaskActivated ()
		{
			TaskContext.TaskActivated ();
		}

		/// <summary>
		/// To be fired by TaskForceMain only 
		/// </summary>
		public void OnTaskDeactivated ()
		{
			TaskContext.TaskDeactivated ();

		}

		public override void PostSerializeHook ()
		{
			// do any post-serialization work here

			base.PostSerializeHook ();
		}

		public override void PostDeserializeHook ()
		{
			base.PostDeserializeHook ();
			// Check if the label accidentally has a "*[A]* " at the beginning
			this.Label.TrimStart ("*[A]*  ".ToCharArray ());
		}



	}
}
