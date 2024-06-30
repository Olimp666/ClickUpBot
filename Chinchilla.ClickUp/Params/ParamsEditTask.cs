using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of Edit Task Request
	/// </summary>
	public class ParamsEditTask
	{
		#region Attributes

		/// <summary>
		/// The Task Id
		/// </summary>
		[JsonProperty("task_id")]
		[DataMember(Name = "task_id")]
		public string TaskId { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// Constructor of ParamsEditTask
		/// </summary>
		/// <param name="taskId"></param>
		public ParamsEditTask(string taskId)
		{
			TaskId = taskId;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Method that validate data insert
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(TaskId))
			{
				throw new ArgumentNullException("TaskId");
			}
		}

		#endregion
	}
}