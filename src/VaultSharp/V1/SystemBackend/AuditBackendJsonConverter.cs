using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VaultSharp.V1.SystemBackend
{
    /// <summary>
    /// Converts the <see cref="AbstractAuditBackend" /> object from JSON.
    /// </summary>
    internal class AuditBackendJsonConverter : JsonConverter<AbstractAuditBackend>
    {
        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override AbstractAuditBackend Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonElement = JsonDocument.ParseValue(ref reader).RootElement;
            object target = null;

            if (jsonElement.TryGetProperty("type", out var typeProperty))
            {
                var typeValue = typeProperty.GetString();
                var type = new AuditBackendType(typeValue);

                if (type == AuditBackendType.File)
                {
                    target = new FileAuditBackend();
                }
                else
                {
                    if (type == AuditBackendType.Syslog)
                    {
                        target = new SyslogAuditBackend();
                    }
                }

                if (target == null)
                {
                    target = new CustomAuditBackend(new AuditBackendType(typeValue));
                }

                JsonSerializer.Deserialize(jsonElement.GetRawText(), target.GetType(), options);
            }

            return target as AbstractAuditBackend;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, AbstractAuditBackend value, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(AbstractAuditBackend).IsAssignableFrom(typeToConvert);
        }
    }
}
