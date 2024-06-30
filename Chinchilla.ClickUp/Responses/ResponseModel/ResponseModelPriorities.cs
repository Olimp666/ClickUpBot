using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chinchilla.ClickUp.Responses.Model
{

	/// <summary>
	/// Model object of Priorities information response
	/// </summary>
	public class ResponseModelPriorities
		: Helpers.IResponse
	{
		/// <summary>
		/// Check if enabled Priorities
		/// </summary>
		[JsonProperty("enabled")]
		public bool? Enabled { get; set; }

		/// <summary>
		/// List of Model Priority with priorities information
		/// </summary>
		[JsonProperty("priorities")]
		public List<ResponseModelPriority> Priorities { get; set; }
	}
}