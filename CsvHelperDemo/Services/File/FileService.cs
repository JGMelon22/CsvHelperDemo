using System.Globalization;
using Bogus;
using CsvHelper.Configuration;
using CsvHelperDemo.Services.File;

namespace CsvHelper.Sevices.File;

public class FileService
{
    // MOCK_DATA.csv
    private readonly string filePath = Path.Combine(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar.ToString() + "CsvHelpWriter.csv");
    private readonly string filePathWrite = Path.Combine(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar.ToString() + "CsvHelpWriter.csv");

    public async Task ReadCsvFile()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Reading CSV file...");
        Console.ResetColor();

        CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower(),
        };

        using (StreamReader reader = new StreamReader(filePath))
        using (CsvReader csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<PersonMap>();

            await csv.ReadAsync();
            csv.ReadHeader();

            List<CsvHelperDemo.Person>? records = csv.GetRecords<CsvHelperDemo.Person>().ToList();

            foreach (CsvHelperDemo.Person? item in records)
            {
                Console.WriteLine($"{item.Id}, {item.Name}");
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Amount of records: {records.Count()}");
            Console.ResetColor();

            // while (await csv.ReadAsync())
            // {
            //     CsvHelperDemo.Person? records = csv.GetRecord<Person>();

            //     Console.WriteLine($"{records.Id}, {records.Name}");
            // }
        }
    }

    public async Task WriteCsvFile()
    {
        Faker<CsvHelperDemo.Person>? fakePerson = new Faker<CsvHelperDemo.Person>()
            .RuleFor(c => c.Id, f => f.IndexFaker + 1)
            .RuleFor(c => c.Name, f => f.Person.FullName);

        List<CsvHelperDemo.Person>? records = fakePerson.Generate(50);

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Writing CSV file...");
        Console.ResetColor();

        using (StreamWriter writer = new StreamWriter(filePathWrite))
        using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<PersonMap>();

            csv.WriteRecords(records);
        }

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("CSV File Wrote.");
        Console.ResetColor();

        await Task.CompletedTask;
    }
}
