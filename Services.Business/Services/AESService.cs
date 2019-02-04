using Newtonsoft.Json.Linq;
using Service.Interfaces;
using Services.Business.Security;
using System;
using System.IO;

namespace Services.Business.Services
{
    public class AESService : IAESService
    {
        /// <summary>
        /// Store true if states have been cleanup; otherwise, false.
        /// </summary>
        private bool disposed = false;
        
        /// <summary>
        /// The grasshopper object.
        /// </summary>
        private AESHelper helperAES = null;

        /// <summary>
        /// The grasshopper object.
        /// </summary>
        private Grasshopper algorithm = null;

        /// <summary>
        /// The block length.
        /// </summary>
        private readonly int blockLength = 16;

        /// <summary>
        /// Decrypts the cipherbytes. Implements <see cref="IAESService.Decrypt(byte[])"/>.
        /// </summary>
        /// <param name="cipherbytes">The cipherbytes to be decrypted.</param>
        /// <returns>The JSON object.</returns>
        public JObject Decrypt(byte[] cipherbytes)
        {
            helperAES = new AESHelper();
            algorithm = new Grasshopper();
            byte[] plainbytes = new byte[cipherbytes.Length];
            using (MemoryStream stream = new MemoryStream(cipherbytes))
            {
                byte[] block = new byte[16];
                int count = 0;
                while (stream.Read(block, 0, blockLength) > 0)
                {
                    Array.Copy(algorithm.Decrypt(block, Keys.GrasshopperKey), 0, plainbytes, count, blockLength);
                    count += blockLength;
                }
                return helperAES.ParseToJson(plainbytes);
            }
        }

        /// <summary>
        /// Overloaded Implementation of Dispose. Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (helperAES != null)
                    {
                        helperAES = null;
                    }
                    if (algorithm != null)
                    {
                        algorithm = null;
                    }
                    disposed = true;
                }
            }
        }

        /// <summary>
        /// Performs all object cleanup. Frees unmanaged resources and indicates that the 
        /// finalizer, if one is present, doesn't have to run.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Encrypts the message. Implements <see cref="IAESService.Encrypt(dynamic)"/>.
        /// </summary>
        /// <param name="json">The JSON object to be encrypted.</param>
        /// <returns>The cipherbytes.</returns>
        public byte[] Encrypt(dynamic json)
        {
            helperAES = new AESHelper();
            byte[] message = helperAES.Normalize(json);
            algorithm = new Grasshopper();
            using (MemoryStream stream = new MemoryStream(message))
            {
                byte[] block = new byte[16];
                byte[] cipherbytes = new byte[0];
                while (stream.Read(block, 0, blockLength) > 0)
                {
                    Array.Resize(ref cipherbytes, cipherbytes.Length + blockLength);
                    Array.Copy(algorithm.Encrypt(block, Keys.GrasshopperKey), 0, cipherbytes, cipherbytes.Length - blockLength, blockLength);
                }
                return cipherbytes;
            }
        }
    }
}