using System.ComponentModel.DataAnnotations;

namespace DataManager.Models.TaxCalculator
{
    public class PostalCodeTaxMap
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }

        public int TaxTypeId { get; set; }
        public virtual TaxType TaxType { get; set; }
        public virtual IEnumerable<TaxSubmission> TaxSubmissions { get; set; }
    }


    public class TaxSubmission
    {
        public int Id { get; set; }
        [Required]
        public double AnnualIncome { get; set; }
        [Required]
        public int PostalCodeTaxMapId { get; set; }
        public virtual PostalCodeTaxMap PostalCodeTaxMap { get; set; }

        public DateTime SubmissionDate { get; set; }
        public double CalculatedTax { get; set; }

    }

    public class TaxType
    {
        public int TaxTypeId { get; set; }
        [Required]
        public string TaxTypeDescription { get; set; }

        public virtual IEnumerable<PostalCodeTaxMap> PostalCodeTaxMaps { get; set; }
    }

}
