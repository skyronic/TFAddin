
using System;
using MonoDevelop.Core.Gui;
using MonoDevelop.Ide.Codons;
using MonoDevelop.Ide.Gui;
using Gtk;
using MonoDevelop.TaskForce.Utilities;
namespace MonoDevelop.TaskForce
{
	
	
	public class TaskViewContent : AbstractViewContent, MonoDevelop.Ide.Gui.Content.IUrlHandler
	{
		TaskViewWidget taskViewWidget;
		LogUtil log;
				
		public TaskViewContent()
		{
			taskViewWidget = new TaskViewWidget();
			log = new LogUtil("TaskViewContent");
			this.ContentName = "Task View";
			log.DEBUG("Task view initialized");
		}
		
		public void SetTaskIntData(int data)
		{
			taskViewWidget.SetTaskIntData(data);
			
			this.ContentName += data.ToString();
			log.DEBUG("Task data set");
			
		}
		
		public override Widget Control {
			get {
				return taskViewWidget;
			}
		}
		
		public override void Load (string fileName)
		{
			log.DEBUG("Load called with fileNAme: " + fileName);			
		}
		
		public override void Dispose ()
		{
			// TODO: Dispose	
		}
		
		public override void Save ()
		{
			base.Save ();
			log.DEBUG("Save called");
		}
		
		public override void Save (string fileName)
		{
			log.DEBUG("Save called with fileName = " + fileName);
			base.Save (fileName);
		}
		
		public override string StockIconId {
			get {
				return Gtk.Stock.Yes;
			}
		}
		
		public override string ContentName {
			get {
				return base.ContentName;
			}
			set {
				base.ContentName = value;
			}
		}

		public void Open(string url)
		{
			log.DEBUG("Open called with url=" + url);
		}
		
		

	}
}
