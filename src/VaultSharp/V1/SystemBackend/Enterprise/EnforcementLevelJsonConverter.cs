using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VaultSharp.V1.SystemBackend.Enterprise
{
    /// <summary>
    /// Converts the <see cref="EnforcementLevel" /> object to and from JSON.
    /// </summary>
    internal class EnforcementLevelJsonConverter : JsonConverter<EnforcementLevel>
    {
        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override EnforcementLevel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string type = reader.GetString();
            return new EnforcementLevel(type);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, EnforcementLevel value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}