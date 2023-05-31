using Debts.Domain;
using Debts.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Debts.Infrastructure.Persistence
{
    public class DebtsContext : DbContext
    {
        /// <summary>
        /// Instructions to EF Core
        /// To create a migration: add-migration MyFirstMigration
        /// To update the database: update-database
        /// To revert a migration: remove-migration
        /// To update to specific database: update-Database 20230228182523_MyFirstMigration        
        /// To create script Script-Migration -From 0 -To 20230307191133_FirstMigration -Context IdentityServerDbContext
        /// </summary>

        public DebtsContext()
        {

        }

        public DebtsContext(DbContextOptions<DebtsContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: Hay que tener especialmente cuidado con esta sección. Para comenzar, las migraciones toman esta cadena, por lo que puedo realizar una migración a BD directamente a producción y dañar los datos.
            // Por otro lado, puede dejar de funcionar la API Gateway apuntando hacia aquí dándome error 500 sin información por apuntar a otra BD (en PRO apuntando a local, error)...

#if DEBUG
            optionsBuilder.UseMySQL("server=localhost;database=debts;user=root;password=123456");
#else
            optionsBuilder.UseMySQL("Server=finance.ca7udgodjtbv.eu-west-3.rds.amazonaws.com;Database=debts;User Id=admin;Password=kGj8hVF8B9cauN6F73NdbBdszjTbf");
#endif
        }

        public DbSet<User> Users { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "System";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "System";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = Guid.Parse("c5ce74fb-0e50-4c9f-9ed0-03ddcd4159e2")
                }
            );
        }
    }
}
