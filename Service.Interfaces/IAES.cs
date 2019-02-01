using System.Collections.Generic;

namespace Service.Interfaces
{
    /// <summary>
    /// Represents a business logic for GOST 34.12 "Grasshopper".
    /// </summary>
    public interface IAES
    {
        /// <summary>
        /// Decrypts the cipherbytes.
        /// </summary>
        /// <param name="cipherbytes">The cipherbytes to be decrypted</param>
        /// /// <param name="key">The 256 bite key.</param>
        /// <example>byte[] key =
        ///    {
        ///       0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff,
        ///        0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
        ///        0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10,
        ///        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef
        ///    };</example>
        /// <returns>The plainbytes.</returns>
        byte[] Decrypt(byte[] cipherbytes, byte[] key);

        /// <summary>
        /// Encrypts the message.
        /// </summary>
        /// <param name="message">The message to be encrypted.</param>
        /// <param name="key">The 256 bite key.</param>
        /// <example>byte[] key =
        ///    {
        ///       0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff,
        ///        0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
        ///        0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10,
        ///        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef
        ///    };</example>
        /// <returns>The cipherbytes.</returns>
        byte[] Encrypt(byte[] message, byte[] key);

        /// <summary>
        /// Normalizes input values.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>The normalized message.</returns>
        byte[] Normalize(dynamic json);

        /// <summary>
        /// Parses the plainbytes to JSON object.
        /// </summary>
        /// <param name="plainbytes">The plainbytes.</param>
        /// <returns>The JSON object.</returns>
        Dictionary<string, object> ParseToJson(byte[] plainbytes);
    }
}