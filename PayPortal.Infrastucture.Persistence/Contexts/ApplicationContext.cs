using PayPortal.Core.Domain.Common;
using PayPortal.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace PayPortal.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        #region Recibo las entidades
        DbSet<Products> Products { get; set; }
        DbSet<SavingsAccount> SavingsAccounts { get; set; }

        DbSet<CreditCard> CreditCards { get; set; }

        DbSet<Loan> Loans { get; set; }

        DbSet<Beneficiary> Beneficiaries { get; set; }

        DbSet<Transaction> Transactions { get; set; }

        DbSet<Payments> Payments { get; set; }
        #endregion

        #region Configuro el auditableBaseEntity para identificar que usario y cuando realiza una modificacion
        public override Task<int> SaveChangesAsync(CancellationToken cancellation = new())
        {
            foreach (var item in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.CreatedBy = "Default";
                        item.Entity.CreatedDate = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        item.Entity.LastModifiedBy = "Default";
                        item.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellation);
        }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region  "Tables"

            modelBuilder.Entity<Products>().ToTable("Products");
            modelBuilder.Entity<SavingsAccount>().ToTable("SavingsAccounts");
            modelBuilder.Entity<Loan>().ToTable("Loans");
            modelBuilder.Entity<CreditCard>().ToTable("CreditCards");
            modelBuilder.Entity<Beneficiary>().ToTable("Beneficiaries");
            modelBuilder.Entity<Payments>().ToTable("Payments");
            modelBuilder.Entity<Transaction>().ToTable("Transactions");




            #endregion
          
            #region  "Primary Key"

            modelBuilder.Entity<Products>().HasKey(p => p.Id);
            modelBuilder.Entity<SavingsAccount>().HasKey(s => s.Id);
            modelBuilder.Entity<Loan>().HasKey(l => l.Id);
            modelBuilder.Entity<CreditCard>().HasKey(c => c.Id);
            modelBuilder.Entity<Beneficiary>().HasKey(b => b.Id);
            modelBuilder.Entity<Payments>().HasKey(p => p.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);





            #endregion

            #region  "Relations"

            #region Productos con Cuentas de Ahorros
            modelBuilder.Entity<Products>()
              .HasMany<SavingsAccount>(p => p.SavingsAccount)
              .WithOne(s => s.Products)
              .HasForeignKey(s => s.ProductsId)
              .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Productos con deudas
            modelBuilder.Entity<Products>()
              .HasMany<Loan>(p => p.Loans)
              .WithOne(l => l.Products)
              .HasForeignKey(l => l.ProductsId)
              .OnDelete(DeleteBehavior.Cascade);
            #endregion
            
            #region Producto con Tarjeta de Creditos
            modelBuilder.Entity<Products>()
              .HasMany<CreditCard>(p => p.CreditCards)
              .WithOne(c => c.Products)
              .HasForeignKey(c => c.ProductsId)
              .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Productos con Pagos
            modelBuilder.Entity<Products>()
              .HasMany<Payments>(p => p.Payments)
              .WithOne(p => p.Products)
              .HasForeignKey(c => c.ProductsId)
              .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Productos con trasacciones
            modelBuilder.Entity<Products>()
              .HasMany<Transaction>(p => p.Transactions)
              .WithOne(t => t.Products)
              .HasForeignKey(t => t.ProductsId)
              .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #endregion

            #region  "Properties Configuration"

            modelBuilder.Entity<SavingsAccount>().Property(s => s.Identifier).IsRequired().HasMaxLength(9); ;
            modelBuilder.Entity<SavingsAccount>().Property(s => s.Amount).IsRequired();
            modelBuilder.Entity<SavingsAccount>().Property(s => s.IsMain).IsRequired();
            modelBuilder.Entity<SavingsAccount>().Property(s => s.OwnerName).IsRequired();

            modelBuilder.Entity<Products>().Property(p => p.Name).IsRequired();

            modelBuilder.Entity<Loan>().Property(p => p.Owed).IsRequired();
            modelBuilder.Entity<Loan>().Property(p => p.Identifier).IsRequired().HasMaxLength(9);
            modelBuilder.Entity<Loan>().Property(p => p.Amount).IsRequired();
            modelBuilder.Entity<Loan>().Property(p => p.OwnerName).IsRequired();

            modelBuilder.Entity<CreditCard>().Property(p => p.Limit).IsRequired();
            modelBuilder.Entity<CreditCard>().Property(p => p.Identifier).IsRequired().HasMaxLength(9);
            modelBuilder.Entity<CreditCard>().Property(p => p.OwnerName).IsRequired();


            modelBuilder.Entity<Beneficiary>().Property(b => b.UserId).IsRequired();
            modelBuilder.Entity<Beneficiary>().Property(b => b.FullName).IsRequired();
            modelBuilder.Entity<Beneficiary>().Property(b => b.AccountId).IsRequired();



            modelBuilder.Entity<Transaction>().Property(b => b.TransactionTo).IsRequired();
            modelBuilder.Entity<Transaction>().Property(b => b.TransactionBy).IsRequired();


            modelBuilder.Entity<Payments>().Property(b => b.PaymentTo).IsRequired();
            modelBuilder.Entity<Payments>().Property(b => b.PaymentBy).IsRequired();



            #endregion
        }

    }
}
