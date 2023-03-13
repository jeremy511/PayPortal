using PayPortal.Core.Application.Dtos.Payment;
using PayPortal.Core.Application.ViewModels.Client;
using PayPortal.Core.Application.ViewModels.SavingsAccount;
using PayPortal.Core.Application.ViewModels.Users;
using PayPortal.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Application.Interfaces.Services
{
    public interface ISavingsAccountService : IGenericService<SaveSavingsViewModel, SavingsViewModel, SavingsAccount>
    {
        Task AddMainAsync(SaveUserViewModel saveView);
        Task<PaymentResponse> ValidatePayment(SaveExpressPaymentViewModel viewModel);
        Task<PaymentResponse> ExpressPayment(PaymentResponse payment);
        Task<SaveTransferViewModel> AccountTransfer(SaveTransferViewModel vm);
    }
}
