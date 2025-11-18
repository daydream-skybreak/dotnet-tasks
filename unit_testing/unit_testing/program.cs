using unit_testing.services;

namespace unit_testing;

using unit_testing.services;
using unit_testing.IoFiles;

public class Program
{
    public static void Main()
    {
        // Dependency Injection (manual for simplicity)
        IDataProvider dataProvider = new MockDataProvider();
        IQueryService queryService = new LinqService(dataProvider);
        var ui = new ConsoleIO(queryService);

        ui.Run();
    }
}