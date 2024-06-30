using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of Get Team By ID requet
	/// </summary>
	public class ParamsGetTeamById
	{
		#region Attributes

		/// <summary>
		/// The Team Id to search
		/// </summary>
		[JsonProperty("team_id")]
		[DataMember(Name = "team_id")]
		public string TeamId { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// The Constructor of ParamsGetTeamByID
		/// </summary>
		/// <param name="teamId"></param>
		public ParamsGetTeamById(string teamId)
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