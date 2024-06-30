using Chinchilla.ClickUp.Responses.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinchilla.ClickUp.Responses
{
    /// <summary>
    /// Response object of the method GetListMembers()
    /// </summary>
    public class ResponseMembers
        :Helpers.IResponse
    {
        /// <summary>
        /// List of Model Task with information of Members received
        /// </summary>
        [JsonProperty("members")]
        public List<ResponseModelUser> Members { get; set; }
    }
}
