using cardValidate.Controllers;

namespace CardValidationAPI.Tests
{
    [TestClass]
    public class CardValidatorTests
    {
        //card numbers obtained from https://support.bluesnap.com/docs/test-credit-card-numbers

        [TestMethod]
        public void TestInvalidCard()
        {
            var result = CardValidator.Validate("1234567890123456");
            Assert.IsFalse(result.IsValid);
        }


        [TestMethod]
        public void TestValidVisaCard()
        {
            var result = CardValidator.Validate("4263982640269299");
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual("Visa", result.CardType);
        }


        [TestMethod]
        public void TestValidAmexCard()
        {
            var result = CardValidator.Validate("374245455400126");
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual("AmEx", result.CardType);
        }


        [TestMethod]
        public void TestValidMastercardOne()
        {
            var result = CardValidator.Validate("5425233430109903");
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual("Mastercard", result.CardType);
        }
        [TestMethod]
        public void TestValidMastercardTwo()
        {
            var result = CardValidator.Validate("2222420000001113");
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual("Mastercard", result.CardType);
        }
    }
}
