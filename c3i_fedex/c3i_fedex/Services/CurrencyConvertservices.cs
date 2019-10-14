using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace c3i_fedex.Services
{
    public class CurrencyConvertservices
    {
        [WebMethod]
        public decimal ConvertGOOG(decimal amount, string fromCurrency, string toCurrency)
        {
            WebClient web = new WebClient();
            string url = @"https://www.xe.com/de/currencyconverter/convert/?Amount="+ amount + "&From="+ fromCurrency.ToUpper() + "&To="+toCurrency.ToUpper()+"";


            string response1 = web.DownloadString(url);

        
            string apiURL = String.Format("http://finance.google.com/finance/converter?a={0}&from={1}&to={2}", amount, fromCurrency.ToUpper(), toCurrency.ToUpper());
            string response = web.DownloadString(apiURL);
            var split = response.Split((new string[] { "<span class=bld>" }), StringSplitOptions.None);
            var value = split[1].Split(' ')[0];
            decimal rate = decimal.Parse(value, CultureInfo.InvariantCulture);
            return rate;
        }
    }
}