using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Service.Interfaces;
using Services.Business.Security;
using Services.Business.Services;

namespace sstu_nevdev.Tests.Services
{
    [TestClass]
    public class AESServiceTest
    {
        private IAESService serviceMock;
        private IRSA serviceRSAMock;

        [TestInitialize]
        public void Initialize()
        {
            serviceMock = new AESService();
            serviceRSAMock = new RSA();
        }

        [TestMethod]
        public void Encrypt_Decrypt()
        {
            //Arrange
            dynamic json = new JObject();
            json.Data = "message";

            //Act
            serviceRSAMock.GenerateKey();
            var cipherbytes = serviceMock.Encrypt(json);
            var plainbytes = serviceMock.Decrypt(cipherbytes);

            //Assert
            Assert.IsNotNull(cipherbytes);
            Assert.AreEqual(Newtonsoft.Json.JsonConvert.SerializeObject(json),
                Newtonsoft.Json.JsonConvert.SerializeObject(plainbytes));
        }
    }
}