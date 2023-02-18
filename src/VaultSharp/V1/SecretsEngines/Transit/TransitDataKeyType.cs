using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace VaultSharp.V1.SecretsEngines.Transit
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransitDataKeyType
    {
        [EnumMember(Value = "plaintext")]
        plaintext,

        [EnumMember(Value = "wrapped")]
        wrapped
    }
}