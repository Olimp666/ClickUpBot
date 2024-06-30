using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Requests
{
	/// <summary>
	/// Request object for method CreateTeamSpace()
	/// </summary>
	[Serializable]
	[DataContract]
	public class RequestCreateTeamWebhook
	{
		#region Attributes

		/// <summary>
		/// Name of the new list
		/// </summary>
		[JsonProperty("endpoint")]
		[DataMember(Name = "endpoint")]
		public string Endpoint { get; set; }

		[JsonProperty("events")]
		[DataMember(Name = "events")]
		public string[] Events { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// The constructor of RequestCreateTeamWebhook
		/// </summary>
		/// <param name="endpoint"></param>
		public RequestCreateTeamWebhook(string endpoint)
		{
			Endpoint = endpoint;
			Events = new[] { "*" };
		}

		/// <summary>
		/// The constructor of RequestCreateTeamWebhook
		/// </summary>
		/// <param name="endpoint"></param>
		public RequestCreateTeamWebhook(string endpoint, string[] events)
		{
			Endpoint = endpoint;
			Events = events;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Validation method of data
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(Endpoint))
			{
				throw new ArgumentNullException("Name");
			}

			if (!Events.Any())
			{
				throw new ArgumentNullException("Events");
			}
		}

		#endregion
	}
}