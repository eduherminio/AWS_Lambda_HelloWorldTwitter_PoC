using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Tweetinvi;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace HelloWorldTwitterLambda
{
    public static class Functions
    {
        /// <summary>
        /// A simple function that posts a Tweet saying "Hello world!"
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task HelloWorld(ILambdaContext context)
        {
            var credentials = await SecretManager.GetTwitterCredentials(context);

            Auth.SetUserCredentials(credentials.ConsumerKey, credentials.ConsumerSecret, credentials.AccessToken, credentials.AccessSecret);

            var me = User.GetAuthenticatedUser();

            try
            {
                me.PublishTweet("Hello world!!!");
            }
            catch (Exception e)
            {
                context.Logger.LogLine($"Error posting tweet{Environment.NewLine}" +
                    $"{e.GetType().FullName}: {e.Message}{Environment.NewLine}");

                throw;
            }
        }
    }
}
