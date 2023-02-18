using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VaultSharp.V1.SecretsEngines.Database
{
    /// <summary>
    /// Converts the <see cref="DatabaseProviderType" /> object to and from JSON.
    /// </summary>
    internal class DatabaseProviderTypeJsonConverter : JsonConverter<DatabaseProviderType>
    {
        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override DatabaseProviderType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return new DatabaseProviderType(reader.GetString());
            }

            throw new JsonException();
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DatabaseProviderType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Type);
        }
    }
}