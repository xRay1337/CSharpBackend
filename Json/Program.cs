using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri("https://restcountries.eu/rest/v2/region/americas");
            var request = WebRequest.Create(uri);
            var response = request.GetResponse();

            string jsonText;

            using (var stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                jsonText = stream.ReadToEnd();
            }

            var countries = JsonConvert.DeserializeObject<List<Country>>(jsonText);

            var sum = countries.Sum(c => c.Population);

            Console.WriteLine($"Сумма по странам: {sum}");

            var allCurrencyName = countries.SelectMany(country => country.Currencies)
                                            .Select(currency => currency.Name)
                                            .Where(n => n != null)
                                            .Distinct()
                                            .OrderBy(name => name);

            Console.WriteLine("Список валют:");

            foreach (var currencyName in allCurrencyName)
            {
                Console.WriteLine(currencyName);
            }

            Console.ReadLine();
        }
    }
}