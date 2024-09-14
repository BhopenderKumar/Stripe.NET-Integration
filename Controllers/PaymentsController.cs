using Microsoft.AspNetCore.Mvc;
using StripeIntegration.Services;

[Route("payments")]
public class PaymentsController : Controller
{
    private readonly StripeService _stripeService;

    public PaymentsController(StripeService stripeService)
    {
        _stripeService = stripeService;
    }

    // Endpoint to create a payment session
    [HttpPost("checkout-session")]
    public IActionResult CreateCheckoutSession(string planId)
    {
        var session = _stripeService.CreateCheckoutSession(planId);
        return Json(new { id = session.Id });
    }

    // Endpoint to create a new plan
    [HttpPost("create-plan")]
    public IActionResult CreatePlan(string planName, long amount)
    {
        var price = _stripeService.CreatePlan(planName, amount);
        return Json(new { id = price.Id });
    }
}
