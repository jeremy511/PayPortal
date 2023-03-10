

namespace PayPortal.Core.Domain.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navegation Properties 

        public ICollection<SavingsAccount> SavingsAccount { get; set; }
        public ICollection<Loan> Loans { get; set; }
        public ICollection<Payments> Payments { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<CreditCard> CreditCards { get; set; }

    }
}
