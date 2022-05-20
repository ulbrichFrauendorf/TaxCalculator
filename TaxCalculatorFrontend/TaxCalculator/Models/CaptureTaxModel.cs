using System.ComponentModel.DataAnnotations;

namespace TaxCalculatorFrontend.TaxCalculator.Models
{
    public class CaptureTaxModel
    {
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public double AnnualIncome { get; set; }
    }
}
