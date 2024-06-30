using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of Access Token Request
	/// </summary>
	[Serializable]
	[DataContract]
	public class ParamsAccessToken
	{

		#region Attributes

		/// <summary>
		/// The Client Id
		/// </summary>
		[JsonProperty("client_id")]
		[DataMember(Name = "client_id")]
		public string ClientId { get; set; }

		/// <summary>
		/// The Client Secret
		/// </summary>
		[JsonProperty("client_secret")]
		[DataMember(Name = "client_secret")]
		public string ClientSecret { get; set; }

		/// <summary>
		/// The Code 
		/// </summary>
		[JsonProperty("code")]
		[DataMember(Name = "code")]
		public string Code { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// The constructor of ParamsAccessToken
		/// </summary>
		/// <param name="clientId"></param>
		/// <param name="clientSecret"></param>
		/// <param name="code"></param>
		public ParamsAccessToken(string clientId, string clientSecret, string code)
		{
			ClientId = clientId;
			ClientSecret = clientSecret;
			Code = code;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Method that validate data insert
		/// </summary>
		public void ValidateData()
		{
			if (!string.IsNullOrEmpty(ClientId))
			{
				throw new ArgumentNullException("ClientId");
			}

			if (!string.IsNullOrEmpty(ClientSecret))
			{
				throw new ArgumentNullException("ClientSecret");
			}

			if (!string.IsNullOrEmpty(Code))
			{
				throw new ArgumentNullException("Code");
			}
		}

		#endregion

	}

}