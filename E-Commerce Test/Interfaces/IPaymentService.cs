using E_Commerce_Test.Models.Models;
using E_Commerce_Test.ViewModels;
using Stripe.Climate;

namespace E_Commerce_Test.Interfaces
{
    public interface IPaymentService
    {
        Task ProcessPaymentAsync(OrderHeader order,PaymentDetailsViewModel model);
    }
}
