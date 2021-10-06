using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace PixelDesk.Domain.Extesnsions
{
    internal class DotEnvConfigProvider : ConfigurationProvider, IConfigurationSource
    {
        private string envFilePath;

        public DotEnvConfigProvider(string envFilePath)
        {
            this.envFilePath = envFilePath;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return this;
        }

        public override void Load()
        {
            if (string.IsNullOrEmpty(envFilePath) || !File.Exists(envFilePath))
                return;

            foreach (var line in File.ReadAllLines(envFilePath))
            {
                var parts = line.Split('=', System.StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2)
                    continue;
                var key = parts[0];
                var value = string.Join('=', parts.Skip(1));

                Data.Add(key, value);
            }
        }
    }
}
