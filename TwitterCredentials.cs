using Newtonsoft.Json;

namespace HelloWorldTwitterLambda
{
    internal class TwitterCredentials
    {
        [JsonProperty("TWITTER_CONSUMER_KEY")]
        public string ConsumerKey { get; set; }

        [JsonProperty("TWITTER_CONSUMER_SECRET_KEY")]
        public string ConsumerSecret { get; set; }

        [JsonProperty("TWITTER_ACCESS_TOKEN")]
        public string AccessToken { get; set; }

        [JsonProperty("TWITTER_ACCESS_TOKEN_SECRET")]
        public string AccessSecret { get; set; }
    }
}
