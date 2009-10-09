// 
// TFStore.cs
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
using MonoDevelop.Core.Serialization;
using System.Collections.Generic;
using MonoDevelop.Core;
using MonoDevelop.Projects;
using System.IO;
using MonoDevelop.TaskForce.Utilities;
using MonoDevelop.Ide.Gui.Components;

namespace MonoDevelop.TaskForce.Data
{

	/// <summary>
	/// Singleton class which takes care of saving all the providers into disk
	/// </summary>
	public class TFStore
	{
		LogUtil log;
		
		[ItemProperty()]
		public List<ProviderData> SolutionProviders {
			get;
			set;
		}

		/// <summary>
		/// The path to the XML file. Usually set by the solution
		/// </summary>
		public FilePath TargetFile {
			get;
			set;
		}

		public ExtensibleTreeView TreeView {
			get;
			set;
		}

		/// <summary>
		/// Executed on solution closed to clear out the providers
		/// </summary>
		public void SolutionClosed ()
		{
			// If a task is currently running, deactivate it
			TaskForceMain.Instance.DeactivateCurrentTask ();
			TreeView.Clear ();
			UpdateFile ();

			SolutionProviders.Clear ();
		}



		public void CreateNewLocalProvider (Solution ActiveSolution)
		{
			ProviderData defaultProvider = new ProviderData ();

			defaultProvider.provider.ConstructBasicProvider (defaultProvider);
			//defaultProvider.Label = "Tasks for " + ActiveSolution.Name;

			// Create some data in the provider first.
			// defaultProvider.provider.SeedDataForTesting("test_seed");
			SolutionProviders.Add (defaultProvider);
			TreeView.AddChild (defaultProvider);

			// update the file
			this.UpdateFile ();
		}


		/// <summary>
		/// updates the xml file by making atomic changes
		/// </summary>
		public void UpdateFile ()
		{
			log.INFO ("Updating file!");
			
			if(!TaskForceMain.Instance.DryRun)
			{

			// TODO: Create a lock here for potential threading bugs
			if (File.Exists (TargetFile.FullPath + ".temp")) {
				// TODO: should we recover this?
				FileService.DeleteFile (TargetFile.FullPath + ".temp");
			}

			FileStream tempFileStream = File.OpenWrite (TargetFile.FullPath + ".temp");

			// serialize the object.
			// TODO: make this thread safe
			// TODO: make this write directly to the filestream rather than do it twice
			string serializedString = Utilities.Util.SerializeObjectToString (this);
			StreamWriter tempStreamWriter = new StreamWriter (tempFileStream);
			tempStreamWriter.Write (serializedString);

			// clean up
			tempStreamWriter.Close ();
			tempFileStream.Close ();
			serializedString = null;

			// delete the existing taskforce file
			if (File.Exists (TargetFile.FullPath)) {
				FileService.DeleteFile (TargetFile.FullPath);
			}

			// rename the ".temp" file to regular xml
			// FileService.RenameFile(TargetFile.FullPath + ".temp", TargetFile.FullPath);
			FileService.SystemRename (TargetFile.FullPath + ".temp", TargetFile.FullPath);
			}
		}

		/// <summary>
		/// Restore an object's state after the serialization system returned all the data
		/// </summary>
		public void PostDeserializeHook ()
		{
			log.DEBUG ("PostDeserializeHook()");
			foreach (ProviderData providerData in SolutionProviders) {
				// execute the post deserialization hook for all the providers
				providerData.PostDeserializeHook ();

				log.DEBUG ("Activating a provider");
				TreeView.AddChild (providerData);
			}
		}


		public TFStore ()
		{
			log = new LogUtil ("TFStore");
			log.SetHash (this);

			SolutionProviders = new List<ProviderData> ();
		}
	}
}
