using CsvHelper.Configuration;

namespace CsvHelperDemo.Services.File;

public class PersonMap : ClassMap<Person>
{
    public PersonMap()
    {
        Map(m => m.Id).Index(0).Name("id");
        Map(m => m.Name).Index(1).Name("name");
    }
}
