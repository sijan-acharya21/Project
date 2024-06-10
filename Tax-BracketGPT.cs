Copy code
Install-Package CsvHelper
Step 2: Define the TaxBracket Class
csharp
Copy code
public class TaxBracket
{
    public decimal LowerBound { get; set; }
    public decimal UpperBound { get; set; }
    public decimal LowerTaxRate { get; set; }
    public decimal HigherTaxRate { get; set; }
}
Step 3: Read and Parse the CSV File with CsvHelper
csharp
Copy code
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public static List<TaxBracket> ReadTaxBrackets(string filePath)
{
    using var reader = new StreamReader(filePath);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
    return new List<TaxBracket>(csv.GetRecords<TaxBracket>());
}
Step 4: Get the Tax Bracket Based on Income
csharp
Copy code
public static TaxBracket GetTaxBracket(decimal weeklyIncome, List<TaxBracket> taxBrackets)
{
    return taxBrackets.Find(bracket => weeklyIncome >= bracket.LowerBound && weeklyIncome <= bracket.UpperBound);
}
Step 5: Main Program
csharp
Copy code
using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main()
    {
        string filePath = "path/to/your/csvfile.csv";
        var taxBrackets = ReadTaxBrackets(filePath);

        Console.Write("Enter the weekly income: ");
        if (decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal weeklyIncome))
        {
            var taxBracket = GetTaxBracket(weeklyIncome, taxBrackets);

            if (taxBracket != null)
            {
                Console.WriteLine($"For a weekly income of {weeklyIncome:C}:");
                Console.WriteLine($"Lower Tax Rate: {taxBracket.LowerTaxRate:P}");
                Console.WriteLine($"Higher Tax Rate: {taxBracket.HigherTaxRate:P}");
            }
            else
            {
                Console.WriteLine("No tax bracket found for the given income.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    static List<TaxBracket> ReadTaxBrackets(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return new List<TaxBracket>(csv.GetRecords<TaxBracket>());
    }

    static TaxBracket GetTaxBracket(decimal weeklyIncome, List<TaxBracket> taxBrackets)
    {
        return taxBrackets.Find(bracket => weeklyIncome >= bracket.LowerBound && weeklyIncome <= bracket.UpperBound);
    }
}
