using Newtonsoft.Json;
using Chinchilla.ClickUp.Responses.Model;
using System.Collections.Generic;

namespace Chinchilla.ClickUp.Responses
{

	/// <summary>
	/// Response object of the method GetWebhooks()
	/// </summary>
	public class ResponseWebhooks
		: Helpers.IResponse
	{
		[JsonProperty("webhooks")]
		public List<ResponseModelWebhook> Webhooks { get; set; }
	}
}