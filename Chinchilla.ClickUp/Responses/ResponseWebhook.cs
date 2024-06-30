using Newtonsoft.Json;
using Chinchilla.ClickUp.Responses.Model;

namespace Chinchilla.ClickUp.Responses
{

	/// <summary>
	/// Response object of the method CreateWebhook()
	/// </summary>
	public class ResponseWebhook
		: Helpers.IResponse
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("webhook")]
		public ResponseModelWebhook Webhook { get; set; }
	}
}