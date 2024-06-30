using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of get team webhooks request
	/// </summary>
	public class ParamsGetTeamWebhooks
	{
		#region Attributes

		/// <summary>
		/// The Team Id
		/// </summary>
		[JsonProperty("team_id")]
		[DataMember(Name = "team_id")]
		public string TeamId { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// The constructor of ParamsGetTeamWebhooks
		/// </summary>
		/// <param name="teamId"></param>
		public ParamsGetTeamWebhooks(string teamId)
		{
			TeamId = teamId;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Method that validate data insert
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(TeamId))
			{
				throw new ArgumentNullException("TeamId");
			}
		}

		#endregion
	}
}