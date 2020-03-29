using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Tweetinvi;

namespace HelloWorldTwitterLambda
{
    public class Functions
    {
        /// <summary>
        /// The main entry point for the custom runtime.
        /// </summary>
        public static async Task Main()
        {
            Func<ILambdaContext, Task> handler = FunctionHandler;
            using var handlerWrapper = HandlerWrapper.GetHandlerWrapper(handler);
            using var bootstrap = new LambdaBootstrap(handlerWrapper);
            await bootstrap.RunAsync();
        }

        /// <summary>
        /// A simple function that posts a Tweet saying "Hello world!"
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static async Task FunctionHandler(ILambdaContext context)
        {
            var credentials = await SecretManager.GetTwitterCredentials(context);

            Auth.SetUserCredentials(credentials.ConsumerKey, credentials.ConsumerSecret, credentials.AccessToken, credentials.AccessSecret);

            var me = User.GetAuthenticatedUser();

            try
            {
                me.PublishTweet("This is a test tweet, please ignore it!!");
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
