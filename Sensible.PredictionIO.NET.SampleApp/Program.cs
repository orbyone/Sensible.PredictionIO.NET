using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace Sensible.PredictionIO.NET.SampleApp
{
    class Program
    {

        static void Main()
        {
            var client = new Client(ConfigurationManager.AppSettings["apiUrl"], ConfigurationManager.AppSettings["appKey"]);
            Console.WriteLine("Generating sample data");
            GenerateSampleData(client);
            Console.WriteLine("Sample data has been entered. Please train the model from the PredictionIO admin panel before running next step. Press ENTER when ready.");
            Console.ReadLine();
            GetRecommendation(client, ConfigurationManager.AppSettings["recommendationEngine"]);
            Console.ReadLine();
        }

        private static void GenerateSampleData(Client client)
        {
            //Generate 10 users
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
            //Generate 50 items
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
            //Generate 10 random view actions for each user
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
        }

        private static void GetRecommendation(Client client, string engine)
        {
            //Get recommendation for user. Make sute you train the model from the PredictionIO admin panel first
            var builder = client.ItemRecGetTopNRequestBuilder(
                new RecommendationEngineRequest
                {
                    UserId = "1",
                    Engine = engine,
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
        }
    }
}
