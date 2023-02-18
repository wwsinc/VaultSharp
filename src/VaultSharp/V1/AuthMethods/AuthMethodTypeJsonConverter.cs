using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VaultSharp.V1.AuthMethods
{
    /// <summary>
    /// Converts the <see cref="AuthMethodType" /> object to and from JSON.
    /// </summary>
    internal class AuthMethodTypeJsonConverter : JsonConverter<AuthMethodType>
    {
        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override AuthMethodType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var type = reader.GetString();
                return new AuthMethodType(type);
            }

            throw new JsonException($"Expected {nameof(JsonTokenType.String)} but got {reader.TokenType}.");
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, AuthMethodType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Type);
        }
    }
}