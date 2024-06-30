using Chinchilla.ClickUp.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Chinchilla.ClickUp.Responses.Model
{

    /// <summary>
    /// Model object of Attachment information response
    /// </summary>
    public class ResponseModelAttachment
        : Helpers.IResponse
    {
        /// <summary>
        /// Id of the Attachment
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Version of the Attachment
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
		/// Date of the Attachment
		/// </summary>
		[JsonProperty("date")]
        public long Date { get; set; }

        /// <summary>
        /// Title of the Attachment
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Extension of the Attachment
        /// </summary>
        [JsonProperty("extension")]
        public string Extension { get; set; }

        /// <summary>
        /// Small thumbnail of the Attachment
        /// </summary>
        [JsonProperty("thumbnail_small")]
        public string ThumbSmall { get; set; }

        /// <summary>
        /// Large thumbnailof the Attachment
        /// </summary>
        [JsonProperty("thumbnail_large")]
        public string ThumbLarge { get; set; }

        /// <summary>
        /// Url of the Attachment
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

    }
}