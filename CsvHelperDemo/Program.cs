using CsvHelper.Sevices.File;

FileService fileService = new FileService();

await fileService.WriteCsvFile();
await fileService.ReadCsvFile();
