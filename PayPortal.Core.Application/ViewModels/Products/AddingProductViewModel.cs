
using System.ComponentModel.DataAnnotations;

namespace PayPortal.Core.Application.ViewModels.Products
{
    public class AddingProductViewModel
    {
       
        public string ? Id { get; set; }
        public string UserName { get; set; }

        [Range(1, int.MaxValue,ErrorMessage = "Seleccione un producto")]
        public int ProductType { get; set; }

        [DataType(DataType.Currency)]
        public double ? CardLimit{ get; set; }

        [DataType(DataType.Currency)]
        public double ? LoanAmount { get; set; }

        [DataType(DataType.Currency)]
        public double? StartingAmount { get; set; }
    }
}
