using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chinchilla.ClickUp.Responses.Model
{

	/// <summary>
	/// Model object of Folder information response
	/// </summary>
	public class ResponseModelFolder
		: Helpers.IResponse
	{
		/// <summary>
		/// Id of the Project
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// Name of the Project
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// Check if override statuses of the space for this project
		/// </summary>
		[JsonProperty("override_statuses")]
		public bool? OverrideStatuses { get; set; }

		/// <summary>
		/// List of List Model that contains the information of the lists in this project
		/// </summary>
		[JsonProperty("lists")]
		public List<ResponseModelList> Lists { get; set; }

		/// <summary>
		/// List of Status Model that contains the information of Statuses (Only if Override Statuses is true)
		/// </summary>
		[JsonProperty("statuses")]
		public List<ResponseModelStatus> Statuses { get; set; }
	}
}