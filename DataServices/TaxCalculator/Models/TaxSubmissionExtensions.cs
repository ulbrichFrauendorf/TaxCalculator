using DataManager.Data;
using DataManager.Models.TaxCalculator;

namespace DataServices.TaxCalculator.Models
{
    public static class TaxSubmissionExtensions
    {
        public static TaxSubmissionResponse ToResponse(this TaxSubmission taxSubmission)
        {
            return new TaxSubmissionResponse
            {
                Id = taxSubmission.Id,
                AnnualIncome = taxSubmission.AnnualIncome,
                SubmissionDate = taxSubmission.SubmissionDate,
                PostalCode = taxSubmission.PostalCodeTaxMap.PostalCode,
                CalculatedTax = taxSubmission.CalculatedTax,
            };
        }

        public static TaxSubmission FromResponse(this TaxSubmissionResponse taxSubmissionResponse, SQLDatacontext context)
        {
            return new TaxSubmission
            {
                Id = taxSubmissionResponse.Id,
                AnnualIncome = taxSubmissionResponse.AnnualIncome,
                SubmissionDate = taxSubmissionResponse.SubmissionDate,
                PostalCodeTaxMap = context.PostalCodeTaxMap.SingleOrDefault(s => s.PostalCode == taxSubmissionResponse.PostalCode),
                CalculatedTax = taxSubmissionResponse.CalculatedTax,
            };
        }
    }

}
