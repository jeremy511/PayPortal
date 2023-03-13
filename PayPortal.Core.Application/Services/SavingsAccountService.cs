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
    public class SavingsAccountService : GenericService<SaveSavingsViewModel, SavingsViewModel, SavingsAccount>, ISavingsAccountService
    {
        private readonly ISavingsAccountRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _payment;
        private readonly ITransactionRepository _transaction;

        public SavingsAccountService(ISavingsAccountRepository repository, IMapper mapper, IPaymentRepository payment, ITransactionRepository transaction) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _payment = payment;
            _transaction = transaction;
        }

        public async Task AddMainAsync(SaveUserViewModel saveView)
        {
            string RandomID = RandomId.GenerateId();
            var CheckingUniqueID = await _repository.GetAllViewModel();
            for (int i = 0; i < 1; i++)
            {
                var CheckingId = CheckingUniqueID.Where(x => x.Identifier == RandomID).Select(x => new SavingsViewModel { Identifier = x.Identifier }).ToList();
                if (CheckingId.Count == 0)
                {
                    SavingsAccount savings = new();
                    savings.IsMain = 1;
                    savings.Identifier = RandomId.GenerateId();
                    savings.Amount = (double)saveView.StartingAmount;
                    savings.OwnerUserName = saveView.UserName;
                    savings.OwnerName = saveView.FirstName + saveView.LastName;
                    savings.ProductsId = 1;
                    await _repository.AddAsync(savings);


                }
                else
                {
                    RandomID = RandomId.GenerateId();
                    i = i - 1;
                }
            }

        }

        public async Task<PaymentResponse> ValidatePayment(SaveExpressPaymentViewModel viewModel)
        {
            var SavingsList = await _repository.GetAllViewModel();
            var CheckingForAccount = SavingsList.Where(s => s.Identifier == viewModel.DestinaryAccount).FirstOrDefault();
            PaymentResponse response = new();

            if (CheckingForAccount != null)
            {
                SavingsAccount DestinaryAccount = SavingsList.Where(s => s.Identifier == viewModel.DestinaryAccount).FirstOrDefault();
                SavingsAccount SenderAccount = SavingsList.Where(s => s.Identifier == viewModel.FromAccount).FirstOrDefault();

                if (SenderAccount.Amount < viewModel.Amount)
                {
                    response.HasError = true;
                    response.Message = "No tiene ese monto en esa cuenta";
                    return response;
                }

                response.FullName = DestinaryAccount.OwnerName;
                response.PaymentAmount = viewModel.Amount;
                response.SenderAccount = SenderAccount.Id;
                response.DestinaryAccount = DestinaryAccount.Id;
                response.HasError = false;

                return response;
            }

            response.HasError = true;
            response.Message = "No se encontro cuenta de ahorro con ese numero de cuenta, asegurese de introducir un numero de cuenta valido";
            return response;
        }


        public async Task<PaymentResponse> ExpressPayment(PaymentResponse payment)
        {
            SavingsAccount Destinary = await _repository.GetViewModelById(payment.DestinaryAccount);
            SavingsAccount Sender = await _repository.GetViewModelById(payment.SenderAccount);

            Sender.Amount = Sender.Amount - payment.PaymentAmount;
            await _repository.UpdateAsyn(Sender,Sender.Id);

            Destinary.Amount = Destinary.Amount + payment.PaymentAmount;
            await _repository.UpdateAsyn(Destinary, Destinary.Id);

            payment.Message = "Pago completado con exito!";

            Payments payments = new();
            payments.PaymentTo = Destinary.Identifier;
            payments.PaymentBy = Sender.Identifier;
            payments.AmountOfMoney = payment.PaymentAmount;
            payments.ProductsId = 1; 

            await _payment.AddAsync(payments);

            return payment;


        }

       public async Task<SaveTransferViewModel> AccountTransfer(SaveTransferViewModel vm)
        {
            SavingsAccount Destinary = await _repository.GetViewModelById(vm.DestinaryAccount);
            SavingsAccount Sender = await _repository.GetViewModelById(vm.FromAccount);

           if (Sender.Amount < vm.Amount)
            {
                vm.HasError = true;
                vm.Message = "la cantidad que intentas transferir es mayor a la cantidad que tienes actualmente.";
                return vm;
            }


            Transaction transaction = new();
            transaction.TransactionTo = Destinary.Identifier;
            transaction.TransactionBy = Sender.Identifier;
            transaction.AmountOfMoney = vm.Amount;
            transaction.ProductsId = 1;
            await _transaction.AddAsync(transaction);

            Destinary.Amount = Destinary.Amount + vm.Amount;
            Sender.Amount = Sender.Amount - vm.Amount;
            await _repository.UpdateAsyn(Destinary, Destinary.Id);
            await _repository.UpdateAsyn(Sender, Sender.Id);
            vm.Message = "Transferencia realizada correctamente!";
            vm.HasError = false;
            return vm;
        }

            

    }
}
