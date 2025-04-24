using Application.Core;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Services;

public class PaymentService(IConfiguration config) : IPaymentService
{
    public async Task<PaymentIntentResult> CreatePaymentIntentAsync(decimal amount)
    {
        StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];

        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(amount * 100),
            Currency = "eur",
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true
            }
        };

        var service = new PaymentIntentService();

        var paymentIntent = await service.CreateAsync(options);

        return new PaymentIntentResult
        {
            ClientSecret = paymentIntent.ClientSecret,
            PaymentIntentId = paymentIntent.Id
        };
    }
}
