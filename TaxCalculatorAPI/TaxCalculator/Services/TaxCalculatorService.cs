using Microsoft.EntityFrameworkCore;
using TaxCalculatorAPI.Data;
using TaxCalculatorAPI.Data.Entities;
using TaxCalculatorAPI.TaxCalculator.ViewModels;

namespace TaxCalculatorAPI.TaxCalculator.Services;

public interface ITaxCalculatorService
{
    IEnumerable<TaxSubmissionResponse> GetTop10();
    TaxSubmissionResponse GetById(int id);
    TaxSubmissionResponse PostTaxSubmission(TaxSubmissionSubmit model);
    TaxSubmissionResponse UpdateTaxSubmission(TaxSubmissionUpdate model);
    void DeleteTaxSubmission(int id);

}

public class TaxCalculatorService : ITaxCalculatorService
{
    private SQLDatacontext _context;
    private readonly ITaxSubmissionBuilder _taxSubmissionBuilder;
    public TaxCalculatorService(SQLDatacontext context, ITaxSubmissionBuilder taxSubmissionBuilder)
    {
        _context = context;
        _taxSubmissionBuilder = taxSubmissionBuilder;
    }

    public IEnumerable<TaxSubmissionResponse> GetTop10()
    {
        var retList = new List<TaxSubmissionResponse>();
        var response = _context.TaxSubmission
            .Include(s => s.PostalCodeTaxMap)
            .OrderByDescending(s => s.SubmissionDate)
            .Take(10)
            .ToList();
        foreach (var item in response)
        {
            retList.Add(item.ToResponse());
        }

        return retList;
    }

    public TaxSubmissionResponse GetById(int id)
    {
        var taxSubmission = _context.TaxSubmission
            .Include(s => s.PostalCodeTaxMap).FirstOrDefault(x => x.Id == id);

        if (taxSubmission == null)
            throw new KeyNotFoundException("Tax calculation record not found");

        return taxSubmission.ToResponse();
    }

    public TaxSubmissionResponse PostTaxSubmission(TaxSubmissionSubmit model)
    {
        var taxSubmission = _taxSubmissionBuilder.Build(model.PostalCode, model.AnnualIncome);
        _context.Add(taxSubmission);
        _context.SaveChanges();
        return taxSubmission.ToResponse();
    }

    public TaxSubmissionResponse UpdateTaxSubmission(TaxSubmissionUpdate model)
    {
        if (model.Id == 0)
            throw new KeyNotFoundException("Invalid Id.");
        var taxSubmission = _taxSubmissionBuilder.Build(model.PostalCode, model.AnnualIncome);
        taxSubmission.Id = model.Id;
        _context.Update(taxSubmission);
        _context.SaveChanges();
        return taxSubmission.ToResponse();
    }

    public void DeleteTaxSubmission(int id)
    {
        var taxSubmission = GetById(id);
        _context.TaxSubmission.Remove(taxSubmission.FromResponse(_context));
        _context.SaveChanges();
    }
}
