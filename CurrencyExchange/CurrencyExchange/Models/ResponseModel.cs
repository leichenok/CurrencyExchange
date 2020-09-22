using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyExchange.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public decimal Amount { get; set; }

        public ResponseModel(bool success)
        {
            Success = success;
        }

        public ResponseModel(bool success, decimal amount)
        {
            Success = success;
            Amount = amount;
        }
    }
}