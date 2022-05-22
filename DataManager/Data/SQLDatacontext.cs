using DataManager.Models.Authorization;
using DataManager.Models.TaxCalculator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataManager.Data
{
    public class SQLDatacontext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public SQLDatacontext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<ApiUser> ApiUsers { get; set; }

        //TaxCalculatorTables
        public DbSet<TaxSubmission> TaxSubmission { get; set; }
        public DbSet<TaxType> TaxType { get; set; }
        public DbSet<PostalCodeTaxMap> PostalCodeTaxMap { get; set; }
        public DbSet<RateType> RateTypes { get; set; }
        public DbSet<ProgressiveTaxTable> ProgressiveTaxTable { get; set; }
        public DbSet<FlatValueTaxTable> FlatValueTaxTable { get; set; }
        public DbSet<FlatRateTaxTable> FlatRateTaxTable { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TaxCalculatorDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserSeeder.Seed(modelBuilder);
            TaxTableSeeder.Seed(modelBuilder);
        }
    }
}