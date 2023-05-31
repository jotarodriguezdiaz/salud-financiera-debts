using Debts.Application.Contracts.Persistence;
using Debts.Infrastructure.Persistence;
using Debts.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.EntityFrameworkCore.Extensions;

namespace Debts.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {   
            var stringConnection = configuration.GetConnectionString("ConnectionString") ?? string.Empty;            

            services.AddDbContext<DebtsContext>(options => options.UseMySQL(stringConnection));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));          

            return services;
        }

        public class MysqlEntityFrameworkDesignTimeServices : IDesignTimeServices
        {
            public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
            {
                serviceCollection.AddEntityFrameworkMySQL();
                new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection)
                    .TryAddCoreServices();
            }
        }
    }
}