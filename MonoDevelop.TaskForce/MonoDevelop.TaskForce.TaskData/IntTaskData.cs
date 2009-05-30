
using System;
using MonoDevelop.TaskForce.Utilities;
using System.Collections;
namespace MonoDevelop.TaskForce.TaskData
{
	
	
	public class IntTaskData : ITaskData
	{
		public int seed, data;
		public ArrayList children;
		LogUtil log;
		public IntTaskData parent;
		
		
		public IntTaskData(int _seed)
		{
			parent = null; // for root nodes
			log = new LogUtil("IntTaskData" + _seed.ToString());
			
			log.DEBUG("new IntTaskData");			
			seed = _seed;
			data = seed;
			
			children = new ArrayList();
			for(int i = data - 1; i>0; i--)
			{
				IntTaskData child = new IntTaskData(i);
				// set the parent's ref as the current object
				child.parent = this;
				
				// push the child's ref onto the children list
				
				children.Add(child);
			}
		}
		
		#region ITaskData implementation
		TaskType ITaskData.GetTaskType()
		{
			return TaskType.IntTask;
		}
		#endregion
	}
}