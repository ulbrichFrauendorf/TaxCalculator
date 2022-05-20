namespace TaxCalculatorAPI.TaxCalculator.Controllers;

using Microsoft.AspNetCore.Mvc;
using TaxCalculatorAPI.Authorization.Attributes;
using TaxCalculatorAPI.ExceptionHandlers;
using TaxCalculatorAPI.TaxCalculator.Services;
using TaxCalculatorAPI.TaxCalculator.ViewModels;

[Authorize]
[Route("tax/submission")]
[ApiController]
public class TaxSubmissionsController : ControllerBase
{
    ITaxCalculatorService _taxCalculatorService;
    public TaxSubmissionsController(ITaxCalculatorService taxCalculatorService)
    {
        _taxCalculatorService = taxCalculatorService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var taxSubmissions = _taxCalculatorService.GetTop10();
        return Ok(taxSubmissions);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var taxSubmission = _taxCalculatorService.GetById(id);
        return Ok(taxSubmission);
    }

    [HttpPost]
    public IActionResult Post(TaxSubmissionSubmit model)
    {
        var response = _taxCalculatorService.PostTaxSubmission(model);
        return Ok(new { message = "Tax submission Posted successfully" });
    }

    [HttpPut]
    public IActionResult Update(TaxSubmissionUpdate model)
    {
        var response = _taxCalculatorService.UpdateTaxSubmission(model);
        return Ok(new { message = "Tax submission Updated successfully" });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _taxCalculatorService.DeleteTaxSubmission(id);
        return Ok(new { message = "Tax submission deleted successfully" });
    }
}
