using Microsoft.EntityFrameworkCore;
using TaxCalculatorAPI.Data.Entities;

namespace TaxCalculatorAPI.Data
{
    public class SQLDatacontext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public SQLDatacontext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("TaxCalculatorDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserSeeder.Seed(modelBuilder);
            SelectorTableSeeder.Seed(modelBuilder);
            TaxTableSeeder.Seed(modelBuilder);
        }

        //UserTables
        public DbSet<ApiUser> ApiUsers { get; set; }

        //TaxCalculatorTables
        public DbSet<TaxSubmission> TaxSubmission { get; set; }
        public DbSet<TaxType> TaxType { get; set; }
        public DbSet<PostalCodeTaxMap> PostalCodeTaxMap { get; set; }
        public DbSet<RateType> RateTypes { get; set; }
        public DbSet<ProgressiveTaxTable> ProgressiveTaxTable { get; set; }
        public DbSet<FlatValueTaxTable> FlatValueTaxTable { get; set; }
        public DbSet<FlatRateTaxTable> FlatRateTaxTable { get; set; }

    }

    internal static class UserSeeder
    {
        internal static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiUser>().HasData(
                        new ApiUser
                        {
                            ApiUserId = 1,
                            Email = "admin@testsite.com",
                            Password = BCrypt.Net.BCrypt.HashPassword("P@ssw0rd")
                        });
        }
    }

    internal static class TaxTableSeeder
    {
        internal static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RateType>().HasData(
                    new RateType
                    {
                        RateTypeId = 1,
                        RateTypeDescription = "Amount",

                    },
                    new RateType
                    {
                        RateTypeId = 2,
                        RateTypeDescription = "Percentage",

                    });

            modelBuilder.Entity<FlatRateTaxTable>().HasData(
                new FlatRateTaxTable
                {
                    Id = 1,
                    RateTypeId = 2,
                    Active = true,
                    RateValue = 0.175
                });

            modelBuilder.Entity<FlatValueTaxTable>().HasData(
                new FlatValueTaxTable
                {
                    Id = 1,
                    RateTypeId = 2,
                    Active = true,
                    RateValue = 0.05,
                    MinimumValue = 0,
                    MaximumValue = 199000
                },
                 new FlatValueTaxTable
                 {
                     Id = 2,
                     RateTypeId = 1,
                     Active = true,
                     RateValue = 10000,
                     MinimumValue = 200000.00,
                     MaximumValue = Convert.ToDouble(int.MaxValue),
                 });

            modelBuilder.Entity<ProgressiveTaxTable>().HasData(
                new ProgressiveTaxTable
                {
                    Id = 1,
                    RateTypeId = 2,
                    Active = true,
                    RateValue = 0.1,
                    MinimumValue = 0,
                    MaximumValue = 8350,
                },
                new ProgressiveTaxTable
                {
                    Id = 2,
                    RateTypeId = 2,
                    Active = true,
                    RateValue = 0.15,
                    MinimumValue = 8351,
                    MaximumValue = 33950,
                },
                new ProgressiveTaxTable
                {
                    Id = 3,
                    RateTypeId = 2,
                    Active = true,
                    RateValue = 0.25,
                    MinimumValue = 33951,
                    MaximumValue = 82250,
                },
                new ProgressiveTaxTable
                {
                    Id = 4,
                    RateTypeId = 2,
                    Active = true,
                    RateValue = 0.28,
                    MinimumValue = 82251,
                    MaximumValue = 171550,
                },
                new ProgressiveTaxTable
                {
                    Id = 5,
                    RateTypeId = 2,
                    Active = true,
                    RateValue = 0.33,
                    MinimumValue = 171551,
                    MaximumValue = 372950,
                },
                new ProgressiveTaxTable
                {
                    Id = 6,
                    RateTypeId = 2,
                    Active = true,
                    RateValue = 0.35,
                    MinimumValue = 372951,
                    MaximumValue = Convert.ToDouble(int.MaxValue),
                }
                );
        }
    }

    internal static class SelectorTableSeeder
    {
        internal static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxType>().HasData(
                        new TaxType
                        {
                            TaxTypeId = 1,
                            TaxTypeDescription = "Progressive",

                        },
                        new TaxType
                        {
                            TaxTypeId = 2,
                            TaxTypeDescription = "Flat Value",
                        },
                        new TaxType
                        {
                            TaxTypeId = 3,
                            TaxTypeDescription = "Flat Rate",
                        });
            modelBuilder.Entity<PostalCodeTaxMap>().HasData(
                new PostalCodeTaxMap
                {
                    Id = 1,
                    PostalCode = "7441",
                    TaxTypeId = 1
                },
                new PostalCodeTaxMap
                {
                    Id = 2,
                    PostalCode = "A100",
                    TaxTypeId = 2
                },
                new PostalCodeTaxMap
                {
                    Id = 3,
                    PostalCode = "7000",
                    TaxTypeId = 3
                },
                new PostalCodeTaxMap
                {
                    Id = 4,
                    PostalCode = "1000",
                    TaxTypeId = 1
                }
                );
        }
    }
}