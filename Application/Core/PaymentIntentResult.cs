namespace Application.Core;

public class PaymentIntentResult
{
    public required string ClientSecret { get; set; }
    public required string PaymentIntentId { get; set; }
}
