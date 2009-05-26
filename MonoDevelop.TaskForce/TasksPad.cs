using System;
using MonoDevelop.Ide.Gui;
using Gtk;
namespace MonoDevelop.TaskForce
{
	class TasksPad : AbstractPadContent
	{
		public TasksPad()
		{
			
			
		}
		
		public override Gtk.Widget Control
		{
			get
			{
				Gtk.Label temp = new Gtk.Label();
				temp.Text = "Hello World";
				temp.Show();
				return temp;
			}
		}
	}
}