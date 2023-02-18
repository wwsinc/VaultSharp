using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VaultSharp.V1.SystemBackend
{
    /// <summary>
    /// Converts the <see cref="AuditBackendType" /> object to and from JSON.
    /// </summary>
    internal class AuditBackendTypeJsonConverter : JsonConverter<AuditBackendType>
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The JSON writer to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="options">The serialization options.</param>
        public override void Write(Utf8JsonWriter writer, AuditBackendType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The JSON reader to read from.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">The serialization options.</param>
        /// <returns>The object value.</returns>
        public override AuditBackendType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string type = reader.GetString();
            return new AuditBackendType(type);
        }
    }
}