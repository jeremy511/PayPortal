

using PayPortal.Core.Application.ViewModels.Products;

namespace PayPortal.Core.Application.ViewModels.SavingsAccount
{
    public class SavePaysViewModel
    {
        public int Id { get; set; }
        public string PaymentTo { get; set; }
        public string PaymentBy { get; set; }

        public string AmountOfMoney { get; set; }

        public int ProductsId { get; set; }

        public ProductViewModel Products { get; set; }
    }
}
