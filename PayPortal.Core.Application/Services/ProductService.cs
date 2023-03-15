using PayPortal.Core.Application.Dtos.Account;

using AutoMapper;
using PayPortal.Core.Application.Dtos.Payment;
using PayPortal.Core.Application.Dtos.Products;
using PayPortal.Core.Application.Helpers;
using PayPortal.Core.Application.Interfaces.Repositories;
using PayPortal.Core.Application.ViewModels.Admin;
using PayPortal.Core.Application.ViewModels.Client;
using PayPortal.Core.Application.ViewModels.Products;
using PayPortal.Core.Application.ViewModels.SavingsAccount;
using PayPortal.Core.Domain.Entities;
using PayPortal.Core.Application.Interfaces.Services;

namespace PayPortal.Core.Application.Services
{
    public class ProductService : GenericService<SaveProductViewModel,ProductViewModel,Products>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly ICreditCardRepository _creditCard;
        private readonly ILoanRepository _loanRepository;
        private readonly IAccountService _accountService;
        private readonly IBeneficiaryRepository _bRepository;
        private readonly ITransactionRepository _transaction;
        private readonly IPaymentRepository _payment;
       

        public ProductService(IProductRepository repository, IMapper mapper, ISavingsAccountRepository savingsAccountRepository, ICreditCardRepository creditCard, ILoanRepository loanRepository, IAccountService accountService, IBeneficiaryRepository biographyRepository, ITransactionRepository transaction, IPaymentRepository payment) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _savingsAccountRepository = savingsAccountRepository;
            _creditCard = creditCard;
            _loanRepository = loanRepository;
            _accountService = accountService;
            _bRepository = biographyRepository;
            _transaction = transaction;
            _payment = payment;
        }


        public async Task<AdminViewModel> ShowDataAsync()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            var Savings = await _savingsAccountRepository.GetAllViewModel();
            var Cards = await _creditCard.GetAllViewModel();
            var Loans = await _loanRepository.GetAllViewModel();
            var UsersList = await _accountService.GetAllUsers();
            var Transactions = await _transaction.GetAllViewModel();
            var Payments = await _payment.GetAllViewModel();

            adminViewModel.Products = Savings.Count + Loans.Count + Cards.Count;
            adminViewModel.ActiveClient = UsersList.Where(users => users.IsActive == true).Count();
            adminViewModel.DisableClient = UsersList.Where(users => users.IsActive == false).Count();
            adminViewModel.TotalTransactions = Transactions.Count;
            adminViewModel.TodayTransactions = Transactions.Where(transaction => transaction.CreatedDate.Value.Day == DateTime.Now.Day && transaction.CreatedDate.Value.Month == DateTime.Now.Month).Count();
            adminViewModel.TotalPays = Payments.Count;
            adminViewModel.TodayPays = Payments.Where(pays => pays.CreatedDate.Value.Day == DateTime.Now.Day && pays.CreatedDate.Value.Month == DateTime.Now.Month).Count();
            adminViewModel.Loans = Loans.Count;
            adminViewModel.Savings = Savings.Count;
            adminViewModel.Credits = Cards.Count;



            return adminViewModel;

        }

        public  async Task AddAsync()
        {
            string[] products = new string[] { "Cuenta de ahorro", "Tarjeta de crédito", "Préstamo" };
            foreach (var product in products)
            {
                Products products1 = new();
                products1.Name = product;
                await _repository.AddAsync(products1);
            }

        }

        public async Task<List<ShowProductsViewModel>> GetAllProducts(string id)
        {
            List<ShowProductsViewModel> products = new List<ShowProductsViewModel>();
            var SavingsAccount = await _savingsAccountRepository.GetAllViewModel();
            var CreditCard = await _creditCard.GetAllViewModel();
            var Loan = await _loanRepository.GetAllViewModel();
            RegisterRequest User = await _accountService.GetUserById(id);


            foreach (var product in SavingsAccount.Where(s => s.OwnerUserName == User.UserName))
            {
                ShowProductsViewModel showProducts = new();
                showProducts.Identifier = product.Identifier;
                showProducts.Type = "Cuenta de Ahorros";
                showProducts.Id = product.Id;
                showProducts.UserName = product.OwnerUserName;
                showProducts.Owed = 0;
                showProducts.Amount = product.Amount;
                products.Add(showProducts);
            }
            foreach (var product in CreditCard.Where(c => c.OwnerUserName == User.UserName))
            {
                ShowProductsViewModel showProducts = new();
                showProducts.Identifier = product.Identifier;
                showProducts.Type = "Tarjeta de Credito";
                showProducts.Id = product.Id;
                showProducts.Owed = product.Owed;
                showProducts.UserName = product.OwnerUserName; 
                products.Add(showProducts);
            }
            foreach (var product in Loan.Where(l => l.OwnerUserName == User.UserName))
            {
                ShowProductsViewModel showProducts = new();
                showProducts.Identifier = product.Identifier;
                showProducts.Type = "Prestamo";
                showProducts.Id = product.Id;
                showProducts.Owed = product.Owed;
                showProducts.UserName = product.OwnerUserName; 
                products.Add(showProducts);
            }



            return products;
        }

        public async Task<AddResponse> AddingProducts(AddingProductViewModel vm)
        {
            RegisterRequest User  = await _accountService.GetUserById(vm.UserName);
            AddResponse response = new ();


            if (vm.ProductType == 1)
            {
                string RandomID = RandomId.GenerateId();
                var CheckingUniqueID = await _savingsAccountRepository.GetAllViewModel();
                for (int i = 0; i < 1; i++)
                {
                    var CheckingId = CheckingUniqueID.Where(x => x.Identifier == RandomID).Select(x => new SavingsViewModel { Identifier = x.Identifier }).ToList();
                    if (CheckingId.Count == 0)
                    {
                        SavingsAccount savings = new();
                        savings.IsMain = 0;
                        savings.Identifier = RandomId.GenerateId();
                        if (vm.StartingAmount != null || vm.StartingAmount == 0 ) {
                            savings.Amount = (double)vm.StartingAmount; 
                        }
                        else
                        {
                            savings.Amount = 0;

                        }
                        savings.OwnerUserName = User.UserName;
                        savings.OwnerName = User.FirstName + " " + User.LastName;
                        savings.ProductsId = 1;
                        response.Message = "Producto agregado correctamente.";
                        response.UserId = User.Id;
                        await _savingsAccountRepository.AddAsync(savings);


                    }
                    else
                    {
                        RandomID = RandomId.GenerateId();
                        i = i - 1;
                    }
                }

            }
            if (vm.ProductType == 2)
            {
                string RandomID = RandomId.GenerateId();
                var CheckingUniqueID = await _creditCard.GetAllViewModel();
                for (int i = 0; i < 1; i++)
                {
                    var CheckingId = CheckingUniqueID.Where(x => x.Identifier == RandomID).Select(x => new CreditCardViewModel { Identifier = x.Identifier }).ToList();
                    if (CheckingId.Count == 0)
                    {
                        CreditCard credit = new();
                        credit.Identifier = RandomId.GenerateId();
                        credit.Limit = (double)vm.CardLimit;
                        credit.OwnerUserName = User.UserName;
                        credit.OwnerName = User.FirstName + " " + User.LastName;
                        credit.ProductsId = 2;
                        response.Message = "Producto agregado correctamente.";
                        response.UserId = User.Id;
                        await _creditCard.AddAsync(credit);
                    }
                    else
                    {
                        RandomID = RandomId.GenerateId();
                        i = i - 1;
                    }
                }

            }
            if (vm.ProductType == 3)
            {
                string RandomID = RandomId.GenerateId();
                var CheckingUniqueID = await _loanRepository.GetAllViewModel();
                var GettingMain = await _savingsAccountRepository.GetAllViewModel();
                SavingsAccount Main = GettingMain.Where(s => s.IsMain == 1 && s.OwnerUserName == User.UserName).FirstOrDefault();

                for (int i = 0; i < 1; i++)
                {
                    var CheckingId = CheckingUniqueID.Where(x => x.Identifier == RandomID).Select(x => new LoanViewModel { Identifier = x.Identifier }).ToList();
                    if (CheckingId.Count == 0)
                    {
                        Loan loan = new();
                        loan.Identifier = RandomID;
                        loan.Amount = (double)vm.LoanAmount;
                        loan.OwnerUserName = User.UserName;
                        loan.OwnerName = User.FirstName + " " + User.LastName;
                        loan.ProductsId = 3;
                        loan.Owed = (double) vm.LoanAmount;
                        Main.Amount = (double)(Main.Amount + vm.LoanAmount);
                        response.Message = "Producto agregado correctamente.";
                        response.UserId = User.Id;
                        await _loanRepository.AddAsync(loan);
                        await _savingsAccountRepository.UpdateAsyn(Main, Main.Id);

                    }
                    else
                    {
                        RandomID = RandomId.GenerateId();
                        i = i - 1;
                    }
                }

            }

            return response;
        }


        public async Task<DeleteResponse> DeleteProducts(string id)
        {
            string[] split = id.Split("-");
            int _id = Convert.ToInt32 (split[0]);
            int type = Convert.ToInt32(split[1]);
            DeleteResponse response = new();

            switch (type)
            {
                case 1:

                    SavingsAccount savingsAccount = await _savingsAccountRepository.GetViewModelById(_id);
                    RegisterRequest GettingUserId = await _accountService.GetUserByName(savingsAccount.OwnerUserName);

                    if (savingsAccount.IsMain == 1)
                    {
                        response.UserId = GettingUserId.Id;
                        response.Message = $"El producto de tipo Cuenta de ahorro con identificador {savingsAccount.Identifier} no se puede eliminar ya que es de tipo cuenta principal.";
                        return response;
                    }
                    else
                    {
                        var GettingMain = await _savingsAccountRepository.GetAllViewModel();
                        SavingsAccount Main = GettingMain.Where(s => s.IsMain == 1 && s.OwnerUserName == savingsAccount.OwnerUserName).FirstOrDefault();
                        response.UserId = GettingUserId.Id;
                        Main.Amount = Main.Amount + savingsAccount.Amount;
                        await _savingsAccountRepository.DeleteAsync(savingsAccount);
                        await _savingsAccountRepository.UpdateAsyn(Main, Main.Id);
                        response.Message = "Producto eliminado correctamente.";
                        return response;

                    }
                    break;
                case 2:
                    CreditCard creditCard = await _creditCard.GetViewModelById(_id);
                    RegisterRequest GettingUserId2 = await _accountService.GetUserByName(creditCard.OwnerUserName);

                    if (creditCard.Owed > 0)
                    {
                        response.UserId = GettingUserId2.Id;
                        response.Message = $"La tarjeta de credito con identificador {creditCard.Identifier} no se puede eliminar ya que aun debe {creditCard.Owed}";
                    }
                    else
                    {
                        response.UserId = GettingUserId2.Id;

                        response.Message = "Producto eliminado correctamente.";
                        await _creditCard.DeleteAsync(creditCard);
                    }
                
                    break;
                case 3:

                    Loan Loan = await _loanRepository.GetViewModelById(_id);
                    RegisterRequest GettingUserId3 = await _accountService.GetUserByName(Loan.OwnerUserName);

                    if (Loan.Owed > 0)
                    {
                        response.UserId = GettingUserId3.Id;
                        response.Message = $"El Prestamo con identificador {Loan.Identifier} no se puede eliminar ya que aun debe {Loan.Owed}";
                        return response;
                    }
                    else
                    {
                        response.UserId = GettingUserId3.Id;
                        response.Message = "Producto eliminado correctamente.";
                        await _loanRepository.DeleteAsync(Loan);
                        return response;
                    }
                    break;

            }
            
            return response;
        }

        public async Task<SaveCreditPaymentViewModel> CreditCardPayment(SaveCreditPaymentViewModel vm)
        {

            CreditCard creditCard = await _creditCard.GetViewModelById(vm.CreditCard);
            SavingsAccount savings = await _savingsAccountRepository.GetViewModelById(vm.FromAccount);

    

            Payments payments = new();
            payments.PaymentTo = creditCard.Identifier;
            payments.PaymentBy = savings.Identifier;
            payments.AmountOfMoney = vm.Amount;
            payments.ProductsId = 2;

            if (savings.Amount < vm.Amount)
            {
                vm.HasError = true;
                vm.Message = "El monto que intentas pagar es mayor a la cantidad que tienes en la cuenta seleccionada";
                return vm;
            }

            if (creditCard.Owed >= vm.Amount)
            {
                creditCard.Owed = creditCard.Owed - vm.Amount;
                savings.Amount = savings.Amount - vm.Amount;
                await _creditCard.UpdateAsyn(creditCard,creditCard.Id);
                await _savingsAccountRepository.UpdateAsyn(savings, savings.Id);
                vm.Message = "Pago realizado correctamente!";
                await _payment.AddAsync(payments);


                return vm;
            }

            double operation = vm.Amount - creditCard.Owed;
            double creditPayment = vm.Amount - operation;
            creditCard.Owed = creditCard.Owed - creditPayment;
            savings.Amount = savings.Amount - creditPayment;

            await _creditCard.UpdateAsyn(creditCard, creditCard.Id);
            await _savingsAccountRepository.UpdateAsyn(savings, savings.Id);

            await _payment.AddAsync(payments);

            vm.Message = "Pago realizado correctamente!";
            return vm;
        }

        public async Task<SaveLoanPaymentViewModel> LoanPayment(SaveLoanPaymentViewModel vm)
        {

            Loan loans = await _loanRepository.GetViewModelById(vm.Loan);
            SavingsAccount savings = await _savingsAccountRepository.GetViewModelById(vm.FromAccount);
            Payments payments = new();
            payments.PaymentTo = loans.Identifier;
            payments.PaymentBy = savings.Identifier;
            payments.AmountOfMoney = vm.Amount;
            payments.ProductsId = 3;

            if (savings.Amount < vm.Amount)
            {
                vm.HasError = true;
                vm.Message = "El monto que intentas pagar es mayor a la cantidad que tienes en la cuenta seleccionada";
                return vm;
            }

            if (loans.Owed >= vm.Amount)
            {
                loans.Owed = loans.Owed - vm.Amount;
                savings.Amount = savings.Amount - vm.Amount;
                await _loanRepository.UpdateAsyn(loans, loans.Id);
                await _savingsAccountRepository.UpdateAsyn(savings, savings.Id);
                await _payment.AddAsync(payments);

                vm.Message = "Pago realizado correctamente!";
                return vm;
            }

            double operation = vm.Amount - loans.Owed;
            double creditPayment = vm.Amount - operation;
            loans.Owed = loans.Owed - creditPayment;
            savings.Amount = savings.Amount - creditPayment;

            await _loanRepository.UpdateAsyn(loans, loans.Id);
            await _savingsAccountRepository.UpdateAsyn(savings, savings.Id);

            await _payment.AddAsync(payments);

            vm.Message = "Pago realizado correctamente!";
            return vm;
        }


        public async Task<PaymentBResponse> BeneficiaryPayment(SaveBeneficiaryPaymentViewModel vm, int opt )
        {
            var savingsList = await _savingsAccountRepository.GetAllViewModel();
            var Blist = await _bRepository.GetAllViewModel();

            PaymentBResponse response = new();
            SavingsAccount beneficiaryAccount = savingsList.Where(acc => acc.Identifier == vm.BeneficiaryAcc).FirstOrDefault();
            SavingsAccount savings = await _savingsAccountRepository.GetViewModelById(vm.Account);
            Beneficiary beneficiary = Blist.Where(b => b.AccountId == vm.BeneficiaryAcc).FirstOrDefault();

            response.SenderAccount = vm.Account;
            response.DestinaryAccount = vm.BeneficiaryAcc;
            response.PaymentAmount = vm.Amount;
            response.FullName = beneficiary.FullName;

            Payments payments = new();
            payments.PaymentTo = beneficiaryAccount.Identifier;
            payments.PaymentBy = savings.Identifier;
            payments.AmountOfMoney = vm.Amount;
            payments.ProductsId = 1;


            if (opt == 0)
            {
                if (savings.Amount < vm.Amount)
                {
                    response.HasError = true;
                    response.Message = "El monto que intentas pagar es mayor a la cantidad que tienes en la cuenta seleccionada";
                    return response;
                }

                await _payment.AddAsync(payments);

                response.HasError = false;
                return response;
            }
          


             beneficiaryAccount.Amount = beneficiaryAccount.Amount +  vm.Amount;
             savings.Amount = savings.Amount - vm.Amount;
             await _savingsAccountRepository.UpdateAsyn(beneficiaryAccount, beneficiaryAccount.Id);
             await _savingsAccountRepository.UpdateAsyn(savings, savings.Id);

         

            await _payment.AddAsync(payments);

            response.Message = "Pago realizado correctamente!";
            response.HasError = false;
             return response;
            
          
        }


        public async Task<SaveCreditPaymentViewModel> PaymentInAdvance(SaveCreditPaymentViewModel vm)
        {

            CreditCard creditCard = await _creditCard.GetViewModelById(vm.CreditCard);
            SavingsAccount savings = await _savingsAccountRepository.GetViewModelById(vm.FromAccount);

            if (creditCard.Limit < vm.Amount)
            {
                vm.HasError = true;
                vm.Message = $"El monto supera el limite de tu tarjeta el cual es {creditCard.Limit} ingrese un monto inferior a su limite.";
                return vm;
            }

            if (creditCard.Limit - creditCard.Owed < vm.Amount )
            {
                vm.HasError = true;
                vm.Message = $"Tu limite es {creditCard.Limit} y debes {creditCard.Owed} la cantidad que intentas tranferir se pasa del limite de tu tarjeta";
                return vm;
            }
           

         
            double creditOwed = (vm.Amount * 6.25/(100));
            creditCard.Owed = creditCard.Owed + creditOwed + vm.Amount;
            savings.Amount = savings.Amount + vm.Amount;

            Transaction transaction = new();
            transaction.TransactionTo = savings.Identifier;
            transaction.TransactionBy = creditCard.Identifier;
            transaction.AmountOfMoney = vm.Amount;
            transaction.ProductsId = 2;
            await _transaction.AddAsync(transaction);

            await _creditCard.UpdateAsyn(creditCard, creditCard.Id);
            await _savingsAccountRepository.UpdateAsyn(savings, savings.Id);

            vm.Message = "Avance de efectivo completado correctamente";
            return vm;
        }



    }
}
