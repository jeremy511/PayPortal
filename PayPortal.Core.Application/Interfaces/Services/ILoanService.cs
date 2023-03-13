using PayPortal.Core.Application.ViewModels.Client;
using PayPortal.Core.Application.ViewModels.SavingsAccount;
using PayPortal.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Application.Interfaces.Services
{
    public interface ILoanService : IGenericService<SaveLoanPaymentViewModel, LoanViewModel, Loan>
    {
    }
}
