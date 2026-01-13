using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Helper;
using MercanciaSegura.RestAPI;

namespace MercanciaSegura.UnitTest.FixtureBase
{
    [Collection("FunctionalCollection")]
    public class DatabaseTestFixture : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        protected readonly CustomWebApplicationFactory<Program> Factory;

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DatabaseTestFixture()
        {
            
            // Build configuration with User Secrets
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<DatabaseTestFixture>()
                .AddEnvironmentVariables()
                .AddInMemoryCollection(initialData: new Dictionary<string, string?>
                {
                    { "Jwt:Key", "SuperSecretKeyForTestingPurposesOnly123!" },
                    { "Jwt:Issuer", "WalletService" },
                    { "Jwt:Audience", "WalletServiceUser" }
                });
            _configuration = builder.Build();

            _connectionString = DbConnectionHelper.GetConnectionString(configuration: _configuration);
            
            SetEnvironmentalVariables();
            var factory = new CustomWebApplicationFactory<Program>();
            Factory = factory;
         
            using var context = CreateContext();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        protected async Task SetupDataAsync(Func<ServiceDbContext, Task> setupDataAction)
        {
            await using var context = CreateContext();
            await setupDataAction(context);
        }

        protected internal ServiceDbContext CreateContext()
            => new(
                new DbContextOptionsBuilder<ServiceDbContext>()
                    .UseSqlServer(_connectionString,
                        optionsBuilder => optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                    .Options);
        
        private void SetEnvironmentalVariables()
        {
            Environment.SetEnvironmentVariable(
                "dbConnectionString",
                _connectionString);

            
     
        }
    }
}