using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

    /// <summary>
    /// The param object of Get List Members Request
    /// </summary>
    public class ParamsGetListMembers
    {
        #region Attributes

        /// <summary>
        /// The List Id
        /// </summary>
        [JsonProperty("list_id")]
        [DataMember(Name = "list_id")]
        public string ListId { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// The Constructor of 'ParamsGetListMembers'
        /// </summary>
        /// <param name="listId"></param>
        public ParamsGetListMembers(string listId)
        {
            ListId = listId;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Method that validate data insert
        /// </summary>
        public void ValidateData()
        {
            if (string.IsNullOrEmpty(ListId))
            {
                throw new ArgumentNullException("ListId");
            }
        }

        #endregion

    }
}