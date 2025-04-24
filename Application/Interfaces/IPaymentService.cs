using Application.Core;

namespace Application.Interfaces;

public interface IPaymentService
{
    Task<PaymentIntentResult> CreatePaymentIntentAsync(decimal amount);
}
