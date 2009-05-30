
using System;

namespace MonoDevelop.TaskForce.TaskData
{
	enum TaskType
	{
		IntTask,// Temporary
		StringTask, // Temporary
	};
	
	interface ITaskData
	{
		TaskType GetTaskType();
		
		// TODO: add more
		
	}
}
