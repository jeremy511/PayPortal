using PayPortal.Core.Application.Dtos.Account;
using PayPortal.Core.Application.ViewModels.Users;
using AutoMapper;
using PayPortal.Core.Application.ViewModels.Products;
using PayPortal.Core.Application.ViewModels.SavingsAccount;
using PayPortal.Core.Domain.Entities;

namespace PayPortal.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<RegisterRequest, EditUserViewModel>()
              .ForMember(x => x.Error, opt => opt.Ignore())
              .ForMember(x => x.HasError, opt => opt.Ignore())
              .ForMember(x => x.AditionalAmount, opt => opt.Ignore())

              .ReverseMap();

            CreateMap<SavingsAccount, SaveSavingsViewModel>()
               .ReverseMap()
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ForMember(x => x.CreatedDate, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
               .ForMember(x => x.Products, opt => opt.Ignore());

            CreateMap<SavingsAccount, SavingsViewModel>()
               .ReverseMap()
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ForMember(x => x.CreatedDate, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Products, SaveProductViewModel>()
               .ReverseMap()
               .ForMember(x => x.SavingsAccount, opt => opt.Ignore());



            CreateMap<Products, ProductViewModel>()
               .ReverseMap();

            CreateMap<CreditCard, CreditCardViewModel>()
             .ReverseMap();

            CreateMap<Loan, LoanViewModel>()
           .ReverseMap();


            CreateMap<Beneficiary, SaveBeneficiaryViewModel>()
           .ReverseMap()
           .ForMember(x => x.CreatedBy, opt => opt.Ignore())
           .ForMember(x => x.CreatedDate, opt => opt.Ignore())
           .ForMember(x => x.LastModified, opt => opt.Ignore())
           .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Beneficiary, BeneficiaryViewModel>()
          .ReverseMap()
          .ForMember(x => x.CreatedBy, opt => opt.Ignore())
          .ForMember(x => x.CreatedDate, opt => opt.Ignore())
          .ForMember(x => x.LastModified, opt => opt.Ignore())
          .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());


            CreateMap<Payments, SavePaysViewModel>()
              .ReverseMap()
              .ForMember(x => x.CreatedBy, opt => opt.Ignore())
              .ForMember(x => x.CreatedDate, opt => opt.Ignore())
              .ForMember(x => x.LastModified, opt => opt.Ignore())
              .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Transaction, SaveTransactionViewModel>()
              .ReverseMap()
              .ForMember(x => x.CreatedBy, opt => opt.Ignore())
              .ForMember(x => x.CreatedDate, opt => opt.Ignore())
              .ForMember(x => x.LastModified, opt => opt.Ignore())
              .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());



        }
    }
}
