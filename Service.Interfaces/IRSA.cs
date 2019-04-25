using System;
using System.Security.Cryptography;

namespace Service.Interfaces
{
    /// <summary>
    /// Represents a facade pattern and business logic.
    /// </summary>
    public interface IRSA : IDisposable
    {
        /// <summary>
        /// Decrypts the cipherbytes. 
        /// </summary>
        /// <param name="cipherbytes">The cipherbytes to be decrypted.</param>
        /// <param name="SK">The server private key.</param>
        /// <returns>The plainbytes.</returns>
        byte[] Decrypt(byte[] cipherbytes, RSAParameters SK);

        /// <summary>
        /// Encrypts the message.
        /// </summary>
        /// <param name="message">The message to be encrypted.</param>
        /// <param name="PK">The client public key.</param>
        /// <returns>The cipherbytes.</returns>
        byte[] Encrypt(byte[] message, RSAParameters PK);

        /// <summary>
        /// Generates new RSA-keys.
        /// </summary>
        void GenerateKey();

        /// <summary>
        /// Encrypts to client.
        /// </summary>
        /// <param name="data">The message to be encrypted.</param>
        /// <param name="publicKey">The client public key.</param>
        /// <returns>The cipherbytes.</returns>
        byte[] ResponseKey(byte[] message, string publicKey);

        /// <summary>
        /// Decrypts from client.
        /// </summary>
        /// <param name="cipherbytes">The cipherbytes from client.</param>
        /// <returns>The plainbytes.</returns>
        string RequestKey(byte[] cipherbytes);
    }
}