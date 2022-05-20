using TaxCalculatorAPI.Data;

namespace TaxCalculatorAPI.TaxCalculator;

public interface ITaxCalculatorFactory
{
    ITaxCalculator GetTaxCalculator(string taxType);
}

public class TaxCalculatorFactory : ITaxCalculatorFactory
{
    internal readonly SQLDatacontext _context;
    public TaxCalculatorFactory(SQLDatacontext context)
    {
        _context = context;
    }

    public ITaxCalculator GetTaxCalculator(string taxType)
    {
        return taxType switch
        {
            "Flat Rate" => new FlatRateTaxCalculator(_context),
            "Flat Value" => new FlatValueTaxCalculator(_context),
            "Progressive" => new ProgressiveTaxCalculator(_context),
            _ => throw new NotImplementedException($"Tax Calculator does not exist for : {taxType}")
        };
    }
}
