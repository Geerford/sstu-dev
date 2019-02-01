using Service.Interfaces;
using System;

namespace Services.Business
{
    /// <summary>
    /// Implements <see cref="IGrasshopper"/>.
    /// </summary>
    public class Grasshopper : IGrasshopper
    {
        /// <summary>
        /// The block length.
        /// </summary>
        private readonly int blockLength = 16;

        /// <summary>
        /// The PI from section 5.1.1.
        /// </summary>
        private readonly byte[] PI = {
            252, 238, 221, 17, 207, 110, 49, 22, 251, 196, 250, 218, 35, 197, 4, 77,
            233, 119, 240, 219, 147, 46, 153, 186, 23, 54, 241, 187, 20, 205, 95, 193,
            249, 24, 101, 90, 226, 92, 239, 33, 129, 28, 60, 66, 139, 1, 142, 79,
            5, 132, 2, 174, 227, 106, 143, 160, 6, 11, 237, 152, 127, 212, 211, 31,
            235, 52, 44, 81, 234, 200, 72, 171, 242, 42, 104, 162, 253, 58, 206, 204,
            181, 112, 14, 86, 8, 12, 118, 18, 191, 114, 19, 71, 156, 183, 93, 135,
            21, 161, 150, 41, 16, 123, 154, 199, 243, 145, 120, 111, 157, 158, 178, 177,
            50, 117, 25, 61, 255, 53, 138, 126, 109, 84, 198, 128, 195, 189, 13, 87,
            223, 245, 36, 169, 62, 168, 67, 201, 215, 121, 214, 246, 124, 34, 185, 3,
            224, 15, 236, 222, 122, 148, 176, 188, 220, 232, 40, 80, 78, 51, 10, 74,
            167, 151, 96, 115, 30, 0, 98, 68, 26, 184, 56, 130, 100, 159, 38, 65,
            173, 69, 70, 146, 39, 94, 85, 47, 140, 163, 165, 125, 105, 213, 149, 59,
            7, 88, 179, 64, 134, 172, 29, 247, 48, 55, 107, 228, 136, 217, 231, 137,
            225, 27, 131, 73, 76, 63, 248, 254, 141, 83, 170, 144, 202, 216, 133, 97,
            32, 113, 103, 164, 45, 43, 9, 91, 203, 155, 37, 208, 190, 229, 108, 82,
            89, 166, 116, 210, 230, 244, 180, 192, 209, 102, 175, 194, 57, 75, 99, 182
        };

        /// <summary>
        /// The inverse PI.
        /// </summary>
        private readonly byte[] inversePI = {
            165, 45, 50, 143, 14, 48, 56, 192, 84, 230, 158, 57, 85, 126, 82, 145,
            100, 3, 87, 90, 28, 96, 7, 24, 33, 114, 168, 209, 41, 198, 164, 63,
            224, 39, 141, 12, 130, 234, 174, 180, 154, 99, 73, 229, 66, 228, 21, 183,
            200, 6, 112, 157, 65, 117, 25, 201, 170, 252, 77, 191, 42, 115, 132, 213,
            195, 175, 43, 134, 167, 177, 178, 91, 70, 211, 159, 253, 212, 15, 156, 47,
            155, 67, 239, 217, 121, 182, 83, 127, 193, 240, 35, 231, 37, 94, 181, 30,
            162, 223, 166, 254, 172, 34, 249, 226, 74, 188, 53, 202, 238, 120, 5, 107,
            81, 225, 89, 163, 242, 113, 86, 17, 106, 137, 148, 101, 140, 187, 119, 60,
            123, 40, 171, 210, 49, 222, 196, 95, 204, 207, 118, 44, 184, 216, 46, 54,
            219, 105, 179, 20, 149, 190, 98, 161, 59, 22, 102, 233, 92, 108, 109, 173,
            55, 97, 75, 185, 227, 186, 241, 160, 133, 131, 218, 71, 197, 176, 51, 250,
            150, 111, 110, 194, 246, 80, 255, 93, 169, 142, 23, 27, 151, 125, 236, 88,
            247, 31, 251, 124, 9, 13, 122, 103, 69, 135, 220, 232, 79, 29, 78, 4,
            235, 248, 243, 62, 61, 189, 138, 136, 221, 205, 11, 19, 152, 2, 147, 128,
            144, 208, 36, 52, 203, 237, 244, 206, 153, 16, 68, 64, 146, 58, 1, 38,
            18, 26, 72, 104, 245, 129, 139, 199, 214, 32, 10, 8, 0, 76, 215, 116
        };

        /// <summary>
        /// The linear array from sect 5.1.2
        /// </summary>
        private readonly byte[] linear = {
            0x94, 0x20, 0x85, 0x10, 0xC2, 0xC0, 0x01, 0xFB,
            0x01, 0xC0, 0xC2, 0x10, 0x85, 0x20, 0x94, 0x01
        };

        /// <summary>
        /// Implements <see cref="IGrasshopper"/>.
        /// </summary>
        public byte[] Decrypt(byte[] message, byte[] key)
        {
            byte[][] K = ExpandKey(key);
            for (int i = 9; i > 0; i--)
            {
                message = XOR(message, K[i]);
                message = ReverseL(message);
                message = ReverseS(message);
            }
            message = XOR(message, K[0]);
            return message;
        }

        /// <summary>
        /// Implements <see cref="IGrasshopper"/>.
        /// </summary>
        public byte[] Encrypt(byte[] message, byte[] key)
        {
            byte[][] K = ExpandKey(key);
            for (int i = 0; i < 9; i++)
            {
                message = XOR(message, K[i]);
                message = S(message);
                message = L(message);
            }
            message = XOR(message, K[9]);
            return message;
        }

        /// <summary>
        /// Implements <see cref="IGrasshopper"/>.
        /// </summary>
        public byte[][] ExpandKey(byte[] K)
        {
            byte[][] result = new byte[10][];
            for (int i = 0; i < 10; i++)
            {
                result[i] = new byte[16];
            }
            Array.Copy(K, 0, result[0], 0, 16);
            Array.Copy(K, 16, result[1], 0, 16);
            for (int i = 0; i < 8; i += 2)
            {
                byte[] keys = new byte[32];
                Array.Copy(result[i], 0, keys, 0, 16);
                Array.Copy(result[i + 1], 0, keys, 16, 16);
                for (int j = 0; j < 8; j++)
                {
                    byte[] K1 = new byte[16];
                    byte[] K2 = new byte[16];
                    Array.Copy(keys, 0, K1, 0, 16);
                    Array.Copy(keys, 16, K2, 0, 16);
                    byte[] iter = new byte[16];
                    for (int z = 0; z < 15; z++)
                    {
                        iter[0] = 0x00;
                    }
                    iter[15] = (byte)((i / 2) * 8 + j + 1);
                    keys = F(K1, K2, L(iter));
                }
                Array.Copy(keys, 0, result[i + 2], 0, 16);
                Array.Copy(keys, 16, result[i + 3], 0, 16);
            }
            return result;
        }

        /// <summary>
        /// Implements <see cref="IGrasshopper"/>.
        /// </summary>
        public byte[] F(byte[] K1, byte[] K2, byte[] value)
        {
            byte[] result = new byte[32];
            Array.Copy(K1, 0, result, 16, 16);
            byte[] inner = new byte[16];
            inner = XOR(K1, value);
            inner = S(inner);
            inner = L(inner);
            inner = XOR(inner, K2);
            Array.Copy(inner, 0, result, 0, 16);
            return result;
        }

        /// <summary>
        /// Implements <see cref="IGrasshopper"/>.
        /// </summary>
        public byte[] L(byte[] input)
        {
            for (int i = 0; i < 16; i++)
            {
                input = R(input);
            }
            return input;
        }

        /// <summary>
        /// Gets the multiplication of numbers in a finite field over an 
        /// irreducible polynomial x^8 + x^7 + x^6 + x + 1.
        /// </summary>
        /// <param name="a">The first byte value.</param>
        /// <param name="b">The second byte value.</param>
        /// <returns>The multiplication of two <see cref="byte"/> values.</returns>
        public byte Multiply(byte a, byte b)
        {
            byte c = 0x00;
            while (b != 0x00)
            {
                if ((b & 1) != 0x00)
                {
                    c ^= a;
                }
                a = (byte)((a << 1) ^ ((a & 0x80) == 0x80 ? 0xC3 : 0x00));
                b >>= 1;
            }
            return c;
        }

        /// <summary>
        /// Implements <see cref="IGrasshopper"/>.
        /// </summary>
        public byte[] R(byte[] input)
        {
            byte a_0 = 0x00;
            for (int i = 0; i < 16; i++)
            {
                a_0 ^= Multiply(input[i], linear[i]);
            }
            for (int i = 14; i >= 0; i--)
            {
                input[i + 1] = input[i];
            }
            input[0] = a_0;
            return input;
        }

        /// <summary>
        /// Implements <see cref="IGrasshopper"/>.
        /// </summary>
        public byte[] ReverseL(byte[] input)
        {
            for (int i = 0; i < 16; i++)
            {
                input = ReverseR(input);
            }
            return input;
        }

        /// <summary>
        /// Implements <see cref="IGrasshopper"/>.
        /// </summary>
        public byte[] ReverseR(byte[] input)
        {
            byte a_15 = input[0];
            for (int i = 0; i < 15; i++)
            {
                input[i] = input[i + 1];
            }
            input[15] = a_15;
            a_15 = 0x00;
            for (int i = 0; i < 16; i++)
            {
                a_15 ^= Multiply(linear[i], input[i]);
            }
            input[15] = a_15;
            return input;
        }

        /// <summary>
        /// Implements <see cref="IGrasshopper"/>.
        /// </summary>
        public byte[] ReverseS(byte[] input)
        {
            byte[] output = new byte[blockLength];
            for (int i = 0; i < blockLength; i++)
            {
                output[i] = inversePI[input[i]];
            }
            return output;
        }

        /// <summary>
        /// Implements <see cref="IGrasshopper"/>.
        /// </summary>
        public byte[] S(byte[] input)
        {
            byte[] output = new byte[blockLength];
            for (int i = 0; i < blockLength; i++)
            {
                output[i] = PI[input[i]];
            }
            return output;
        }

        /// <summary>
        /// Implements <see cref="IGrasshopper"/>.
        /// </summary>
        public byte[] XOR(byte[] a, byte[] b)
        {
            byte[] c = new byte[blockLength];
            for (int i = 0; i < blockLength; i++)
            {
                c[i] = (byte)(a[i] ^ b[i]);
            }
            return c;
        }
    }
}