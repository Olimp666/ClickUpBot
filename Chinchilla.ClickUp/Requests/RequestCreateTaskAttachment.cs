using Newtonsoft.Json;
using Chinchilla.ClickUp.Enums;
using System;
using System.Collections.Generic;
using Chinchilla.ClickUp.Helpers;

namespace Chinchilla.ClickUp.Requests
{
    /// <summary>
    /// Request object for method CreateTaskAttachment()
    /// </summary>
    public class RequestCreateTaskAttachment
    {
		#region Attributes

		/// <summary>
		/// Attachment for the task
		/// </summary>
		[JsonProperty("attachment")]
		public byte[] Attachment { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// Constructor of RequestCreateTaskInList
		/// </summary>
		/// <param name="name"></param>
		public RequestCreateTaskAttachment(byte[] attachment)
		{
			Attachment = attachment;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Validation method of data
		/// </summary>
		public void ValidateData()
		{
			if (Attachment==null)
			{
				throw new ArgumentNullException("Attachment");
			}
		}

		#endregion
	}
}