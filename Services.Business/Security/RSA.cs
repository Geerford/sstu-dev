using Service.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Services.Business.Security
{
    /// <summary>
    /// Implements <see cref="IRSA"/>.
    /// </summary>
    public class RSA : IRSA
    {
        /// <summary>
        /// Store true if states have been cleanup; otherwise, false.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// The RSA-algorithm object.
        /// </summary>
        private RSACryptoServiceProvider rsa = null;

        /// <summary>
        /// The grasshopper object.
        /// </summary>
        private Grasshopper grasshopper = null;

        /// <summary>
        /// Decrypts the cipherbytes. Implements <see cref="IRSA.Decrypt(byte[], RSAParameters)"/>.
        /// </summary>
        /// <param name="cipherbytes">The cipherbytes to be decrypted.</param>
        /// <param name="SK">The server private key.</param>
        /// <returns>The plainbytes.</returns>
        public byte[] Decrypt(byte[] cipherbytes, RSAParameters SK)
        {
            rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(SK);
            return rsa.Decrypt(cipherbytes, false);
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
                    if (rsa != null)
                    {
                        rsa = null;
                    }
                    if (grasshopper != null)
                    {
                        grasshopper = null;
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
        /// Encrypts the message. Implements <see cref="IRSA.Encrypt(byte[], RSAParameters)"/>.
        /// </summary>
        /// <param name="message">The message to be encrypted.</param>
        /// <param name="PK">The client public key.</param>
        /// <returns>The cipherbytes.</returns>
        public byte[] Encrypt(byte[] message, RSAParameters PK)
        {
            rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(PK);
            return rsa.Encrypt(message, false);
        }

        /// <summary>
        /// Generates new RSA-keys and grasshopper-key. Implements <see cref="IRSA.GenerateKey"/>.
        /// </summary>
        public void GenerateKey()
        {
            rsa = new RSACryptoServiceProvider();
            Keys.PublicKey = rsa.ToXmlString(false);
            Keys.PrivateKey = rsa.ToXmlString(true);
            grasshopper = new Grasshopper();
            grasshopper.GenerateKey();
        }

        /// <summary>
        /// Encrypts to client. Implements <see cref="IRSA.Response(string, string)"/>.
        /// </summary>
        /// <param name="data">The message to be encrypted.</param>
        /// <param name="publicKey">The client public key.</param>
        /// <returns>The cipherbytes.</returns>
        public byte[] ResponseKey(byte[] message, string publicKey)
        {
            rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            return Encrypt(message, rsa.ExportParameters(false));
        }

        /// <summary>
        /// Decrypts from client. Implements <see cref="IRSA.RequestKey(byte[])"/>.
        /// </summary>
        /// <param name="cipherbytes">The cipherbytes from client.</param>
        /// <returns>The plainbytes.</returns>
        public string RequestKey(byte[] cipherbytes)
        {
            rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(Keys.PrivateKey);
            byte[] plainbytes = Decrypt(cipherbytes, rsa.ExportParameters(true));
            return Encoding.UTF8.GetString(plainbytes);
        }
    }
}