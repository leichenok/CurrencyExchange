using CurrencyExchange.BaseController;
using CurrencyExchange.Models;
using System;
using System.Web.Mvc;

namespace CurrencyExchange.Controllers
{
    public class HomeController : CustomController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new ExchangeModel();
            model.CurrencyCodes = ExchangeRateManager.GetRateCodes();

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Calculate(string code, string amount)
        {
            try
            {
                var amountDecimal = Convert.ToDecimal(amount, System.Globalization.CultureInfo.InvariantCulture);

                decimal? result = ExchangeRateManager.ConvertTo(amountDecimal, code);
                if (result == null)
                    return Json(new ResponseModel(false));

                return Json(new ResponseModel(true, result.Value));
            }
            catch (Exception)
            {
                return Json(false);
            }       
        }
    }
}