using DataManager.Data;
using DataManager.Models.TaxCalculator;

namespace DataServices.TaxCalculator
{
    public interface ITaxSubmissionBuilder
    {
        TaxSubmission Build(string postalcode, double annualIncome);
    }

    public class TaxSubmissionBuilder : ITaxSubmissionBuilder
    {
        private SQLDatacontext _context;
        private readonly ITaxCalculatorFactory _taxCalculatorFactory;
        public TaxSubmissionBuilder(SQLDatacontext context, ITaxCalculatorFactory taxCalculatorFactory)
        {
            _context = context;
            _taxCalculatorFactory = taxCalculatorFactory;
        }

        public TaxSubmission Build(string postalcode, double annualIncome)
        {
            var taxSubmission = new TaxSubmission
            {
                AnnualIncome = annualIncome
            };

            var postmap = _context.PostalCodeTaxMap
                .SingleOrDefault(s => s.PostalCode == postalcode);

            var taxType = _context.TaxType.SingleOrDefault(s => s.TaxTypeId == postmap.TaxTypeId);
            if (postmap == null)
                throw new KeyNotFoundException("Postal code does not exist in database");

            taxSubmission.PostalCodeTaxMap = postmap;
            taxSubmission.SubmissionDate = DateTime.Now;
            var taxCalculator = _taxCalculatorFactory.GetTaxCalculator(taxType.TaxTypeDescription);
            taxSubmission.CalculatedTax = taxCalculator.CalculateTax(annualIncome);
            return taxSubmission;
        }
    }
}