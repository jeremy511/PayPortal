using PayPortal.Core.Application.ViewModels.Users;
using AutoMapper;
using PayPortal.Core.Application.Helpers;
using PayPortal.Core.Application.Interfaces.Repositories;
using PayPortal.Core.Application.ViewModels.SavingsAccount;
using PayPortal.Core.Domain.Entities;
using PayPortal.Core.Application.Interfaces.Services;

namespace PayPortal.Core.Application.Services
{
    public class BeneficiaryService : GenericService<SaveBeneficiaryViewModel, BeneficiaryViewModel, Beneficiary>, IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _repository;
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly IMapper _mapper;

        public BeneficiaryService(IBeneficiaryRepository repository, IMapper mapper, ISavingsAccountRepository savingsAccount) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _savingsAccountRepository = savingsAccount;
        }

        public async Task<List<BeneficiaryViewModel>> ShowBeneficiary(string userId)
        {
            var BenificiaryList = await _repository.GetAllViewModel();
            var beneficiaries = BenificiaryList.Where(b => b.UserId == userId).Select(b => new BeneficiaryViewModel
            {
                Id = b.Id,
                FullName = b.FullName,
                AccountId = b.AccountId,
                UserId = userId,
            }).ToList();

            return beneficiaries;
           
        }

        public async Task<SaveBeneficiaryViewModel> Add(SaveBeneficiaryViewModel vm)
        {
            
           
            var CheckingForAccount = await _savingsAccountRepository.GetAllViewModel();
            SavingsAccount savings = CheckingForAccount.Where(acc => acc.Identifier == vm.AccountId).FirstOrDefault();
            
           


            if (savings != null )
            {

                Beneficiary beneficiary = _mapper.Map<Beneficiary>(vm);

                vm.FullName = savings.OwnerName;
                beneficiary.FullName = vm.FullName;
                await _repository.AddAsync(beneficiary);
                vm.HasMessage = false;
                vm.Message = "Beneficiario Agregado Correctamente!";
                return vm;

            }

     
            vm.HasMessage = true;
            vm.Message = "Numero de cuenta no existe, asegurate de introducir un numero de cuenta existente.";
            return vm;
            
           
        }

    }
}
