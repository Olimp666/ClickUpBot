using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The Param object of create list request
	/// </summary>
	public class ParamsCreateFolderList
	{

		#region Attributes 

		/// <summary>
		/// The Project Id
		/// </summary>
		[JsonProperty("folder_id")]
		[DataMember(Name = "folder_id")]
		public string FolderId { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// The constructor of ParamsCreateList
		/// </summary>
		/// <param name="folderId"></param>
		public ParamsCreateFolderList(string folderId)
		{
			FolderId = folderId;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Method that validate the data insert
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(FolderId))
			{
				throw new ArgumentNullException("FolderId");
			}
		}

		#endregion

	}
}