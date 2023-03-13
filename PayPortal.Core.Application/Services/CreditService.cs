using PayPortal.Core.Application.ViewModels.Users;
using AutoMapper;
using PayPortal.Core.Application.Dtos.Payment;
using PayPortal.Core.Application.Helpers;
using PayPortal.Core.Application.Interfaces.Repositories;
using PayPortal.Core.Application.ViewModels.Client;
using PayPortal.Core.Application.ViewModels.SavingsAccount;
using PayPortal.Core.Domain.Entities;
using PayPortal.Core.Application.Interfaces.Services;

namespace PayPortal.Core.Application.Services
{
    public class CreditService : GenericService<SaveCreditPaymentViewModel, CreditCardViewModel, CreditCard>, ICreditService
    {
        private readonly ICreditCardRepository _repository;
        private readonly IMapper _mapper;

        public CreditService(ICreditCardRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        

    }
}
