using PayPortal.Core.Application.Services;
using PayPortal.Core.Application.ViewModels.SavingsAccount;
using PayPortal.Core.Domain.Entities;


namespace PayPortal.Core.Application.Interfaces.Services
{
    public interface IBeneficiaryService: IGenericService<SaveBeneficiaryViewModel, BeneficiaryViewModel, Beneficiary>
    {
        Task<List<BeneficiaryViewModel>> ShowBeneficiary(string userId);
        Task<SaveBeneficiaryViewModel> Add(SaveBeneficiaryViewModel vm);
    }
}
