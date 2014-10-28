_This branch is for the older PredictionIO 0.7 version_

Sensible.PredictionIO.NET
=========================
Sensible.PredictionIO.NET is an open source C# wrapper for the [PredictionIO] API. If you are not already familiar 
with PredictionIO, it is an "open source machine learning server for software developers to create predictive features, such as personalization, recommendation and content discovery". It is an amazing machine learning software which stands on the shoulders of giants such as Apache Mahout and MongoDB, and allows you to add features to your application such as:

* predict user behaviors
* offer personalized video, news, deals, ads and job openings
* help users to discover interesting events, documents, apps and restaurants
* provide impressive match-making services

Sensible.PredictionIO.NET supports PredictionIO version 0.7.0, but should be compatible with other versions as well.

This repo is maintained by Themos Piperakis from [Sensible].

Installation
------------
You may easily install the latest version of Sensible.PredictionIO.NET from the nuget command line by issuing the following command:
```
Install-Package Sensible.PredictionIO.NET
```

Quick start
----------
You will need to have access to an instance of PredictionIO, either locally (e.g. via a vagrant VM), or online (e.g. AWS AMI). For installation instructions have a look at the [PredictionIO] website.

After you setup the PredictionIO server, you will first need to create an application through the admin panel. Make note of the application key generated. After that, create an item recommendation engine with the default algorithm (kNN), and name it itemrec. 

Generating our sample data
--------------------------

You will then need to feed the engine with some sample data. There are two main concepts in PredictIO: users, and items. Let's first create 10 users for our sample application:
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

In the code above, we need the apiUrl for the PredictionIO instance (e.g. http://127.0.0.1:8000) and the application key generated for the item recommendation engine.

Since Sensible.PredictionIO.NET is based on [RestSharp], all requests follow the same philosophy: a Builder is first created from the corresponging class after passing the appropriate arguments. Then a RestClient is build with a .Build() method, and the response is returned after executing the .Execute() method. Of course, all our requests can be easily converted to asynchronous, just by using RestSharp's .ExecuteAsync()!

After our users are generated, we now need to add some items. Items have one or more item types, which in real life could represent a product category. In our example, "1" represents the item type.

```
for (var i = 1; i <= 50; i++)
{
    var builder = client.AddItemRequestBuilder(new Item
    {
        ItemId = i.ToString(),
        ItemTypes = new List<string> { "1" }
    });
    var request = builder.Build();
    var response = request.Execute(builder.RestRequest);
    if (response.StatusCode == HttpStatusCode.Created)
    {
        Console.WriteLine(string.Format("Item {0} created", i.ToString()));
    }
}
```

The last step to generate our sample data is to enter some user actions. A user action represents actions such as like, dislike, rate, etc. We are generating 10 random item actions for our users.

```
for (var i = 1; i <= 10; i++)
{
    for (var j = 1; j <= 10; j++)
    {
        var item = new Random().Next(1, 51);
        var builder = client.UserActionRequestBuilder(
            new UserAction
            {
                UserId = i.ToString(),
                ItemId = item.ToString(),
                Action = UserAction.Actions.View
            }
            );
        var request = builder.Build();
        var response = request.Execute(builder.RestRequest);
        if (response.StatusCode == HttpStatusCode.Created)
        {
            Console.WriteLine(string.Format("User {0} viewed item {1}", i.ToString(), item.ToString()));
        }
    }
}
```

Our data is now ready. At this point, our model needs to be trained before being able to generate recommendations. By default PredictionIO is setup to train the model every hour, but you can force a manual training through the admin panel. Please refer to [PredictionIO] documentation for further details.

Generating a recommendation
---------------------------

When you see that the engine in in Running status, it's time to generate our first user recommendation. Let's get 10 recommended items for user 1.

```
var builder = client.ItemRecGetTopNRequestBuilder(
    new RecommendationEngineRequest
    {
        UserId = "1",
        Engine = "itemrec",
        NumberOfItems = 10,
    });
var request = builder.Build();
var response = request.Execute<EngineResponse>(builder.RestRequest);
if (response.StatusCode == HttpStatusCode.OK)
{
    Console.WriteLine("Recommended items for user 1 are: " + string.Join(", ", response.Data.ItemIds));
}
else
{
    Console.WriteLine("An error occured. " + response.Content);
}
```

If everything goes as planned, PredictionIO will generate 10 recommended items for our user! Amazing, wasn't it?!


Dependencies
------------
The only external dependency for this project is [RestSharp], available on nuget via this command:
```
> Install-Package RestSharp
```

Changelog
---------
* 1.1: 
 * Added support for start/end time
 * Added support for item rating
* 1.0: Initial release

Roadmap
-------
* support for custom attributes for items



[PredictionIO]:http://prediction.io
[Sensible]:http://www.sensible.gr
[RestSharp]:http://restsharp.org
