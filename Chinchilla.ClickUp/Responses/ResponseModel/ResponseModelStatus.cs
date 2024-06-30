using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{

	/// <summary>
	/// Model Object of Status information Response
	/// </summary>
	public class ResponseModelStatus
		: Helpers.IResponse
	{
		/// <summary>
		/// Name of the Status
		/// </summary>
		[JsonProperty("status")]
		public string Status { get; set; }

		/// <summary>
		/// Type of the Status
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; set; }

		/// <summary>
		/// Order index of the Status (Zero based)
		/// </summary>
		[JsonProperty("orderindex")]
		public int OrderIndex { get; set; }

		/// <summary>
		/// Color assign at the Status
		/// </summary>
		[JsonProperty("color")]
		public string Color { get; set; }
	}
}