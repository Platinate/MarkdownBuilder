// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using System.CommandLine;

namespace mdbuilder;
class Program
{
    static async Task Main(string[] args)
    {
        var directoryOption = new Option<string>(name: "--directory",
            description: "The base directory where the command line will work on.",
            getDefaultValue: Directory.GetCurrentDirectory);
        var subFolderOption = new Option<string>(name: "--sub",
            description: "The sub directory where the markdown files are stored.",
            getDefaultValue: () => string.Empty);
        var outputFilenameOption = new Option<string>(name: "--output",
            description: "The sub directory where the markdown files are stored.",
            getDefaultValue: () => "output");

        var command = new RootCommand("A command to merge markdown files together");
        command.AddOption(directoryOption);
        command.AddOption(subFolderOption);
        command.AddOption(outputFilenameOption);

        command.SetHandler(MergeMarkdowns, directoryOption, subFolderOption, outputFilenameOption);

        await command.InvokeAsync(args);
    }

    private static void MergeMarkdowns(string directory, string subFolder, string outputFilename)
    {
        var folderToSearchOn = string.IsNullOrEmpty(directory) ? Directory.GetCurrentDirectory() : directory;
        var filesInTheDirectory = Path.Combine(folderToSearchOn ,subFolder);
        var baseFilename = "index";
        var baseFileContent = File.ReadAllText($"{folderToSearchOn}/{baseFilename}.md");
        var regex = new Regex("#(?'filename'.+)#");
        var matches = regex.Matches(baseFileContent);
        if (matches.Count <= 0) return;
        foreach (Match match in matches)
        {
            var targetFilename = match.Groups["filename"].Value;
            var fileContent = File.ReadAllText($"{filesInTheDirectory}/{targetFilename}.md");
            baseFileContent = Regex.Replace(baseFileContent, $"#{targetFilename}#", fileContent);
        }
        File.WriteAllText(Path.Combine(directory, outputFilename + ".md"), baseFileContent);
    }
}



