using System.Text;

Console.Write("Enter the path: ");
var path = Console.ReadLine();

if (!Directory.Exists(path))
{
    Console.Write("Specified folder not found");
    Console.ReadKey();
    return;
}
Console.Write("Specify file extension e.g. 'txt','md' (by default *) ");
var enterExt = Console.ReadLine();
var ext = string.IsNullOrWhiteSpace(enterExt) ? "*" : enterExt.Trim();

var files = Directory.EnumerateFiles(path, $"*.{ext}", SearchOption.AllDirectories).ToList();

if (!files.Any())
{
    Console.Write("Files not found");
    Console.ReadKey();
    return;
}

Console.WriteLine($"Found {files.Count} files");

Console.Write("Enter the lines to add (type #exit to finish entering): ");
StringBuilder textLines = new();
string? line;
while ((line = Console.ReadLine()) != "#exit")
{
    textLines.AppendLine(line);
}


Console.WriteLine($"Add the text :{Environment.NewLine}'{textLines}'{Environment.NewLine} to the found files ? (y/n) ");
if (Console.ReadKey().Key != ConsoleKey.Y)
    return;

foreach (var file in files)
{
    Console.Write(file + ": ");

    if (!File.Exists(file))
        Console.WriteLine("File not found");

    try
    {
        File.AppendAllText(file, textLines.ToString());
        Console.WriteLine("OK");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

Console.WriteLine("Done");
Console.ReadKey();


