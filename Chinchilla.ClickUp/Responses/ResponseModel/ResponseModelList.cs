using System.Collections.Generic;
using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{

	/// <summary>
	/// Model object of List information response
	/// </summary>
	public class ResponseModelList
		: Helpers.IResponse
	{

		/// <summary>
		/// Id of the List
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// Name of the List
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("content")]
		public string Content { get; set; }

		[JsonProperty("folder")]
		public ResponseModelFolder Folder { get; set; }

		[JsonProperty("space")]
		public ResponseModelSpace Space { get; set; }

		/// <summary>
		/// List of Status Model that contains the information of Statuses (Only if Override Statuses is true)
		/// </summary>
		[JsonProperty("statuses")]
		public List<ResponseModelStatus> Statuses { get; set; }
	}
}