using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CurrencyExchange.Models;

namespace CurrencyExchange.Managres
{
    public class ExchangeRateManager
    {
        private static readonly string DAILY_EXCHANGE_RATE_URL = "http://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.txt";
        private static Dictionary<string, Currency> _exchangeRatesCache = new Dictionary<string, Currency>();


        public ExchangeRateManager()
        {
        }


        public static void LoadFromServer()
        {
            try
            {
                _exchangeRatesCache.Clear();

                using (HttpClient client = new HttpClient())
                {
                    using (var response = client.GetAsync(DAILY_EXCHANGE_RATE_URL, HttpCompletionOption.ResponseHeadersRead).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (var stream = response.Content.ReadAsStreamAsync().Result)
                            {
                                using (var sReader = new StreamReader(stream))
                                {
                                    while (!sReader.EndOfStream)
                                    {
                                        string rawData = sReader.ReadLine();

                                        ParseData(rawData);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                _exchangeRatesCache.Clear();
            }
        }

        public List<string> GetRateCodes()
        {
            return _exchangeRatesCache.Keys.ToList();
        }
        public decimal? ConvertTo(decimal amount, string targerCurrencyCode)
        {
            try
            {
                if (!_exchangeRatesCache.TryGetValue(targerCurrencyCode, out Currency currency))
                    return null;

                decimal convertedAmount = (decimal)currency.Rate * amount;
                return decimal.Round(convertedAmount, 2);
            }
            catch (Exception)
            {
                return null;
            }
        }


        private static void ParseData(string rawData)
        {
            try
            {
                string header = "země|měna|množství|kód|kurz";
                if (rawData.Equals(header))
                    return;

                var regex = new Regex(@"^([0-9]{2})[.]([0-9]{2})[.]([0-9]{4})");
                if (regex.IsMatch(rawData))
                    return;

                ParseExchangeRate(rawData);
            }
            catch (Exception)
            {
            }
        }
        private static void ParseExchangeRate(string rawData)
        {
            try
            {
                char separator = '|';

                string[] parsedDataArray = rawData.Split(separator);
                if (parsedDataArray.Length < 0)
                    return;

                //will use only currency code
                _exchangeRatesCache.Add(
                    parsedDataArray[3],
                    new Currency()
                    {
                        Code = parsedDataArray[3],
                        Rate = float.Parse(parsedDataArray[4], System.Globalization.NumberStyles.Float),
                    });
            }
            catch (Exception)
            { }
        }
    }
}