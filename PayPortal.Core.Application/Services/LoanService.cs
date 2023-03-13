
using AutoMapper;

using PayPortal.Core.Application.Interfaces.Repositories;
using PayPortal.Core.Application.Interfaces.Services;
using PayPortal.Core.Application.ViewModels.Client;
using PayPortal.Core.Application.ViewModels.SavingsAccount;
using PayPortal.Core.Domain.Entities;

namespace PayPortal.Core.Application.Services
{
    public class LoanService : GenericService<SaveLoanPaymentViewModel, LoanViewModel, Loan>, ILoanService
    {
        private readonly ILoanRepository _repository;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        

    }
}
