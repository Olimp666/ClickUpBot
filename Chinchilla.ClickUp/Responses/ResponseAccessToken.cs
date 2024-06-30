using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses
{

	/// <summary>
	/// Response object of the Authentication
	/// </summary>
	public class ResponseAccessToken
		: Helpers.IResponse
	{
		/// <summary>
		/// AccessToken Returned
		/// </summary>
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }
	}
}