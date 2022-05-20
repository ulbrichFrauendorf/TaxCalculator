using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using TaxCalculatorAPI.Data;
using TaxCalculatorAPI.TaxCalculator;

namespace TaxCalculatorTests;

public class TaxCalculatorFactoryTest
{
    private TaxCalculatorFactory service;
    [SetUp]
    public void Setup()
    {
        var mockConfig = new Mock<IConfiguration>();
        var context = new SqliteDataContext(mockConfig.Object);
        service = new TaxCalculatorFactory(context);
    }

    [Test]
    public void Get_FlatRateTaxCalculator_Test()
    {
        var obj = service.GetTaxCalculator("Flat Rate");
        Assert.IsAssignableFrom(typeof(FlatRateTaxCalculator), obj);
    }

    [Test]
    public void Get_FlatValueTaxCalculator_Test()
    {
        var obj = service.GetTaxCalculator("Flat Value");
        Assert.IsAssignableFrom(typeof(FlatValueTaxCalculator), obj);
    }

    [Test]
    public void Get_ProgressiveTaxCalculator_Test()
    {
        var obj = service.GetTaxCalculator("Progressive");
        Assert.IsAssignableFrom(typeof(ProgressiveTaxCalculator), obj);
    }

    [Test]
    public void Get_UnknowsTaxCalculator_Test()
    {
        Assert.Throws<NotImplementedException>(() => service.GetTaxCalculator("Random Type"));
    }
}
