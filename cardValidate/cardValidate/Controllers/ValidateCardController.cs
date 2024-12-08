using Microsoft.AspNetCore.Mvc;

namespace cardValidate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidateCardController : ControllerBase
    {
        [HttpPost]
        public IActionResult ValidateCard([FromBody] CardValidationRequest request)
        {
            var result = CardValidator.Validate(request.CardNumber);
            return Ok(result);
        }
    }

    public class CardValidationRequest
    {
        public string CardNumber { get; set; }
    }

    public static class CardValidator
    {
        public static CardValidationResponse Validate(string cardNumber)
        {
            if (!LuhnCheck(cardNumber))
                return new CardValidationResponse { IsValid = false };

            string cardType = GetCardType(cardNumber);
            return new CardValidationResponse
            {
                IsValid = !string.IsNullOrEmpty(cardType),
                CardType = cardType
            };
        }

        private static string GetCardType(string cardNumber)
        {
            if (cardNumber.StartsWith("34") || cardNumber.StartsWith("37") && cardNumber.Length == 15)
                return "AmEx";
            if (cardNumber.StartsWith("4") && cardNumber.Length == 16)
                return "Visa";
            if ((cardNumber.StartsWith("22") || int.Parse(cardNumber.Substring(0, 2)) >= 51 && int.Parse(cardNumber.Substring(0, 2)) <= 55) && cardNumber.Length == 16)
                return "Mastercard";
            if (cardNumber.StartsWith("6011") && cardNumber.Length == 16)
                return "Discover";
            return null;
        }

        private static bool LuhnCheck(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(cardNumber[i].ToString());
                if (alternate)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }
                sum += n;
                alternate = !alternate;
            }
            return sum % 10 == 0;
        }
    }

    public class CardValidationResponse
    {
        public bool IsValid { get; set; }
        public string CardType { get; set; }
    }
}
