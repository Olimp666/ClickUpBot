using System.Net;

namespace Chinchilla.ClickUp.Helpers
{

	/// <summary>
	/// The Response Generic is the response of all request to an API. It contains the status of request, the response of the request and the possible response of the error.
	/// </summary>
	/// <typeparam name="TResponseSuccess"></typeparam>
	/// <typeparam name="TResponseError"></typeparam>
	public class ResponseGeneric<TResponseSuccess, TResponseError>
		where TResponseSuccess : IResponse
		where TResponseError : IResponse
	{

		/// <summary>
		/// Contains the status of the request
		/// </summary>
		public HttpStatusCode RequestStatus { get; set; }

		/// <summary>
		/// Contains Success Response of the Method. If there is error is null.
		/// </summary>
		public TResponseSuccess ResponseSuccess { get; set; }

		/// <summary>
		/// Contain Error Response of the Method. If there isn't error is null.
		/// </summary>
		public TResponseError ResponseError { get; set; }

		/// <summary>
		/// Constructor of Response Generic. Set to Default all values
		/// </summary>
		public ResponseGeneric()
		{
			RequestStatus = HttpStatusCode.NotImplemented;
			ResponseSuccess = default(TResponseSuccess);
			ResponseError = default(TResponseError);
		}

	}
}