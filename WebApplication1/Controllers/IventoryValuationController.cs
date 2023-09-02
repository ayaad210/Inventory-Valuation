using businessLayer.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorePro.ViewModels;
using System.Data;

namespace StorePro.Controllers
{
    [Authorize(Roles = "Employee")]

    public class InventoryValuationController : Controller
    {
        private readonly IInvintoryValuationService invintoryValuation;

        public InventoryValuationController(IInvintoryValuationService invintoryValuation)
        {
            this.invintoryValuation = invintoryValuation;
        }
        [HttpGet]

        public async Task<IActionResult> Valuation()
        {
            return View(new ValuationInputs() { });
        }
        [HttpPost]

        public async Task<IActionResult> Valuation([Bind(include: "ValuationMethod")] ValuationInputs valuationInputs)
        {

            try
            {
                await invintoryValuation.SetStrategy(valuationInputs.ValuationMethod);

                var res = await invintoryValuation.EvaluateInventory();

                ViewBag.ValuationRes = res.TotalProfit;
                ViewBag.TotalMony = res.TotalMony;
                ViewBag.TotalCost = res.TotalCost;
                return View(valuationInputs);
            }
            catch (Exception)
            {
                return RedirectToAction("BadRequest", "Default");

            }


        }
    }
}
