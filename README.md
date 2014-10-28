Sensible.PredictionIO.NET
=========================
Sensible.PredictionIO.NET is an open source C# wrapper for the [PredictionIO]. If you are not already familiar 
with PredictionIO, it is an "open source machine learning server for software developers to create predictive features, such as personalization, recommendation and content discovery". It is an amazing machine learning software which stands on the shoulders of giants such as Apache Hadoop and Elasticsearch, and allows you to add features to your application such as:

* predict user behaviors
* offer personalized video, news, deals, ads and job openings
* help users to discover interesting events, documents, apps and restaurants
* provide impressive match-making services

Sensible.PredictionIO.NET supports PredictionIO version 0.8. A branch for 0.7 is also available.

This repo is maintained by Themos Piperakis from [Sensible].

Quick start
----------
You will need to have access to an instance of PredictionIO, either locally (e.g. via a vagrant VM), or online (e.g. AWS AMI). For installation instructions have a look at the [PredictionIO] website.

After you setup the PredictionIO server, you will first need to create an application based on an algorithm.

Generating our sample data
--------------------------

You will then need to feed the EventServer with some sample data. There are two main concepts in PredictIO: users, and items. Let's first create 10 users for our sample application:
```
var eventClient = new EventClient(ConfigurationManager.AppSettings["eventUrl"], ConfigurationManager.AppSettings["appId"]);
for (var i = 1; i <= 10; i++)
{
    var eventId = eventClient.SetUser(i.ToString());
    Console.WriteLine(string.Format("User {0} created, event id {1}", i.ToString(), eventId));
}
```

In the code above, we need the eventUrl for the PredictionIO EventServer instance (e.g. http://127.0.0.1:7070) and the application key generated for the item recommendation engine.

Since Sensible.PredictionIO.NET is based on [RestSharp], most requests are available as both synchronous or asynchronous calls. So, in
order to call SetUser asynchronously, you would use SetUserAsync, which returns Task<string>.

After our users are generated, we now need to add some items. Items have one or more item types, which in real life could represent a product category. In our example, "1" represents the item type.

```
for (var i = 1; i <= 50; i++)
{
    var eventId = eventClient.SetItem(i.ToString(), new[] { "1" });
    Console.WriteLine(string.Format("Item {0} created, event id {1}", i.ToString(), eventId));
}
```

The last step to generate our sample data is to enter some user actions. A user action represents actions such as like, dislike, rate, etc. .

```
for (var i = 1; i <= 10; i++)
{
    for (var j = 1; j <= 10; j++)
    {
        eventClient.SetActionItem(i.ToString(), j.ToString(), "like");
    }
}
```

Alternatively, you may also rate items if your algorithm supports it. In a vanilla PredictionIO installation, you will need to set booleanData to false in algorithms.json, in order to supply a numeric rating for the action.

```
for (var i = 1; i <= 10; i++)
{
    for (var j = 1; j <= 10; j++)
    {
        var rating = new Random().Next(1, 6);
        eventClient.RateItem(i.ToString(), j.ToString(), rating);
    }
}
```

Our data is now ready. At this point, our model needs to be trained before being able to generate recommendations. Please refer to [PredictionIO] documentation for further details.

Generating a recommendation
---------------------------

When you have successfully deployed the engine, it's time to generate our first user recommendation. Let's get 5 recommended items for user 1.

```
var engineClient = new EventClient(ConfigurationManager.AppSettings["engineUrl"]);
var recommendations = engineClient.GetItemRecommendations("1", 5);
```

In the code above, we need the eventUrl for the PredictionIO engine instance (e.g. http://127.0.0.1:8000). If everything goes as planned, PredictionIO will generate 5 recommended items for our user, along with a prediction score! Amazing, wasn't it?!

The library also supports the item ranking algorithm. All you have to do is call the GetItemRankings method, passing the user id and a string array of item ids you want ranking for. If the method returns an empty list, this implies PredictionIO was not able to compute ranking for these items

```
var engineClient = new EventClient(ConfigurationManager.AppSettings["engineUrl"]);
var rankings = engineClient.GetItemRankings("1", new []{"1","2","3","4","5"});
```

Dependencies
------------
The only external dependencies for this project are [RestSharp] and [Newtonsoft.Json], available on nuget via this command:
```
> Install-Package RestSharp
> Install-Package Newtonsoft.Json
```

Changelog
---------
* 1.0: Initial release


[PredictionIO]:http://prediction.io
[Sensible]:http://www.sensible.gr
[RestSharp]:http://restsharp.org
[Newtonsoft.Json]:http://james.newtonking.com/json
