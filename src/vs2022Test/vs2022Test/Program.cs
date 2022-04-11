// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Newtonsoft.Json;
using UtilityLibraries;

Console.WriteLine("What`s your name?");
var name = Console.ReadLine();
var currentDate = DateTime.Now;
Console.WriteLine($"{Environment.NewLine} Hello, {name}, on {currentDate:d} at {currentDate:t}!");
Console.WriteLine($"{Environment.NewLine} Press any key to exit...");
Console.ReadKey();

// Console.WriteLine(name.StartsWithUpper());
/* project library */
int row = 0;
do
{
    if (row == 0 || row > 25)
        ResetConsole();

    string? input = Console.ReadLine();
    if (string.IsNullOrEmpty(input)) ;
    Console.WriteLine($"Input:{input}");
    Console.WriteLine("Begins with uppercase? " +
     $"{(input.StartsWithUpper() ? "Yes" : "No")}");
    Console.WriteLine();
    row += 4;
} while (true);

// reference Nuget Sample
Account account = new Account
{
    Name = "John Doe",
    Email = "john@microsoft.com",
    DOB = new DateTime(1980, 2, 20, 0, 0, 0, DateTimeKind.Utc),
};
string json = JsonConvert.SerializeObject(account, Formatting.Indented);
// TextBlock.Text = json;


void ResetConsole()
{
    if (row > 0)
    {
        Console.WriteLine("Press any key to continute...");
        Console.ReadKey();
    }
    Console.Clear();
    Console.WriteLine($"{Environment.NewLine} Press <Enter> only to exit; otherwise, enter a string and press <Enter>:{Environment.NewLine}");

}


