namespace Services.Business.Security
{
    public static class Keys
    {
        /// <summary>
        /// The server public key.
        /// </summary>
        public static string PublicKey { get; internal set; }

        /// <summary>
        /// The server private key.
        /// </summary>
        internal static string PrivateKey { get; set; }

        /// <summary>
        /// The grasshopper key.
        /// </summary>
        internal static byte[] GrasshopperKey { get; set; }
    }
}
