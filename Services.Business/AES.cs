using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Service.Interfaces;

namespace Services.Business
{
    /// <summary>
    /// Implements <see cref="IAES"/>.
    /// </summary>
    public class AES : IAES
    {
        /// <summary>
        /// The block length.
        /// </summary>
        private readonly int blockLength = 16;

        /// <summary>
        /// Implements <see cref="IAES"/>.
        /// </summary>
        public byte[] Decrypt(byte[] cipherbytes, byte[] key)
        {
            Grasshopper algorithm = new Grasshopper();
            byte[] plainbytes = new byte[cipherbytes.Length];
            using (MemoryStream stream = new MemoryStream(cipherbytes))
            {
                byte[] block = new byte[16];
                int count = 0;
                while (stream.Read(block, 0, blockLength) > 0)
                {
                    Array.Copy(algorithm.Decrypt(block, key), 0, plainbytes, count, blockLength);
                    count += blockLength;
                }
                return plainbytes;
            }
        }

        /// <summary>
        /// Implements <see cref="IAES"/>.
        /// </summary>
        public byte[] Encrypt(byte[] message, byte[] key)
        {
            Grasshopper algorithm = new Grasshopper();
            using (MemoryStream stream = new MemoryStream(message))
            {
                byte[] block = new byte[16];
                byte[] cipherbytes = new byte[0];
                while (stream.Read(block, 0, blockLength) > 0)
                {
                    Array.Resize(ref cipherbytes, cipherbytes.Length + blockLength);
                    Array.Copy(algorithm.Encrypt(block, key), 0, cipherbytes, cipherbytes.Length - blockLength, blockLength);
                }
                return cipherbytes;
            }
        }

        /// <summary>
        /// Implements <see cref="IAES"/>.
        /// </summary>
        public byte[] Normalize(dynamic json)
        {
            byte[] message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(json));
            int remainder = message.Length % blockSize;
            if (remainder != 0)
            {
                var length = message.Length;
                Array.Resize(ref message, message.Length + (blockSize - remainder));
                for (int i = length; i < message.Length; i++)
                {
                    message[i] = 0;
                }
            }
            return message;
        }

        /// <summary>
        /// Implements <see cref="IAES"/>.
        /// </summary>
        public Dictionary<string, object> ParseToJson(byte[] plainbytes)
        {
            string jsonString = Encoding.UTF8.GetString(plainbytes);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
        }
    }
}