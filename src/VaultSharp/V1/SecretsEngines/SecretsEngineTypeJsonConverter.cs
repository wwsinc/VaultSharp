using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VaultSharp.V1.SecretsEngines
{
    /// <summary>
    /// Converts the <see cref="SecretsEngineType" /> object to and from JSON.
    /// </summary>
    internal class SecretsEngineTypeJsonConverter : JsonConverter<SecretsEngineType>
    {
        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override SecretsEngineType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var type = reader.GetString();
            return new SecretsEngineType(type);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, SecretsEngineType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Type);
        }
    }
}