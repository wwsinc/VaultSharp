using System.Text.Json.Serialization;

namespace VaultSharp.V1.SecretsEngines.ActiveDirectory.Models
{
    public class CreateConnectionConfigModel : ConnectionConfigModel
    {
        [JsonPropertyName("bind password")]
        public string BindingPassword { get; set; }

        public CreateConnectionConfigModel()
        {
            ConnectionRequestTimeout = 90;
        }
    }
}