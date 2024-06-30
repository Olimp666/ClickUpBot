using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{

	/// <summary>
	/// Model object of Custom Field information response
	/// </summary>
	public class ResponseModelCustomFields
		: Helpers.IResponse
	{
		/// <summary>
		/// Check if the Custom Fields are enabled
		/// </summary>
		[JsonProperty("enabled")]
		public bool? Enabled { get; set; }
	}
}