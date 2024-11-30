using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите значение градусов: ");
        double degrees = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите исходную шкалу (C, K, F): ");
        string fromScale = Console.ReadLine().ToUpper();

        Console.Write("Введите целевую шкалу (C, K, F): ");
        string toScale = Console.ReadLine().ToUpper();

        double convertedDegrees = ConvertDegrees(degrees, fromScale, toScale);

        Console.WriteLine($"{degrees}°{fromScale} = {convertedDegrees}°{toScale}");
    }

    static double ConvertDegrees(double degrees, string fromScale, string toScale)
    {
        if (fromScale == "C")
        {
            if (toScale == "K")
            {
                return degrees + 273.15;
            }
            else if (toScale == "F")
            {
                return (degrees * 9 / 5) + 32;
            }
        }
        else if (fromScale == "K")
        {
            if (toScale == "C")
            {
                return degrees - 273.15;
            }
            else if (toScale == "F")
            {
                return (degrees - 273.15) * 9 / 5 + 32;
            }
        }
        else if (fromScale == "F")
        {
            if (toScale == "C")
            {
                return (degrees - 32) * 5 / 9;
            }
            else if (toScale == "K")
            {
                return (degrees - 32) * 5 / 9 + 273.15;
            }
        }

        return degrees;
    }
}

/*
class Program
{
    static void Main()
    {
        Console.Write("Введите слово: ");
        string input = Console.ReadLine();

        bool isPalindrome = CheckPalindrome(input);

        if (isPalindrome)
        {
            Console.WriteLine($"{input} является палиндромом.");
        }
        else
        {
            Console.WriteLine($"{input} не является палиндромом.");
        }
    }

    static bool CheckPalindrome(string word)
    {
        string cleanedWord = word.Replace(" ", "").ToLower();

        int length = cleanedWord.Length;
        for (int i = 0; i < length / 2; i++)
        {
            if (cleanedWord[i] != cleanedWord[length - 1 - i])
            {
                return false;
            }
        }
        return true;
    }
}
*/

/*
using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите количество месяцев: ");
        int months = Convert.ToInt32(Console.ReadLine());

        int rabbitPairs = CalculateRabbitPairs(months);

        Console.WriteLine($"Количество пар кроликов через {months} месяцев: {rabbitPairs}");
    }

    static int CalculateRabbitPairs(int months)
    {
        if (months == 0) return 0;
        if (months == 1) return 1;

        int previous = 1;
        int current = 1;

        for (int i = 2; i < months; i++)
        {
            int next = previous + current;
            previous = current;
            current = next;
        }

        return current;
    }
}
*/

/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

class Program
{
    static void Main()
    {
        string xlsxFilePath = "population_data.xlsx";
        string csvFilePath = "population_data.csv";

        ConvertXlsxToCsv(xlsxFilePath, csvFilePath);

        List<double> populations = ReadPopulationData(csvFilePath);

        Console.WriteLine("Выберите операцию:");
        Console.WriteLine("a. Максимум");
        Console.WriteLine("b. Минимум");
        Console.WriteLine("c. Среднее значение");
        Console.WriteLine("d. Исправленная выборочная дисперсия");
        char choice = Console.ReadKey().KeyChar;
        Console.WriteLine();

        switch (choice)
        {
            case 'a':
                Console.WriteLine($"Максимум: {populations.Max()}");
                break;
            case 'b':
                Console.WriteLine($"Минимум: {populations.Min()}");
                break;
            case 'c':
                Console.WriteLine($"Среднее значение: {populations.Average()}");
                break;
            case 'd':
                Console.WriteLine($"Исправленная выборочная дисперсия: {CalculateSampleVariance(populations)}");
                break;
            default:
                Console.WriteLine("Некорректный выбор.");
                break;
        }
    }

    static void ConvertXlsxToCsv(string xlsxFilePath, string csvFilePath)
    {
        using (var package = new ExcelPackage(new FileInfo(xlsxFilePath)))
        {
            var worksheet = package.Workbook.Worksheets[0];
            using (var writer = new StreamWriter(csvFilePath))
            {
                for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
                {
                    var line = string.Join(",", Enumerable.Range(1, worksheet.Dimension.End.Column)
                        .Select(col => worksheet.Cells[row, col].Text));
                    writer.WriteLine(line);
                }
            }
        }
    }

    static List<double> ReadPopulationData(string filePath)
    {
        List<double> populations = new List<double>();
        
        using (var reader = new StreamReader(filePath))
        {
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                if (double.TryParse(values[1], out double population))
                {
                    populations.Add(population);
                }
            }
        }

        return populations;
    }

    static double CalculateSampleVariance(List<double> data)
    {
        double mean = data.Average();
        double sumOfSquares = data.Sum(x => Math.Pow(x - mean, 2));
        return sumOfSquares / (data.Count - 1);
    }
}
*/