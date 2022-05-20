namespace TaxCalculatorAPI.Data.Entities
{
    //Percentage / Monetary
    public class RateType
    {
        public int RateTypeId { get; set; }
        public string RateTypeDescription { get; set; }
    }

    public interface ITaxTable
    {
        public int Id { get; set; }

        public double RateValue { get; set; }
        public bool Active { get; set; }
        public int RateTypeId { get; set; }
    }

    public abstract class TaxTable : ITaxTable
    {
        public int Id { get; set; }
        public double RateValue { get; set; }
        public bool Active { get; set; }
        public int RateTypeId { get; set; }
        public virtual RateType RateType { get; set; }
    }

    public class ProgressiveTaxTable : TaxTable
    {
        public double MinimumValue { get; set; }
        public double MaximumValue { get; set; }
    }

    public class FlatValueTaxTable : TaxTable
    {
        public double MinimumValue { get; set; }
        public double MaximumValue { get; set; }
    }

    public class FlatRateTaxTable : TaxTable
    {

    }
}
