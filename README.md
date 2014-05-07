Sensible.PredictionIO.NET
=========================
Sensible.PredictionIO.NET is an open source C# wrapper for the PredictionIO API. If you are not already familiar 
with [PredictionIO], it is an "open source machine learning server for software developers to create predictive features, such as personalization, recommendation and content discovery". This library supports version 0.7.0 at the time of writing.

This repo is maintained by Themos Piperakis from [Sensible]

Quickstart
----------
You will need to have access to an instance of PredictionIO, either locally (e.g. via a vagrant VM), or online (e.g. AWS AMI). For installation instructions have a look the the [PredictionIO] website.

After you setup the PredictIO server, you will need to feed the engine with some sample data. There are two main concepts in PredictIO: users, and items. Let's first create 10 users for our sample application:
```
var client = new Client(ConfigurationManager.AppSettings["apiUrl"], ConfigurationManager.AppSettings["appKey"]);
for (var i = 1; i <= 10; i++)
{
    var builder = client.AddUserRequestBuilder(new User { UserId = i.ToString() });
    var request = builder.Build();
    var response = request.Execute(builder.RestRequest);
    if (response.StatusCode == HttpStatusCode.Created)
    {
        Console.WriteLine(string.Format("User {0} created", i.ToString()));
    }
}
```

Dependencies
------------
The only external dependency for this project is RestSharp, available on nuget via this command:
```
> Install-Package RestSharp
```

[PredictionIO]:http://prediction.io
[Sensible]:http://www.sensible.gr
