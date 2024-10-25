﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VaultSharp.V1.Commons;

namespace VaultSharp.V1.SecretsEngines.KeyValue.V2
{
    /// <summary>
    /// V2 of Key Value Secrets Engine
    /// </summary>
    public interface IKeyValueSecretsEngineV2
    {
        /// <summary>
        /// This path configures backend level settings that are applied to every key in the key-value store.
        /// </summary>
        /// <param name="configModel">The config</param>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the KeyValue backend. Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.</param>
        Task ConfigureAsync(KeyValue2ConfigModel configModel, string mountPoint = null);

        /// <summary>
        /// Reads the common config for all keys.
        /// </summary>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the KeyValue backend. Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.</param>
        /// <param name="wrapTimeToLive">
        /// <para>[required]</para>
        /// The TTL for the token and can be either an integer number of seconds or a string duration of seconds.
        /// </param>
        /// <returns>The config</returns>
        Task<Secret<KeyValue2ConfigModel>> ReadConfigAsync(string mountPoint = null, string wrapTimeToLive = null);

        /// <summary>
        /// Retrieves the secret at the specified location.
        /// </summary>
        /// <param name="path"><para>[required]</para>
        /// The location path where the secret needs to be read from.</param>
        /// <param name="version"><para>[optional]</para>
        /// Specifies the version to return. If not set the latest version is returned.
        /// </param>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the KeyValue backend. Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.</param>
        /// <param name="wrapTimeToLive">
        /// <para>[required]</para>
        /// The TTL for the token and can be either an integer number of seconds or a string duration of seconds.
        /// </param>
        /// <returns>
        /// The secret with the data.
        /// </returns>
        Task<Secret<SecretData>> ReadSecretAsync(string path, int? version = null, string mountPoint = null, string wrapTimeToLive = null);
        
        /// <summary>
        /// Retrieves the secret at the specified location.
        /// </summary>
        /// <param name="path"><para>[required]</para>
        /// The location path where the secret needs to be read from.</param>
        /// <param name="version"><para>[optional]</para>
        /// Specifies the version to return. If not set the latest version is returned.
        /// </param>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the KeyValue backend. Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.</param>
        /// <param name="wrapTimeToLive">
        /// <para>[required]</para>
        /// The TTL for the token and can be either an integer number of seconds or a string duration of seconds.
        /// </param>
        /// <returns>
        /// The secret with the data.
        /// </returns>
        Task<Secret<SecretData<T>>> ReadSecretAsync<T>(string path, int? version = null, string mountPoint = null, string wrapTimeToLive = null);

        /// <summary>
        /// Stores a secret at the specified location. If the value does not yet exist, the calling token must have an ACL policy granting the create capability. 
        /// If the value already exists, the calling token must have an ACL policy granting the update capability.
        /// </summary>
        /// <param name="path"><para>[required]</para>
        /// The path where the value is to be stored.</param>
        /// <param name="data"><para>[required]</para>
        /// The value to be written.</param>
        /// <param name="checkAndSet">
        /// <para>[optional]</para>
        /// If not set the write will be allowed. If set to 0 a write will only be allowed if the key doesn’t exist. 
        /// If the index is non-zero the write will only be allowed if the key’s current version matches the version specified in the cas parameter.
        /// </param>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the Generic backend. Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.
        /// </param>
        /// <returns>
        /// The task.
        /// </returns>
        /// <remarks>
        /// Unlike other secrets engines, the KV secrets engine does not enforce TTLs for expiration. 
        /// Instead, the lease_duration is a hint for how often consumers should check back for a new value. 
        /// This is commonly displayed as refresh_interval instead of lease_duration to clarify this in output.
        /// If provided a key of ttl, the KV secrets engine will utilize this value as the lease duration:
        /// Even with a ttl set, the secrets engine never removes data on its own.The ttl key is merely advisory.
        /// When reading a value with a ttl, both the ttl key and the refresh interval will reflect the value:
        /// </remarks>
        Task<Secret<CurrentSecretMetadata>> WriteSecretAsync<T>(string path, T data, int? checkAndSet = null, string mountPoint = null);

        /// <summary>
        /// Writes the data to the given path in the K/V v2 secrets engine. 
        /// The data can be of any type. 
        /// Unlike the <see cref="WriteSecretAsync{T}(string, T, int?, string)"/> method, the patch command combines the change with existing data 
        /// instead of replacing them. 
        /// Therefore, this command makes it easy to make a partial updates to an existing data.
        /// </summary>
        /// <param name="path"><para>[required]</para>
        /// The path where the value is to be stored.</param>
        /// <param name="patchSecretDataRequest"><para>[required]</para>
        /// The value to be replaced and appended.</param>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the Generic backend. Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.
        /// </param>
        /// <returns>
        /// The task.
        /// </returns>
        Task<Secret<CurrentSecretMetadata>> PatchSecretAsync(string path, PatchSecretDataRequest patchSecretDataRequest, string mountPoint = null);

        /// <summary>
        /// This endpoint provides the subkeys within a secret entry that 
        /// exists at the requested path.The secret entry at this path will be
        /// retrieved and stripped of all data by replacing underlying values 
        /// of leaf keys (i.e. non-map keys or map keys with no underlying 
        /// subkeys) with null.
        /// </summary>
        /// <param name="path">
        /// <para>[required]</para>
        /// Specifies the path of the secret to read. This is specified as part
        /// of the URL.
        /// </param>
        /// <param name="version">
        /// Specifies the version to return. If not set the latest version is
        /// returned.
        /// </param>
        /// <param name="depth">
        /// Specifies the deepest nesting level to provide in the output. The 
        /// default value 0 will not impose any limit. If non-zero, keys that 
        /// reside at the specified depth value will be artificially treated as
        /// leaves and will thus be null even if further underlying subkeys 
        /// exist.
        /// </param>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the Generic backend. Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.</param>
        /// <param name="wrapTimeToLive">
        /// <para>[required]</para>
        /// The TTL for the token and can be either an integer number of seconds or a string duration of seconds.
        /// </param>
        /// <returns>Subkeys Info for specified version and depth.</returns>
        Task<Secret<SecretSubkeysInfo>> ReadSecretSubkeysAsync(string path, int version = 0, int depth = 0, string mountPoint = null, string wrapTimeToLive = null);

        /// <summary>
        /// This endpoint issues a soft delete of the secret's latest version at the specified location. 
        /// This marks the version as deleted and will stop it from being returned from reads, 
        /// but the underlying data will not be removed. A delete can be undone using the Undelete method.
        /// </summary>
        /// <param name="path"><para>[required]</para>
        /// Specifies the path of the secret to delete.</param>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the Generic backend. Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.
        /// </param>
        /// <returns>
        /// The task.
        /// </returns>
        Task DeleteSecretAsync(string path, string mountPoint = null);

        /// <summary>
        /// This endpoint issues a soft delete of the secret's latest version at the specified location. 
        /// This marks the version as deleted and will stop it from being returned from reads, 
        /// but the underlying data will not be removed. A delete can be undone using the Undelete method.
        /// </summary>
        /// <param name="path"><para>[required]</para>
        /// Specifies the path of the secret to delete.</param>
        /// <param name="versions">
        /// <para>[required]</para>
        /// The versions to delete.
        /// </param>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the Generic backend. Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.
        /// </param>
        /// <returns>
        /// The task.
        /// </returns>
        Task DeleteSecretVersionsAsync(string path, IList<int> versions, string mountPoint = null);

        /// <summary>
        /// Undeletes the data for the provided version and path in the key-value store.
        /// This restores the data, allowing it to be returned on get requests.
        /// </summary>
        /// <param name="path"><para>[required]</para>
        /// Specifies the path of the secret to undelete.</param>
        /// <param name="versions">
        /// <para>[required]</para>
        /// The versions to undelete.
        /// </param>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the Generic backend. Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.
        /// </param>
        /// <returns>
        /// The task.
        /// </returns>
        Task UndeleteSecretVersionsAsync(string path, IList<int> versions, string mountPoint = null);

        /// <summary>
        /// Permanently removes the specified version data for the provided key and version numbers from the key-value store.
        /// </summary>
        /// <param name="path"><para>[required]</para>
        /// The path where the value is to be stored.</param>
        /// <param name="versions">
        /// <para>[required]</para>
        /// The versions to destroy. Their data will be permanently deleted.
        /// </param>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the Generic backend. Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.
        /// </param>
        /// <returns>
        /// The task.
        /// </returns>
        Task DestroySecretVersionsAsync(string path, IList<int> versions, string mountPoint = null);

        /// <summary>
        /// Retrieves the secret location path entries at the specified location.
        /// Folders are suffixed with /. The input must be a folder; list on a file will not return a value. 
        /// The values themselves are not accessible via this API.
        /// </summary>
        /// <param name="path">
        /// The location path where the secret needs to be read from. Can be empty string or null, if you
        /// want to list all secrets on the mount point.
        /// </param>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the Generic backend. Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.</param>
        /// <param name="wrapTimeToLive">
        /// <para>[required]</para>
        /// The TTL for the token and can be either an integer number of seconds or a string duration of seconds.
        /// </param>
        /// <returns>
        /// The secret list with the data.
        /// </returns>
        Task<Secret<ListInfo>> ReadSecretPathsAsync(string path, string mountPoint = null, string wrapTimeToLive = null);

        /// <summary>
        /// Retrieves the secret metadata at the specified location.
        /// </summary>
        /// <param name="path"><para>[required]</para>
        /// The location path where the secret needs to be read from.</param>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the KeyValue backend. Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.</param>
        /// <param name="wrapTimeToLive">
        /// <para>[required]</para>
        /// The TTL for the token and can be either an integer number of seconds or a string duration of seconds.
        /// </param>
        /// <returns>
        /// The secret metadata.
        /// </returns>
        Task<Secret<FullSecretMetadata>> ReadSecretMetadataAsync(string path, string mountPoint = null, string wrapTimeToLive = null);

        /// <summary>
        /// Creates or updates the metadata of a secret at the specified location in 
        /// the K/V v2 secrets engine.
        /// It does not create a new version.
        /// </summary>
        /// <param name="path">
        /// <para>[required]</para>
        /// The path where the value is to be stored.
        /// </param>
        /// <param name="customMetadataRequest">
        /// <para>[required]</para>
        /// The value to be written.
        /// </param>
        /// <param name="mountPoint">
        /// <para>[optional]</para>
        /// The mount point for the Generic backend. 
        /// Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.
        /// </param>
        /// <returns>
        /// The task.
        /// </returns>
        Task WriteSecretMetadataAsync(string path, CustomMetadataRequest customMetadataRequest, string mountPoint = null);

        /// <summary>
        /// Patch the metadata of a secret at specified location in the K/V v2 secrets engine.
        /// The patch command combines the change with existing custom metadata instead of replacing them.
        /// Therefore, this command makes it easy to make a partial updates to an existing metadata.
        /// </summary>
        /// <param name="path">
        /// <para>[required]</para>
        /// The path where the value is to be stored.
        /// </param>
        /// <param name="customMetadataRequest">
        /// <para>[required]</para>
        /// The value to be replaced and appended.
        /// </param>
        /// <param name="mountPoint">
        /// <para>[optional]</para>
        /// The mount point for the Generic backend. 
        /// Defaults to <see cref="SecretsEngineMountPoints.KeyValueV2" />
        /// Provide a value only if you have customized the mount point.
        /// </param>
        /// <returns>
        /// The task.
        /// </returns>
        Task PatchSecretMetadataAsync(string path, CustomMetadataRequest customMetadataRequest, string mountPoint = null);

        /// <summary>
        /// This endpoint permanently deletes the key metadata and all version data for the specified key. 
        /// All version history will be removed.
        /// </summary>
        /// <param name="path"><para>[required]</para>
        /// Specifies the path of the secret to delete.
        /// </param>
        /// <param name="mountPoint"><para>[optional]</para>
        /// The mount point for the Generic backend. Defaults to "secret"
        /// Provide a value only if you have customized the mount point.
        /// </param>
        /// <returns>
        /// The task.
        /// </returns>
        Task DeleteMetadataAsync(string path, string mountPoint = null);
    }
}