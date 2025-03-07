using System.Text;
using System.Text.Json;
using Microsoft.Extensions.FileProviders;
using XAlarm.Center.Domain.Options;

namespace XAlarm.Center.Api.Extensions;

public static class ConfigurationExtensions
{
    public static void AddOptionsConfiguration(this IConfigurationBuilder configurationBuilder, string fileName)
    {
        configurationBuilder.AddJsonFile(
            new PhysicalFileProvider(Path.Combine(AppContext.BaseDirectory, "..", "assets", "options")),
            $"{fileName}.json", false, false);

        configurationBuilder.AddJsonFile(
            new PhysicalFileProvider(Path.Combine(AppContext.BaseDirectory, "..", "assets", "options")),
            $"{fileName}.Development.json", true, false);
    }

    public static void AddIdentitySettingsConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddJsonStream(new MemoryStream(
            Encoding.ASCII.GetBytes(JsonSerializer.Serialize(new { IdentityOptions = IdentityOptions.Default }))));
    }
}