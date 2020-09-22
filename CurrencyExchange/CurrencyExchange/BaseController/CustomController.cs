using CurrencyExchange.Managres;
using System.Web.Mvc;

namespace CurrencyExchange.BaseController
{
    public class CustomController : Controller
    {
        protected ExchangeRateManager ExchangeRateManager
        {
            get
            {
                if (_exchangeRateManager == null)
                    _exchangeRateManager = new ExchangeRateManager();

                return _exchangeRateManager;
            }
        }
        private ExchangeRateManager _exchangeRateManager;

        public CustomController()
        {
        }
    }
}