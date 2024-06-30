using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The Param object of create list request
	/// </summary>
	public class ParamsCreateFolderlessList
	{

		#region Attributes 

		/// <summary>
		/// The Project Id
		/// </summary>
		[JsonProperty("space_id")]
		[DataMember(Name = "space_id")]
		public string SpaceId { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// The constructor of ParamsCreateList
		/// </summary>
		/// <param name="spaceId"></param>
		public ParamsCreateFolderlessList(string spaceId)
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