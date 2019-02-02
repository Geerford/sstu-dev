using Newtonsoft.Json.Linq;
using System;

namespace Service.Interfaces
{
    /// <summary>
    /// Represents a facade pattern and business logic.
    /// </summary>
    public interface IAESService : IDisposable
    {
        /// <summary>
        /// Decrypts the cipherbytes.
        /// </summary>
        /// <param name="cipherbytes">The cipherbytes to be decrypted.</param>
        /// <returns>The JSON object.</returns>
        JObject Decrypt(byte[] cipherbytes);

        /// <summary>
        /// Encrypts the message.
        /// </summary>
        /// <param name="json">The JSON object to be encrypted.</param>
        /// <returns>The cipherbytes.</returns>
        byte[] Encrypt(dynamic json);
    }
}