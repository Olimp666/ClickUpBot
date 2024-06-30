using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of CReate Task in List Request
	/// </summary>
	public class ParamsCreateTaskAttachment
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
        /// The Constructor of 'ParamsCreateTaskAttachment'
        /// </summary>
        /// <param name="taskId"></param>
        public ParamsCreateTaskAttachment(string listId)
		{
            TaskId = listId;
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