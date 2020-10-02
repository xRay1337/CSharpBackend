using OfficeOpenXml;
using OfficeOpenXml.Style;
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

            var people = new List<Person>(100);

            for (var i = 1; i <= 100; i++)
            {
                people.Add(new Person(i, "Person " + i, random.Next(1, 100), "8-913-XXX-XX-XX"));
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
                worksheet.Cells[1, 4].Value = nameof(Person.Phone);

                for (int i = 0, rowNumber = 2; i < people.Count; i++, rowNumber++)
                {
                    worksheet.Cells[rowNumber, 1].Value = people[i].Id;
                    worksheet.Cells[rowNumber, 2].Value = people[i].Name;
                    worksheet.Cells[rowNumber, 3].Value = people[i].Age;
                    worksheet.Cells[rowNumber, 4].Value = people[i].Phone;
                }

                var totalRows = worksheet.Dimension.End.Row;
                var totalColumns = worksheet.Dimension.End.Column;
                var headerCells = worksheet.Cells[1, 1, 1, totalColumns];
                var headerFont = headerCells.Style.Font;
                headerFont.Bold = true;
                var allCells = worksheet.Cells[1, 1, totalRows, totalColumns];
                allCells.AutoFitColumns();
                allCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                var border = allCells.Style.Border;
                border.Top.Style = border.Left.Style = border.Bottom.Style = border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, 1, totalColumns].AutoFilter = true;

                var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Person.xlsx");
                excelPackage.SaveAs(fileInfo);
                Console.WriteLine("Done");
            }

            Console.ReadLine();
        }
    }
}