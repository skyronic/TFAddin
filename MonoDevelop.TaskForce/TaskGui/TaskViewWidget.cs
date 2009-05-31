
using System;
using Gtk;
namespace MonoDevelop.TaskForce
{
	
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class TaskViewWidget : Gtk.Bin
	{
		Gtk.Label tempLabel;
		public void SetTaskIntData(int data)		
		{
			tempLabel.Text = "Hello World! (" + data.ToString() + ")";
			tempLabel.Show();
		}
		public TaskViewWidget()
		{
			tempLabel = new Label();
			this.Add(tempLabel);
			this.Show();
			this.Build();
		}
	}
}
