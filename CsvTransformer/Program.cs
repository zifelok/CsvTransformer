if (args.Length < 1)
{
    Console.WriteLine("Usage: CsvTransformer <configFilePath>");
    return;
}

var configFilePath = args[0];
if (!File.Exists(configFilePath))
{
    Console.WriteLine($"Configuration file not found: {configFilePath}");
    return;
}

try
{
    var yamlContent = File.ReadAllText(configFilePath);
    var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
        .WithNamingConvention(YamlDotNet.Serialization.NamingConventions.CamelCaseNamingConvention.Instance)
        .Build();
    var config = deserializer.Deserialize<CsvTransformer.Configuration.Configuration>(yamlContent);
    if (config.Files == null || config.Files.Length == 0)
    {
        Console.WriteLine("No files configured for transformation.");
        return;
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error reading or parsing configuration file: {ex.Message}");
    return;
}