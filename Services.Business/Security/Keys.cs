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

        /*
         public byte[] Key { get; } = {
            0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff,
            0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
            0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10,
            0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef
        };
         * */
    }
}
