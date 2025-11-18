using linq_assignment.IoFiles;
using linq_assignment.services;

namespace linq_assignment;
public class Program
{
    public static void Main()
    {
        // Dependency Injection (manual for simplicity)
        IDataProvider dataProvider = new MockDataProvider();
        IQueryService queryService = new LinqService(dataProvider);
        var ui = new ConsoleIo(queryService);

        ui.Run();
    }
}