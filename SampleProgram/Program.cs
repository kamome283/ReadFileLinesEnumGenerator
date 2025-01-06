using ReadFileLinesEnumGenerator.Generated;

namespace ConsoleApp1;

internal static class Program
{
    private static void Main()
    {
        foreach (var value in Enum.GetValues<valid>()) Console.WriteLine(value.ToStringValue());
    }
}