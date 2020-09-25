using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace Excel
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var random = new Random(47);

            var people = new List<Person>();

            for (int i = 1; i <= 100; i++)
            {
                people.Add(new Person(i, "Person " + i, random.Next(1, 100)));
            }

            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "Zlobin";
                excelPackage.Workbook.Properties.Title = "Person";
                excelPackage.Workbook.Properties.Subject = "EPPlus demo export data";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                var worksheet = excelPackage.Workbook.Worksheets.Add("Person");

                worksheet.Cells[1, 1].Value = nameof(Person.Id);
                worksheet.Cells[1, 2].Value = nameof(Person.Name);
                worksheet.Cells[1, 3].Value = nameof(Person.Age);

                for (int i = 0, rowNumber = 2; i < people.Count; i++, rowNumber++)
                {
                    worksheet.Cells[rowNumber, 1].Value = people[i].Id;
                    worksheet.Cells[rowNumber, 2].Value = people[i].Name;
                    worksheet.Cells[rowNumber, 3].Value = people[i].Age;
                }

                var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Person.xlsx");
                excelPackage.SaveAs(fileInfo);
            }

            Console.ReadLine();
        }
    }
}