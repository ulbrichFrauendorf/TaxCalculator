using System.ComponentModel.DataAnnotations;

namespace TaxCalculatorFrontend.TaxCalculator.Models;

public class EditTaxModel
{
    public int Id { get; set; }
    [Required]
    public string PostalCode { get; set; }
    [Required]
    public double AnnualIncome { get; set; }
}