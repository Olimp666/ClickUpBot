using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{

	public partial class ResponseModelWebhook
	{
		public class ResponseModelWebhookHealth
			: Helpers.IResponse
		{
			[JsonProperty("status")]
			public string Status { get; set; }

			[JsonProperty("fail_count")]
			public int FailedCount { get; set; }
		}
	}
}