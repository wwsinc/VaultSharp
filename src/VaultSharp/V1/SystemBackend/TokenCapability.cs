using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace VaultSharp.V1.SystemBackend
{
    /// <summary>
    /// Represents the capabilities of a token.
    /// </summary>
    public class TokenCapability : Dictionary<string, object>
    {
        /// <summary>
        /// Gets the capabilities.
        /// </summary>
        public IEnumerable<string> Capabilities
        {
            get
            {
                if (TryGetValue("capabilities", out var capabilitiesElement) &&
                    capabilitiesElement is JsonElement capabilitiesArray && capabilitiesArray.ValueKind == JsonValueKind.Array)
                {
                    var capabilitiesList = new List<string>();

                    foreach (var capabilityElement in capabilitiesArray.EnumerateArray())
                    {
                        if (capabilityElement.ValueKind == JsonValueKind.String)
                        {
                            capabilitiesList.Add(capabilityElement.GetString());
                        }
                    }

                    return capabilitiesList;
                }

                return Enumerable.Empty<string>();
            }
        }
    }
}