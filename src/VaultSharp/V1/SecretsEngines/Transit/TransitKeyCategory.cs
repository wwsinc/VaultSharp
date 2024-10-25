﻿
namespace VaultSharp.V1.SecretsEngines.Transit
{
    /// <summary>
    /// Represents the type of key category.
    /// </summary>
    public enum TransitKeyCategory
    {
        encryption_key = 1,

        signing_key = 2,

        hmac_key = 3,

        public_key = 4,
        
        certificate_chain = 5,
        /// <summary>
        /// Enterprise only
        /// </summary>
        cmac_key = 6
    }
}
