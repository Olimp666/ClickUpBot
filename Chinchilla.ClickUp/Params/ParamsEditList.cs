using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The Param object of edit list request
	/// </summary>
	public class ParamsEditList
	{
		#region Attributes

		/// <summary>
		/// The List Id 
		/// </summary>
		[JsonProperty("list_id")]
		[DataMember(Name = "list_id")]
		public string ListId { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// Constructor of ParamsEditList
		/// </summary>
		/// <param name="listId"></param>
		public ParamsEditList(string listId)
		{
			ListId = listId;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Method that validate the data insert
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(ListId))
			{
				throw new ArgumentNullException("ListId");
			}
		}

		#endregion
	}
}