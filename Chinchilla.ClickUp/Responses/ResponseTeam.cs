using Newtonsoft.Json;
using Chinchilla.ClickUp.Responses.Model;

namespace Chinchilla.ClickUp.Responses
{

	/// <summary>
	/// Response object of the method GetTeamByID()
	/// </summary>
	public class ResponseTeam
		: Helpers.IResponse
	{
		/// <summary>
		/// Team Model object with the information of the team
		/// </summary>
		[JsonProperty("team")]
		public ResponseModelTeam Team { get; set; }
	}
}