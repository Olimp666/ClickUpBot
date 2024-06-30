using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of Get Space Folders request
	/// </summary>
	public class ParamsGetSpaceFolders
	{
		#region Attributes

		/// <summary>
		/// The Space Id 
		/// </summary>
		[JsonProperty("space_id")]
		[DataMember(Name = "space_id")]
		public string SpaceId { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// The constructor of ParamsGetSpaceProjects
		/// </summary>
		/// <param name="spaceId"></param>
		public ParamsGetSpaceFolders(string spaceId)
		{
			SpaceId = spaceId;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Method that validate the data insert
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(SpaceId))
			{
				throw new ArgumentNullException("SpaceId");
			}
		}

		#endregion
	}
}