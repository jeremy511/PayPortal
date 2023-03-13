using PayPortal.Core.Application.Dtos.Payment;
using PayPortal.Core.Application.Dtos.Products;
using PayPortal.Core.Application.ViewModels.Admin;
using PayPortal.Core.Application.ViewModels.Client;
using PayPortal.Core.Application.ViewModels.Products;
using PayPortal.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Application.Interfaces.Services
{
    public interface IProductService : IGenericService<SaveProductViewModel, ProductViewModel, Products>
    {
        Task AddAsync();
        Task<List<ShowProductsViewModel>> GetAllProducts(string id);
        Task<AddResponse> AddingProducts(AddingProductViewModel vm);
        Task<DeleteResponse> DeleteProducts(string id);

        Task<AdminViewModel> ShowDataAsync();

        Task<SaveCreditPaymentViewModel> CreditCardPayment(SaveCreditPaymentViewModel vm);

        Task<SaveLoanPaymentViewModel> LoanPayment(SaveLoanPaymentViewModel vm);

        Task<PaymentBResponse> BeneficiaryPayment(SaveBeneficiaryPaymentViewModel vm, int opt);

        Task<SaveCreditPaymentViewModel> PaymentInAdvance(SaveCreditPaymentViewModel vm);
    }
}
