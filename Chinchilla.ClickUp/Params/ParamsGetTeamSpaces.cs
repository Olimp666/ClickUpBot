using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of get team space request
	/// </summary>
	public class ParamsGetTeamSpaces
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
		/// The constructor of ParamsGetTeamSpace
		/// </summary>
		/// <param name="teamId"></param>
		public ParamsGetTeamSpaces(string teamId)
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