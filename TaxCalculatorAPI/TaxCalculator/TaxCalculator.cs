namespace TaxCalculatorAPI.TaxCalculator;

using TaxCalculatorAPI.Data;
using TaxCalculatorAPI.Data.Entities;

public interface ITaxCalculator
{
    double CalculateTax(double annualIncome);
}

public abstract class TaxCalculator : ITaxCalculator
{
    public abstract double CalculateTax(double annualIncome);
}

public class TaxCalculatorBase : TaxCalculator
{
    internal readonly SQLDatacontext _context;
    public TaxCalculatorBase(SQLDatacontext context)
    {
        _context = context;
    }
    public override double CalculateTax(double annualIncome)
    {
        return 0;
    }
}

public static class TaxCalculatorExceptionHandler
{
    public static void ThrowIfNull(TaxTable taxTableEntry)
    {
        if (taxTableEntry == null)
            throw new KeyNotFoundException();
    }
}

public class FlatRateTaxCalculator : TaxCalculatorBase
{
    public FlatRateTaxCalculator(SQLDatacontext context) : base(context) { }

    public override double CalculateTax(double annualIncome)
    {
        var rate = _context.FlatRateTaxTable.SingleOrDefault(s => s.Active);
        TaxCalculatorExceptionHandler.ThrowIfNull(rate);
        return annualIncome * rate.RateValue;
    }
}

public class FlatValueTaxCalculator : TaxCalculatorBase
{
    public FlatValueTaxCalculator(SQLDatacontext context) : base(context) { }
    public override double CalculateTax(double annualIncome)
    {
        double taxAmount = 0;
        var rate = _context.FlatValueTaxTable.SingleOrDefault(s => s.Active
            && annualIncome >= s.MinimumValue
            && annualIncome <= s.MaximumValue);

        TaxCalculatorExceptionHandler.ThrowIfNull(rate);

        if (rate.RateTypeId == 1)
        {
            taxAmount = rate.RateValue;
        }
        else if (rate.RateTypeId == 2)
        {
            taxAmount = annualIncome * rate.RateValue;
        }

        return taxAmount;
    }
}

public class ProgressiveTaxCalculator : TaxCalculatorBase
{
    public ProgressiveTaxCalculator(SQLDatacontext context) : base(context) { }
    public override double CalculateTax(double annualIncome)
    {
        var rates = _context.ProgressiveTaxTable.Where(s => s.Active).OrderBy(s => s.MaximumValue).ToList();
        var taxAlgo = new ProgressiveTaxAlgo();
        return taxAlgo.GetTax(rates, 0, annualIncome, 0);
    }

    internal class ProgressiveTaxAlgo
    {
        private double AnnualIncome;
        internal double GetTax(List<ProgressiveTaxTable> rates, int currentTier, double income, double startingTax)
        {
            if (AnnualIncome == 0)
                AnnualIncome = income;

            var taxTier = new TaxTier
            {
                MaxValue = rates[currentTier].MaximumValue,
                TaxableValue = currentTier == 0 ?
                            rates[currentTier].MaximumValue :
                            rates[currentTier].MaximumValue - rates[currentTier - 1].MaximumValue,

                TaxPercentage = rates[currentTier].RateValue
            };

            if (AnnualIncome > taxTier.MaxValue)
            {
                startingTax += taxTier.GetMaxTax();
                income -= taxTier.TaxableValue;
                currentTier++;
                startingTax = GetTax(rates, currentTier, income, startingTax);
                return startingTax;
            }

            return startingTax += income * taxTier.TaxPercentage;
        }
    }

    internal class TaxTier
    {
        internal double MaxValue { get; set; }
        internal double TaxableValue { get; set; }
        internal double TaxPercentage { get; set; }
        internal double GetMaxTax()
        {
            return TaxableValue * TaxPercentage;
        }
    }
}


