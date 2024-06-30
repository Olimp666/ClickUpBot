using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Chinchilla.ClickUp.Helpers
{
	public class RestSharperHelper
	{
		public static ResponseGeneric<TResponseSuccess, TResponseError> ExecuteRequest<TResponseSuccess, TResponseError>(IRestClient client, IRestRequest request, params JsonConverter[] converters)
			where TResponseSuccess : IResponse
			where TResponseError : IResponse
		{
			// attach the JSON.NET serializer for RestSharp
			client.UseNewtonsoftJson(new JsonSerializerSettings {
				Converters = converters,
				DateParseHandling = DateParseHandling.None
			});

			// execute the request
			IRestResponse response = client.Execute(request);
			var result = new ResponseGeneric<TResponseSuccess, TResponseError>
			{
				RequestStatus = response.StatusCode
			};
			switch (result.RequestStatus)
			{
				case HttpStatusCode.OK:
					result.ResponseSuccess = JsonConvert.DeserializeObject<TResponseSuccess>(response.Content);
					break;
				default:
					result.ResponseError = JsonConvert.DeserializeObject<TResponseError>(response.Content);
					break;
			}

			return result;
		}

		public static async Task<ResponseGeneric<TResponseSuccess, TResponseError>> ExecuteRequestAsync<TResponseSuccess, TResponseError>(IRestClient client, IRestRequest request)
			where TResponseSuccess : IResponse
			where TResponseError : IResponse
		{
			// attach the JSON.NET serializer for RestSharp
			client.UseNewtonsoftJson();

			ResponseGeneric<TResponseSuccess, TResponseError> result = null;

			// execute the request
			var taskCompletionSource = new TaskCompletionSource<ResponseGeneric<TResponseSuccess, TResponseError>>();
			var task = client.ExecuteAsync(request, response => {
				result = new ResponseGeneric<TResponseSuccess, TResponseError>
				{
					RequestStatus = response.StatusCode
				};
				switch (result.RequestStatus)
				{
					case HttpStatusCode.OK:
						result.ResponseSuccess = JsonConvert.DeserializeObject<TResponseSuccess>(response.Content);
						break;
					default:
						result.ResponseError = JsonConvert.DeserializeObject<TResponseError>(response.Content);
						break;
				}
				taskCompletionSource.SetResult(result);
			});

			return await taskCompletionSource.Task;
		}
	}
}