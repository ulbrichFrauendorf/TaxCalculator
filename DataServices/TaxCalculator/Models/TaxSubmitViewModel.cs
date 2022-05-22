namespace DataServices.TaxCalculator.Models
{
    public class TaxSubmissionSubmit
    {
        public string PostalCode { get; set; }
        public double AnnualIncome { get; set; }
    }

    public class TaxSubmissionUpdate
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public double AnnualIncome { get; set; }
    }

    public class TaxSubmissionResponse
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public double AnnualIncome { get; set; }
        public DateTime SubmissionDate { get; set; }
        public double CalculatedTax { get; set; }
    }
}
