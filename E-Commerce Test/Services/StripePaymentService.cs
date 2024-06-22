using E_Commerce_Test.Interfaces;
using E_Commerce_Test.Models.Models;
using E_Commerce_Test.ViewModels;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;

namespace E_Commerce_Test.Services
{
    public class StripePaymentService: IPaymentService
    {
        /*private readonly StripeSettings _stripeSettings;

        public StripePaymentService(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }
        public async Task<Stripe.Checkout.Session> CreateCheckoutSessionAsync(string domain, List<SessionLineItemOptions> lineItems)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes= new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = $"{domain}/order/success",
                CancelUrl = $"{domain}/order/cancel"
            };
            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return session;
            
        }*/
        private readonly string _apiKey = "sk_test_51PU8eCGIIr8MbfCkeWyqnssae3LYBUU3xwB9ytw9Yvr0rSIu30XkFWTLOmrhIK22CNy7dRhXS8HyXegCvBH1KFRC00S2dS03Pa";

        public StripePaymentService()
        {
            StripeConfiguration.ApiKey = _apiKey;
        }

        public async Task ProcessPaymentAsync(OrderHeader order, PaymentDetailsViewModel moddel )
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long?)(order.OrderTotal * 100),
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" },
            };
            var service = new PaymentIntentService();
            PaymentIntent intent = service.Create(options);
            var confirmOptions = new PaymentIntentConfirmOptions
            {
                PaymentMethod = "pm_card_visa" // Using a test payment method provided by Stripe
            };
            PaymentIntent confirmedPaymentIntent = await service.ConfirmAsync(intent.Id, confirmOptions);

            if (confirmedPaymentIntent.Status != "succeeded")
            {
                throw new Exception("Payment failed");
            }
        }
    }
}
