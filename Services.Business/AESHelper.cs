using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.Interfaces;

namespace Services.Business
{
    /// <summary>
    /// Implements <see cref="IAESHelper"/>.
    /// </summary>
    public class AESHelper : IAESHelper
    {
        /// <summary>
        /// The block length.
        /// </summary>
        private readonly int blockLength = 16;

        /// <summary>
        /// Implements <see cref="IAESHelper"/>.
        /// </summary>
        public byte[] Normalize(dynamic json)
        {
            byte[] message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(json));
            int remainder = message.Length % blockLength;
            if (remainder != 0)
            {
                var length = message.Length;
                Array.Resize(ref message, message.Length + (blockLength - remainder));
                for (int i = length; i < message.Length; i++)
                {
                    message[i] = 0;
                }
            }
            return message;
        }

        /// <summary>
        /// Implements <see cref="IAESHelper"/>.
        /// </summary>
        public JObject ParseToJson(byte[] plainbytes)
        {
            string jsonString = Encoding.UTF8.GetString(plainbytes);
            return JObject.Parse(jsonString);
        }
    }
}