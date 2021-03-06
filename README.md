# HelloWorld Twitter Lambda PoC

## Requirements

- A role with `AWSLambdaBasicExecutionRole` and `SecretsManagerReadWrite` policies (named `lambdaRole` in this example).
- A [Twitter DEV account](https://developer.twitter.com/).
- An secret in AWS `SecretsManager` with Twitter dev credentials (see `JsonProperty`s in `TwitterCredentials.cs`).

## Instructions

Install or update Amazon.Lambda.Tools

```bash
    dotnet tool install -g Amazon.Lambda.Tools
    dotnet tool update -g Amazon.Lambda.Tools
```

Deploy the lambda function

```bash
    dotnet lambda deploy-function
```

Test the lambda function

```bash
    dotnet lambda invoke-function
```

Cleanup

```bash
    dotnet lambda delete-function HelloWorldTwitterLambda
```

---

Made using [tweetinvi](https://github.com/linvi/tweetinvi) Twitter API library.
