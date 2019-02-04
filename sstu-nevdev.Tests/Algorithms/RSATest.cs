using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Interfaces;
using Services.Business.Security;
using System.Security.Cryptography;
using System.Text;
using RSA = Services.Business.Security.RSA;

namespace sstu_nevdev.Tests.Algorithms
{
    [TestClass]
    public class RSATest
    {
        private IRSA serviceMock;

        [TestInitialize]
        public void Initialize()
        {
            serviceMock = new RSA();
        }

        [TestMethod]
        public void Decrypt_From_Client()
        {
            //Arrange
            string message = "message";
            byte[] data = Encoding.UTF8.GetBytes(message);
            serviceMock.GenerateKey();
            string PK = Keys.PublicKey;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PK);
            byte[] cipherbytes = rsa.Encrypt(data, false);
            
            //Act
            var plainbytes = serviceMock.RequestKey(cipherbytes);

            //Assert
            Assert.IsNotNull(plainbytes);
            Assert.IsInstanceOfType(plainbytes, typeof(string));
            Assert.AreEqual(message, plainbytes);
        }

        [TestMethod]
        public void Encrypt_To_Client()
        {
            //Arrange
            string PK = "<RSAKeyValue><Modulus>rQaN8F5SI3HZ0lR3gEY7RoDGFx/hOwYaGH0cTiy1/" +
                   "je41yE2TIRSlEgme22dJ9Sn8Q9C8V9Wg6mZ6IyBBPoED/lcdP6m7fFBAwCVzZ+TYcwdkC8vGw0" +
                   "YfWON0ya31bhgAtq4fDgfV1271RGelYzapx9kDJd+Dj/LxBE0NHXxjAk=</Modulus><Exponent>" +
                   "AQAB</Exponent></RSAKeyValue>";
            byte[] message = Encoding.UTF8.GetBytes("message");

            //Act
            var cipherbytes = serviceMock.ResponseKey(message, PK);

            //Assert
            Assert.IsNotNull(cipherbytes);
            Assert.IsInstanceOfType(cipherbytes, typeof(byte[]));
        }

        [TestMethod]
        public void Generate_RSA_Keys()
        {
            //Act
            serviceMock.GenerateKey();

            //Assert
            Assert.IsNotNull(Keys.PublicKey);
        }

        [TestMethod]
        public void Is_Equals_Result()
        {
            //Arrange
            byte[] message = Encoding.UTF8.GetBytes("message");
            string SK = "<RSAKeyValue><Modulus>rQaN8F5SI3HZ0lR3gEY7RoDGFx/hOwYaGH0cTiy1/je41yE2TI" +
                "RSlEgme22dJ9Sn8Q9C8V9Wg6mZ6IyBBPoED/lcdP6m7fFBAwCVzZ+TYcwdkC8vGw0YfWON0ya31bhgAt" +
                "q4fDgfV1271RGelYzapx9kDJd+Dj/LxBE0NHXxjAk=</Modulus><Exponent>AQAB</Exponent><P>" +
                "5rGNQDfyrSaNczuDtV9p+NxJ5/NxQaiqbkdGAViwuvki6ffKCeWTMGwWU+kNlEkZm5jh+pApXBLE4qvx" +
                "AtrLnw==</P><Q>wAGLDZdcQ4xs3DAYSrW98EKIWPlNBoQ5ZXnLh/xVNbLAUzpqn0IcgAGcoYqQNR51v" +
                "c/COFpzd+i2RiAiD9sHVw==</Q><DP>DPmTfjcnWQHAFukUAVF6flq1dWxFxHGeFFHB6DV6yylUA2DCZ" +
                "kgZPTH3F4UWFG8AF7ZDj3ooOVt841rUVVrE9Q==</DP><DQ>BzaHoMUU5Dy8QjFUWEonjoURVjZXXG1P" +
                "Mq62pK8oDFJgwz+ojb8QDwcAeVkZPcWdKrpJU5CiUdjeMBg471uNFQ==</DQ><InverseQ>arL2Yjq/7" +
                "3RofGP310n5B0QxrqqiE9zP20AZ1lZCYFgyF++2iZ2kNgq0QO/fLRAO8QrFmSQXTS2Imsa/AdAuIQ==<" +
                "/InverseQ><D>W6QoE1JxoLJRUxRHwtnv1TSpNmA9M7zUn3nMPx9xOPccYF0H3FkNP9pC/4acAReh54x" +
                "UwXDkapGuobuhLhiZg1iGuUA0w9NQZwwms+esvE4c7E3AOlBWFJxUL4wDZNd58HDHSSlQxC+HPYu7ukR" +
                "7tfC3aqAyc5VN2P+2/1AGts0=</D></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(SK);

            //Act
            var cipherbytes = serviceMock.Encrypt(message, rsa.ExportParameters(false));
            var plainbytes = serviceMock.Decrypt(cipherbytes, rsa.ExportParameters(true));

            //Assert
            Assert.IsNotNull(cipherbytes);
            Assert.IsInstanceOfType(cipherbytes, typeof(byte[]));
            Assert.IsInstanceOfType(plainbytes, typeof(byte[]));
            CollectionAssert.AreEqual(message, plainbytes);
        }
    }
}