
namespace PayPortal.Core.Application.ViewModels.SavingsAccount
{
    public class SaveSavingsViewModel
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public double Amount { get; set; }
        public int IsMain { get; set; }
        public string OwnerName { get; set; }
        public string OwnerUserName { get; set; }
        public string OwenerId { get; set; }
    }
}
