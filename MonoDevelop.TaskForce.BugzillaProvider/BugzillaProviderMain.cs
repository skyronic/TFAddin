// 
// BugzillaProviderMain.cs
//  
// Author:
//       anirudhs <>
// 
// Copyright (c) 2009 anirudhs
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
using MonoDevelop.TaskForce.Providers;
using MonoDevelop.TaskForce.Utilities;
using MonoDevelop.TaskForce.Data;
using MonoDevelop.TaskForce.BugzillaProvider.CoreData;

namespace MonoDevelop.TaskForce.BugzillaProvider
{


	public class BugzillaProviderMain : IProvider
	{
		LogUtil log;
		ProviderData providerNode;

		#region IProvider implementation
		public void AddChildCategory (MonoDevelop.TaskForce.Data.NodeData parent)
		{
			throw new System.NotImplementedException();
		}
		
		public void ConstructBasicProvider (MonoDevelop.TaskForce.Data.ProviderData _providerNode)
		{
			providerNode = _providerNode;

			log = new LogUtil ("BugzillaProviderMain");

			if (providerNode == null) {
				log.ERROR ("Provider is wrong!");
			}
		}
		
		public void CreateNewCategory ()
		{
			throw new System.NotImplementedException();
		}
		
		public void DeSerialize (string serializedString)
		{
			//throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// I don't know why this method exists. overriding anyways :\
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		//// </returns>
		public string DoSomething ()
		{
			return "Hello world";
		}
		
		public void EditTask (MonoDevelop.TaskForce.Data.TaskData target)
		{
			throw new System.NotImplementedException();
		}
		
		
		public void InitializeProvider (MonoDevelop.TaskForce.Data.ProviderData _providerNode)
		{
			log.DEBUG("Initializing provider");
			providerNode = _providerNode;	
			
			if(providerNode == null)
			{
				log.ERROR( "Provider is null" );
				throw new NullReferenceException();				
			}
			
			providerNode.Label = "Bugzilla";
			
		}
		
		public void NewTask (MonoDevelop.TaskForce.Data.ProviderData providerNode)
		{
			throw new System.NotImplementedException();
		}
		
		public void RegisterTypes ()
		{
			//throw new System.NotImplementedException();
			Util.AddTypeToContext(typeof(BugzillaProviderCore));
			Util.AddTypeToContext(typeof(BugzillaTaskCore));
			Util.AddTypeToContext(typeof(BugzillaProviderMain));
		}
		
		public void SeedDataForTesting (string seedString)
		{
			for (int i = 0 ; i < 3; i++)
			{
				TaskData taskNode = new TaskData();
				taskNode.CoreDataObject = new BugzillaTaskCore() as ICoreData;
				taskNode.Label = seedString + i.ToString();
				
				providerNode.AddChildSilent(taskNode);
			}
			
			providerNode.TriggerUpdate();
		}
		
		public string SerializeToXML ()
		{
			throw new System.NotImplementedException();
		}
		
		public void ViewTask (MonoDevelop.TaskForce.Data.TaskData target)
		{
			throw new System.NotImplementedException();
		}
		#endregion

		public BugzillaProviderMain ()
		{
			log = new LogUtil("BugzillaProviderMain");
		}
	}
}
