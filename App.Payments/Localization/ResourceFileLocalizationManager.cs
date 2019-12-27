using System;
using System.Globalization;
using System.IO;
using System.Linq;
using App.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Payments.Localization
{
    public class ResourceFileLocalizationManager : ILocalizationManager, ITransientDependency
    {
        private const string DefaultCulture = "en-US";
        private const string ResourceFileFormat = "{0}.Resource.json";
        private const string ResourceFilePath = "Resources";

        private readonly ILogger<ResourceFileLocalizationManager> _logger;
        public ResourceFileLocalizationManager(ILogger<ResourceFileLocalizationManager> logger)
        {
            _logger = logger;
        }

        public string GetResource(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            return GetResourceInternal(key, culture);
        }

        public string GetResource(string key, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (culture == null)
                throw new ArgumentNullException(nameof(culture));
            return GetResourceInternal(key, culture);
        }

        private string GetResourceInternal(string key, CultureInfo culture)
        {
            var specificCultureResource = GetResourceForCulture(key, culture);
            if (specificCultureResource != null)
                return specificCultureResource;
            if (culture.ThreeLetterISOLanguageName != new CultureInfo(DefaultCulture).ThreeLetterISOLanguageName)
            {
                var defaultCultureResource = GetResourceForCulture(key, new CultureInfo(DefaultCulture));
                if (defaultCultureResource != null)
                    return defaultCultureResource;
            }

            _logger.LogError($"Can`t find any resource for key: {key}");
            throw new Exception($"Can`t find any resource for key: {key}");
        }

        private string GetResourceForCulture(string key, CultureInfo culture)
        {
            var fileSuffix = GetFileSuffixFromCulture(culture);
            var fileName = string.Format(ResourceFileFormat, fileSuffix);
            var filePath = Path.Combine(ResourceFilePath, fileName);
            if (!FileExists(filePath))
                return null;
            try
            {
                using(var fileStream = new StreamReader(GetFile(filePath)))
                using(JsonTextReader reader = new JsonTextReader(fileStream))
                {
                    JObject resourceObject = (JObject)JToken.ReadFrom(reader);
                    if (resourceObject == null)
                    {
                        _logger.LogError($"Cannot read localization file: {filePath}");
                        return null;
                    }
                    var prop = resourceObject.GetValue(key);
                    if (prop == null)
                        return null;
                    if (prop.Type != JTokenType.String)
                    {
                        _logger.LogError($"Localization file have invalid format. Expected to be dictionary: {filePath}");
                        return null;
                    }
                    return prop.Value<string>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot read localization file", ex);
                throw ex;
            }
        }

        private string GetFileSuffixFromCulture(CultureInfo culture)
        {
            return culture.ThreeLetterISOLanguageName;
        }

        private bool FileExists(string path)
        {
            var absolturePath = GetAbsolutePath(path);
            return absolturePath != null;
        }

        private Stream GetFile(string path)
        {
            var absolturePath = GetAbsolutePath(path);
            if (absolturePath == null)
                throw new Exception($"Could not find resource on path: {path}");
            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            return currentAssembly.GetManifestResourceStream(absolturePath);
        }

        private string GetAbsolutePath(string resourcePath)
        {
            var path = resourcePath
                .Replace('\\', '.')
                .Replace('/', '.');
            var absoluteResourcePath = typeof(ResourceFileLocalizationManager).Assembly
               .GetManifestResourceNames()
               .FirstOrDefault(r => r.EndsWith(path));
            return absoluteResourcePath;
        }
    }
}