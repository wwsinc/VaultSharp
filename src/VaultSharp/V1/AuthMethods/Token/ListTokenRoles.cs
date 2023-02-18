using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VaultSharp.V1.AuthMethods.Token
{
    public class ListTokenRoles
    {
        /// <summary>
        /// List of available token roles.
        /// </summary>
        [JsonPropertyName("keys")]
        public List<string> Keys { get; set; }
    }
}
