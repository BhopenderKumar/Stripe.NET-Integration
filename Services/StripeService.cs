using Stripe;
using Stripe.Checkout;

namespace StripeIntegration.Services
{
    public class StripeService
    {
        public StripeService(IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
        }

        // Create a new payment session for a specific plan
        public Session CreateCheckoutSession(string planId)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
            {
                "card"
            },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = planId,
                    Quantity = 1,
                },
            },
                Mode = "subscription",
                SuccessUrl = "https://yourdomain.com/success",
                CancelUrl = "https://yourdomain.com/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return session;
        }

        // Create a plan
        public Price CreatePlan(string planName, long amount, string currency = "usd")
        {
            var options = new PriceCreateOptions
            {
                UnitAmount = amount,
                Currency = currency,
                Recurring = new PriceRecurringOptions
                {
                    Interval = "month",
                },
                ProductData = new PriceProductDataOptions
                {
                    Name = planName,
                },
            };

            var service = new PriceService();
            Price price = service.Create(options);
            return price;
        }
    }

}
