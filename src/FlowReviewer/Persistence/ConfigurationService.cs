using System.IO.Abstractions;
using System.Text.Json;
using AutomaticInterface;
using Ciandt.FlowTools.FlowReviewer.Common;
using Spectre.Console;

namespace Ciandt.FlowTools.FlowReviewer.Persistence;

public partial interface IConfigurationService;

[GenerateAutomaticInterface]
public class ConfigurationService(
    IAnsiConsole console,
    IFileSystem fileSystem,
    AppJsonContext jsonContext)
    : IConfigurationService
{
    private const string ConfigFileName = "appsettings.json";

    public Option<AppConfiguration> CurrentAppConfiguration { get; private set; }

    public Option<AppConfiguration> ReadOrCreate()
    {
        var appDataPath = ApplicationData.GetPath(fileSystem);

        var configurationFile = fileSystem.Path.Combine(appDataPath, ConfigFileName);
        if (!fileSystem.File.Exists(configurationFile))
        {
            console.MarkupLine($"[bold]No configuration file found:[/] {configurationFile}");
            var configuration = CreateConfigurationFile(appDataPath, configurationFile);
            return CurrentAppConfiguration = Some(configuration);
        }

        return CurrentAppConfiguration = Some(
            ReadConfigurationFile(configurationFile)
            ?? CreateConfigurationFile(appDataPath, configurationFile));
    }

    private AppConfiguration? ReadConfigurationFile(string configurationFile)
    {
        try
        {
            using var stream = fileSystem.File.OpenRead(configurationFile);
            var configuration = JsonSerializer.Deserialize(stream, jsonContext.AppConfiguration);
            return configuration;
        }
        catch (JsonException exception)
        {
            console.MarkupLine($"[bold]Invalid configuration file:[/] {exception.Message}");
            return null;
        }
    }

    private AppConfiguration CreateConfigurationFile(string appDataPath, string configurationFile)
    {
        var configuration = new AppConfiguration(
            console.Prompt(new TextPrompt<string>("Tenant:") { AllowEmpty = false }),
            console.Prompt(new TextPrompt<string>("Client ID:") { AllowEmpty = false }),
            console.Prompt(new TextPrompt<string>("Client Secret:") { AllowEmpty = false }));

        fileSystem.Directory.CreateDirectory(appDataPath);

        using var stream = fileSystem.File.Open(configurationFile, FileMode.Create, FileAccess.Write);
        JsonSerializer.Serialize(stream, configuration, jsonContext.AppConfiguration);
        return configuration;
    }
}
