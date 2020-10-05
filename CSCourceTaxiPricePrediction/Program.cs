using System;
using CSCourseML.Model;

namespace CSCourceTaxiPricePrediction
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new ModelInput("CMT", 1, 1, 474, 1.5f, "CRD");

            ModelOutput result = ConsumeModel.Predict(input);

            Console.WriteLine($"Ожидается: ${8}");
            Console.WriteLine($"Прогноз: ${result.Score}");
        }
    }
}