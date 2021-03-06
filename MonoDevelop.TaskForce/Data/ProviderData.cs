// 
// ProviderData.cs
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
using MonoDevelop.TaskForce.Providers;
using Mono.Addins;


namespace MonoDevelop.TaskForce.Data
{


	public class ProviderData : NodeData
	{

		public ProviderData () : base()
		{
			log = new LogUtil ("ProviderData");
			log.SetHash (this);

			// TODO: This is temporary
			ExtensionNodeList nodes = AddinManager.GetExtensionNodes ("/MonoDevelop/TaskForce/Providers");


			log.INFO("Creating a new provider");
			// take nodes[0] by default
			ProviderExtensionNode node = nodes[0] as ProviderExtensionNode;
			provider = (IProvider)Activator.CreateInstance (node.Class);
			
			

		}

		public static void RegisterProviderTypes ()
		{
			// TODO: This is temporary
			ExtensionNodeList nodes = AddinManager.GetExtensionNodes ("/MonoDevelop/TaskForce/Providers");

			// take nodes[0] by default
			foreach (ProviderExtensionNode node in nodes) {

				//ProviderExtensionNode node = nodes[0] as ProviderExtensionNode;


				IProvider tempProvider = (IProvider)Activator.CreateInstance (node.Class);
				tempProvider.RegisterTypes ();
			}
		}

		public override NodeType nodeType {
			get { return NodeType.Provider; }
		}




		public override bool CanMakeChild (NodeData childData)
		{
			return this.CanMakeChild (childData.nodeType);
		}

		public override bool CanMakeChild (NodeType childType)
		{
			// A provider can only have a task as a datatype
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
			// Do any post-serialization work here


			base.PostSerializeHook ();
		}


		/// <summary>
		/// To be exectued after deserialization, to clean up any mess that might've been created 
		/// </summary>
		public override void PostDeserializeHook ()
		{
			base.PostDeserializeHook ();
		}


		/// <summary>
		/// TODO: What's the point of this? 
		/// </summary>
		public void CreateEmptyProvider ()
		{
			// Creates the provider and seeds it with minimum possible information
			provider.ConstructBasicProvider (this);

		}


	}
}
