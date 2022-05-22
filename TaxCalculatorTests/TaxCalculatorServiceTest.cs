using DataManager.Data;
using DataServices.TaxCalculator;
using DataServices.TaxCalculator.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace TaxCalculatorTests
{
    public class TaxCalculatorServiceTest
    {
        private TaxCalculatorService service;
        [SetUp]
        public void Setup()
        {
            var mockConfig = new Mock<IConfiguration>();
            var context = new SqliteDataContext(mockConfig.Object);
            var mockTaxCalculatorFactory = new Mock<TaxCalculatorFactory>(context);
            var mockTaxSubmissionBuilder = new Mock<TaxSubmissionBuilder>(context, mockTaxCalculatorFactory.Object);
            service = new TaxCalculatorService(context, mockTaxSubmissionBuilder.Object);
        }

        [Test]
        public void Add_TaxSubmission_All_Test()
        {
            var obj = service.PostTaxSubmission(new TaxSubmissionSubmit { AnnualIncome = 123456, PostalCode = "7000" });
            Assert.AreEqual(obj?.AnnualIncome, 123456);
        }

        [Test]
        public void Get_TaxSubmission_All_Test()
        {
            var obj = service.GetTop10();
            Assert.GreaterOrEqual(obj?.ToList().Count, 1);
        }

        [Test]
        public void Get_TaxSubmission_By_Id_Test()
        {
            var obj = service.GetById(1);
            Assert.AreEqual(obj?.PostalCode, "7000");
        }

        [Test]
        public void Update_TaxSubmission_By_Id_Test()
        {
            var obj = service.UpdateTaxSubmission(new TaxSubmissionUpdate { Id = 1, AnnualIncome = 123456, PostalCode = "7000" });
            var checkObj = service.GetById(1);
            Assert.AreEqual(obj?.AnnualIncome, 123456);
        }
        [Test]
        public void Post_TaxSubmission_Test()
        {
            Assert.DoesNotThrow(() => service.PostTaxSubmission(TaxSubmitTestData.GenericTestData));
        }

        [Test]
        public void Delete_TaxSubmission_Test()
        {
            //Cannot delete due toe Referential constraint All History to be kept
            Assert.Throws<InvalidOperationException>(() => service.DeleteTaxSubmission(2));

        }
        [Test]
        public void Get_TaxSubmission_FlatRate_Test_100000()
        {
            var obj = service.PostTaxSubmission(TaxSubmitTestData.FlatRateTestData);
            Assert.AreEqual(obj?.CalculatedTax, 17500);
        }

        [Test]
        public void Get_TaxSubmission_FlatValue_Test_100000()
        {
            var obj = service.PostTaxSubmission(TaxSubmitTestData.FlatValueTestData);
            Assert.AreEqual(obj?.CalculatedTax, 5000);
        }

        [Test]
        public void Get_TaxSubmission_Progressive_1_Test_125000()
        {
            var obj = service.PostTaxSubmission(TaxSubmitTestData.ProgressiveTestData_1);
            Assert.AreEqual(obj?.CalculatedTax, 28720);
        }

        [Test]
        public void Get_TaxSubmission_Progressive_2_Test_125000()
        {
            var obj = service.PostTaxSubmission(TaxSubmitTestData.ProgressiveTestData_2);
            Assert.AreEqual(obj?.CalculatedTax, 28720);
        }
    }

    internal static class TaxSubmitTestData
    {
        internal static TaxSubmissionSubmit GenericTestData => new TaxSubmissionSubmit { PostalCode = "7000", AnnualIncome = 100000 };
        internal static TaxSubmissionSubmit FlatRateTestData => new TaxSubmissionSubmit { PostalCode = "7000", AnnualIncome = 100000 };
        internal static TaxSubmissionSubmit FlatValueTestData => new TaxSubmissionSubmit { PostalCode = "A100", AnnualIncome = 100000 };
        internal static TaxSubmissionSubmit ProgressiveTestData_1 => new TaxSubmissionSubmit { PostalCode = "7441", AnnualIncome = 125000 };
        internal static TaxSubmissionSubmit ProgressiveTestData_2 => new TaxSubmissionSubmit { PostalCode = "1000", AnnualIncome = 125000 };
    }
}
