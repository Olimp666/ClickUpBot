using Newtonsoft.Json;
using Chinchilla.ClickUp.Responses.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Responses
{

	/// <summary>
	/// Response object of the method GetAuthorizedTeams()
	/// </summary>
	[Serializable]
	[DataContract]
	public class ResponseAuthorizedTeams
		: Helpers.IResponse
	{
		/// <summary>
		/// List of Team Model with information of authorized Teams
		/// </summary>
		[JsonProperty("teams")]
		[DataMember(Name = "teams")]
		public List<ResponseModelTeam> Teams { get; set; }
	}
}