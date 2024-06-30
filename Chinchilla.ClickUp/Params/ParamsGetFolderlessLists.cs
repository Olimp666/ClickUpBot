using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of Get Space lists request
	/// </summary>
	public class ParamsGetFolderlessLists
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
		/// The constructor of ParamsGetFolderlessLists
		/// </summary>
		/// <param name="spaceId"></param>
		public ParamsGetFolderlessLists(string spaceId)
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