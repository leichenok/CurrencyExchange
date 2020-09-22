using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyExchange.Models
{
    public class ExchangeModel
    {
        public bool IsDataLoaded
        {
            get { return CurrencyCodes != null && CurrencyCodes.Count() > 0; }
        }
        public List<string> CurrencyCodes { get; set; }
    }
}