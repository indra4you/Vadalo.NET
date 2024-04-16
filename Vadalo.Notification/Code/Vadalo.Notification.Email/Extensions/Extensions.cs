using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Vadalo.Notification;

internal static class Extensions
{
    internal static async Task<string> LoadHtmlTemplate(
        this EmailNotificationBase _,
        Assembly fromAssembly,
        string templatePath,
        string templateNameWithoutExtension
    )
    {
        var trimmedTemplatePath = templatePath
            .TrimStart('/')
            .TrimEnd('/');
        var trimmedTemplateName = templateNameWithoutExtension
            .TrimStart('/')
            .TrimEnd('/');

        try
        {
            var rootNamespace = fromAssembly
                .ExportedTypes
                .First()
                .Namespace;
            var embeddedFileProvider = new EmbeddedFileProvider(
                fromAssembly,
                rootNamespace
            );

            var resourceStream = embeddedFileProvider
                .GetFileInfo(
                    $"{trimmedTemplatePath}/{trimmedTemplateName}.html"
                )
                .CreateReadStream();

            var streamReader = new StreamReader(
                resourceStream
            );

            var streamContent = await streamReader
                .ReadToEndAsync();

            return streamContent;
        }
        catch (FileNotFoundException e)
        {
            throw new EmailTemplateNotFoundException(
                $"Email Template '{templatePath}/{templateNameWithoutExtension}' not found",
                e
            );
        }
    }
}