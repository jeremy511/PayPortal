

using PayPortal.Core.Application.ViewModels.Products;

namespace PayPortal.Core.Application.ViewModels.SavingsAccount
{
    public class SaveTransactionViewModel
    {
        public string TransactionTo { get; set; }
        public string TransactionBy { get; set; }

        public string AmountOfMoney { get; set; }

        public int ProductsId { get; set; }

        public ProductViewModel Products { get; set; }
    }
}
