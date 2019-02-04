using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Service.Interfaces;
using Services.Business.Security;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace sstu_nevdev.Tests.Algorithms
{
    [TestClass]
    public class GrasshopperTest
    {
        private IGrasshopper serviceMock;

        [TestInitialize]
        public void Initialize()
        {
            serviceMock = new Grasshopper();
        }

        [TestMethod]
        public void Encrypt_Decrypt()
        {
            //Arrange
            string message = "message";
            dynamic json = new JObject();
            json.Data = message;
            byte[] key =
            {
                0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff,
                0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
                0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10,
                0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef
            };
            AESHelper aESHelper = new AESHelper();
            byte[] data = aESHelper.Normalize(json);
            byte[] cipherbytes;

            //Act
            using (MemoryStream stream = new MemoryStream(data))
            {
                byte[] block = new byte[16];
                cipherbytes = new byte[0];
                while (stream.Read(block, 0, 16) > 0)
                {
                    Array.Resize(ref cipherbytes, cipherbytes.Length + 16);
                    Array.Copy(serviceMock.Encrypt(block, key), 0, cipherbytes, cipherbytes.Length - 16, 16);
                }
            }
            byte[] plainbytes = new byte[cipherbytes.Length];
            using (MemoryStream stream = new MemoryStream(cipherbytes))
            {
                byte[] block = new byte[16];
                int count = 0;
                while (stream.Read(block, 0, 16) > 0)
                {
                    Array.Copy(serviceMock.Decrypt(block, key), 0, plainbytes, count, 16);
                    count += 16;
                }
            }
            var result = aESHelper.ParseToJson(plainbytes);

            //Assert
            Assert.IsNotNull(cipherbytes);
            Assert.IsNotNull(plainbytes);
            Assert.AreEqual(cipherbytes.Length, plainbytes.Length);
            Assert.AreEqual(Newtonsoft.Json.JsonConvert.SerializeObject(json),
                Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }

        [TestMethod]
        public void Generate_Grasshopper_Key()
        {
            //Act
            serviceMock.GenerateKey();

            //Assert
            Assert.IsTrue(true);
        }
    }
}