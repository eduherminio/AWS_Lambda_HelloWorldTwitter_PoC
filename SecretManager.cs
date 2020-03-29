using System;
using System.IO;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Configuration;

namespace HelloWorldTwitterLambda
{
    internal static class SecretManager
    {
        private static readonly IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private static readonly string SecretId = Configuration["SecretId"];
        private static readonly RegionEndpoint Region = RegionEndpoint.GetBySystemName(Configuration["Region"]);

        internal static async Task<TwitterCredentials> GetTwitterCredentials(ILambdaContext context)
        {
            using var client = new AmazonSecretsManagerClient(Region);
            var request = new GetSecretValueRequest() { SecretId = SecretId };
            GetSecretValueResponse response = null;

            try
            {
                response = await client.GetSecretValueAsync(request);
                if (response == null)
                {
                    throw new AmazonSecretsManagerException("A null response was received");
                }
            }
            catch (Exception e)
            {
                context.Logger.LogLine($"Error retrieving Twitter credentials{Environment.NewLine}" +
                    $"{e.GetType().FullName}: {e.Message}{Environment.NewLine}");

                throw;
            }

            var secret = string.Empty;
            if (response.SecretString != null)
            {
                secret = response.SecretString;
            }
            else
            {
                using var reader = new StreamReader(response.SecretBinary);
                secret = Encoding.UTF8.GetString(Convert.FromBase64String(await reader.ReadToEndAsync()));
            }

            return JsonConvert.DeserializeObject<TwitterCredentials>(secret);
        }
    }
}
