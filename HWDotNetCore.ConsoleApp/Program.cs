using HWDotNetCore.ConsoleApp.RestClientExamples;

internal class Program
{
    private static async Task Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");

        RestClientExample restClientExample = new RestClientExample();
        await restClientExample.Run();

        Console.ReadKey();
    }
}