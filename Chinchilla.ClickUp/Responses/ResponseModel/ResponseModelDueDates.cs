using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{

	/// <summary>
	/// Model object of Due Dates information response
	/// </summary>
	public class ResponseModelDueDates
		: Helpers.IResponse
	{
		/// <summary>
		/// Check if Due Dates Feature is enabled
		/// </summary>
		[JsonProperty("enabled")]
		public bool? Enabled { get; set; }

		/// <summary>
		/// Check if Start Date Feature is enabled
		/// </summary>
		[JsonProperty("start_date")]
		public bool? StartDate { get; set; }

		/// <summary>
		/// Check if Remap Due Dates Feature is enabled
		/// </summary>
		[JsonProperty("remap_due_dates")]
		public bool? RemapDueDates { get; set; }
	}
}