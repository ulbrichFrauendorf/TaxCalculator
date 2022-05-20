using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using TaxCalculatorAPI.Data;
using TaxCalculatorAPI.TaxCalculator;

namespace TaxCalculatorTests
{
    public class FlatRateTaxCalculatorTest
    {
        private FlatRateTaxCalculator service;
        [SetUp]
        public void Setup()
        {
            var mockConfig = new Mock<IConfiguration>();
            var context = new SqliteDataContext(mockConfig.Object);
            service = new FlatRateTaxCalculator(context);
        }

        [Test]
        public void Get_TaxAmount_FlatRateTaxCalculator_Percentage_Test()
        {
            var taxAmount = service.CalculateTax(200000);
            Assert.AreEqual(taxAmount, 35000);
        }
    }

    public class FlatValueTaxCalculatorTest
    {
        private FlatValueTaxCalculator service;
        [SetUp]
        public void Setup()
        {
            var mockConfig = new Mock<IConfiguration>();
            var context = new SqliteDataContext(mockConfig.Object);
            service = new FlatValueTaxCalculator(context);
        }

        [Test]
        public void Get_TaxAmount_FlatValueTaxCalculator_Amount_Test()
        {
            var taxAmount = service.CalculateTax(100000);
            Assert.AreEqual(taxAmount, 5000);
        }
        [Test]
        public void Get_TaxAmount_FlatValueTaxCalculator_Percentage_Test()
        {
            var taxAmount = service.CalculateTax(62300000);
            Assert.AreEqual(taxAmount, 10000);
        }
    }

    public class ProgressiveTaxCalculatorTest
    {
        private ProgressiveTaxCalculator service;
        private SqliteDataContext context;
        [SetUp]
        public void Setup()
        {
            var mockConfig = new Mock<IConfiguration>();
            context = new SqliteDataContext(mockConfig.Object);
            service = new ProgressiveTaxCalculator(context);
        }

        [Test]
        public void Get_TaxAmount_ProgressiveTaxCalculator_Amount_Test_5000()
        {
            const double annualIncome = 5000;
            var checkTaxAmount = ReverseCalculator.GetTaxAmount(annualIncome, context);
            var taxAmount = service.CalculateTax(annualIncome);
            Assert.AreEqual(taxAmount, checkTaxAmount);
        }

        [Test]
        public void Manual_Get_TaxAmount_ProgressiveTaxCalculator_Amount_Test_10000()
        {
            var taxAmount = service.CalculateTax(10000);
            Assert.AreEqual(taxAmount, 1082.5);
        }

        [Test]
        public void Manual_Get_TaxAmount_ProgressiveTaxCalculator_Amount_Test_25000()
        {
            var taxAmount = service.CalculateTax(25000);
            Assert.AreEqual(taxAmount, 3332.5);
        }

        [Test]
        public void Manual_Get_TaxAmount_ProgressiveTaxCalculator_Amount_Test_55000()
        {
            var taxAmount = service.CalculateTax(55000);
            Assert.AreEqual(taxAmount, 9937.50);
        }

        [Test]
        public void Manual_Get_TaxAmount_ProgressiveTaxCalculator_Amount_Test_125000()
        {
            var taxAmount = service.CalculateTax(125000);
            Assert.AreEqual(taxAmount, 28720);
        }

        [Test]
        public void Get_TaxAmount_ProgressiveTaxCalculator_Amount_Test_125000()
        {
            const double annualIncome = 125000;
            var checkTaxAmount = ReverseCalculator.GetTaxAmount(annualIncome, context);
            var taxAmount = service.CalculateTax(annualIncome);
            Assert.AreEqual(taxAmount, checkTaxAmount);
        }

        [Test]
        public void Get_TaxAmount_ProgressiveTaxCalculator_Amount_Test_55000()
        {
            const double annualIncome = 55000;
            var checkTaxAmount = ReverseCalculator.GetTaxAmount(annualIncome, context);
            var taxAmount = service.CalculateTax(annualIncome);
            Assert.AreEqual(taxAmount, checkTaxAmount);
        }

        [Test]
        public void Get_TaxAmount_ProgressiveTaxCalculator_Amount_Test_425000()
        {
            const double annualIncome = 425000;
            var checkTaxAmount = ReverseCalculator.GetTaxAmount(annualIncome, context);
            var taxAmount = service.CalculateTax(annualIncome);
            Assert.AreEqual(taxAmount, checkTaxAmount);
        }

        [Test]
        public void Get_TaxAmount_ProgressiveTaxCalculator_Amount_Test_0()
        {
            var taxAmount = service.CalculateTax(0);
            Assert.AreEqual(taxAmount, 0);
        }

        [Test]
        public void Get_TaxAmount_ProgressiveTaxCalculator_Amount_Test_Max()
        {
            var annualIncome = Convert.ToDouble(int.MaxValue);
            var checkTaxAmount = ReverseCalculator.GetTaxAmount(annualIncome, context);
            var taxAmount = service.CalculateTax(annualIncome);
            Assert.AreEqual(taxAmount, checkTaxAmount);
        }

        internal static class ReverseCalculator
        {
            internal static double GetTaxAmount(double annualIncome, SqliteDataContext context)
            {
                var rates = context.ProgressiveTaxTable.Where(s => s.Active).OrderBy(s => s.MaximumValue).ToList();
                double taxAmount = 0;
                var tempIncome = annualIncome;
                while (tempIncome > 0)
                {
                    var tierSelection = rates.SingleOrDefault(s => tempIncome >= s.MinimumValue && tempIncome <= s.MaximumValue);

                    var checkAmount = tempIncome - tierSelection.MinimumValue;

                    // Plus one for the previous tier except bottom tier as no previous tier exists
                    checkAmount += (tierSelection.MinimumValue == 0) ? 0 : 1;

                    if (tierSelection.RateTypeId == 1)
                    {
                        taxAmount = tierSelection.RateValue;
                    }
                    else if (tierSelection.RateTypeId == 2)
                    {
                        taxAmount += checkAmount * tierSelection.RateValue;
                    }

                    tempIncome -= checkAmount;
                }

                return taxAmount;
            }
        }
    }


}
