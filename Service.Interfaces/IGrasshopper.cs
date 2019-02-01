namespace Service.Interfaces
{
    /// <summary>
    /// Represents a GOST 34.12 "Grasshopper".
    /// </summary>
    public interface IGrasshopper
    {
        /// <summary>
        /// Decrypts the cipher message.
        /// </summary>
        /// <param name="message">The cipherbytes.</param>
        /// <param name="key">The key.</param>
        /// <returns>The plainbytes.</returns>
        byte[] Decrypt(byte[] message, byte[] key);

        /// <summary>
        /// Encrypts the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="key">The key.</param>
        /// <returns>The cipherbytes.</returns>
        byte[] Encrypt(byte[] message, byte[] key);

        /// <summary>
        /// Generates round keys.
        /// </summary>
        /// <param name="K">The key.</param>
        /// <returns>The expanded key from input key.</returns>
        byte[][] ExpandKey(byte[] K);

        /// <summary>
        /// Gets iteration of (sub)key-scheduling.
        /// </summary>
        /// <param name="K1">The first key.</param>
        /// <param name="K2">The second key.</param>
        /// <param name="value">The iteration value.</param>
        /// <returns>The F-iteration.</returns>
        byte[] F(byte[] K1, byte[] K2, byte[] value);

        /// <summary>
        /// Gets linear L-conversion.
        /// </summary>
        /// <param name="input">The 16-byte block.</param>
        /// <returns>The L-conversion for 16-byte input block.</returns>
        byte[] L(byte[] input);

        /// <summary>
        /// Gets the multiplication of numbers in a finite field over an 
        /// irreducible polynomial x^8 + x^7 + x^6 + x + 1.
        /// </summary>
        /// <param name="a">The first byte value.</param>
        /// <param name="b">The second byte value.</param>
        /// <returns>The multiplication of two <see cref="byte"/> values.</returns>
        byte Multiply(byte a, byte b);

        /// <summary>
        /// Gets R-conversion.
        /// </summary>
        /// <param name="input">The 16-byte block.</param>
        /// <returns>The R-conversion for 16-byte input block.</returns>
        byte[] R(byte[] input);

        /// <summary>
        /// Gets inverse linear L-conversion.
        /// </summary>
        /// <param name="input">The 16-byte block.</param>
        /// <returns>The inverse L-conversion for 16-byte input block.</returns>
        byte[] ReverseL(byte[] input);

        /// <summary>
        /// Gets inverse R-conversion.
        /// </summary>
        /// <param name="input">The 16-byte block.</param>
        /// <returns>The inverse R-conversion for 16-byte input block.</returns>
        byte[] ReverseR(byte[] input);

        /// <summary>
        /// Gets inverse nonlinear bijective conversion.
        /// </summary>
        /// <param name="input">The 16-byte block.</param>
        /// <returns>The reverse S-conversion for 16-byte input block.</returns>
        byte[] ReverseS(byte[] input);

        ///<summary>
        /// Gets nonlinear bijective <see cref="PI"/>-conversion.
        /// </summary>
        /// <param name="input">The 16-byte block.</param>
        /// <returns>The S-conversion for 16-byte input block.</returns>
        byte[] S(byte[] input);

        /// <summary>
        /// Gets XOR of two <see cref="byte"/> arrays.
        /// </summary>
        /// <param name="a">The first array.</param>
        /// <param name="b">The second array.</param>
        /// <returns>The XOR of two <see cref="byte"/> arrays.</returns>
        byte[] XOR(byte[] a, byte[] b);
    }
}