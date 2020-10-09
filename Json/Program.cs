using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Json
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Trace("Приложение запущено");

            List<Country> countries;

            try
            {
                var uri = new Uri("https://restcountries.eu/rest/v2/region/americas");
                logger.Trace("Подключение к ресурсу: " + uri);
                var request = WebRequest.Create(uri);
                logger.Trace("Ожидание ответа");
                var response = request.GetResponse();

                logger.Trace("Чтение ответа");
                string jsonText;

                using (var stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    jsonText = stream.ReadToEnd();
                }

                logger.Trace("Десериализации ответа");
                countries = JsonConvert.DeserializeObject<List<Country>>(jsonText);
            }
            catch (WebException ex)
            {
                logger.Error(ex, "Ошибка на WEB уровне.");
                throw;
            }
            catch (IOException ex)
            {
                logger.Error(ex, "Ошибка на Stream уровне.");
                throw;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }

            logger.Trace("Подсчёт суммы");
            var sum = countries.Sum(c => c.Population);

            Console.WriteLine($"Сумма по странам: {sum}");

            logger.Trace("Подготовка списка валют");
            var allCurrenciesName = countries.SelectMany(country => country.Currencies)
                                            .Where(c => c != null && c.Name != null && c.Code != null)
                                            .Select(currency => currency.Name)
                                            .Distinct()
                                            .OrderBy(name => name);

            Console.WriteLine("Список валют:");
            foreach (var currencyName in allCurrenciesName)
            {
                Console.WriteLine(currencyName);
            }

            Console.ReadLine();
            logger.Trace("Приложение остановлено");
        }
    }
}