using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Sensible.PredictionIO.NET.Domain;

namespace Sensible.PredictionIO.NET.Clients
{
    public class EngineClient : BaseClient
    {

        public EngineClient(string apiUrl)
            : base(apiUrl)
        {
        }

        public void SendQuery()
        {

        }

        public List<ItemRecommendation> GetItemRecommendations(string userId, int maxNumberOfItems)
        {
            var request = new JObject();
            request[Constants.UserId] = userId;
            request[Constants.MaxNumberOfItems] = maxNumberOfItems;
            var body = request.ToString(Formatting.None);
            var response = (JObject) Execute(Constants.EngineResource, Method.POST, body);
            var recommendations = new List<ItemRecommendation>();
            foreach (var item in response[Constants.Items])
            {
                var property = item.First.Value<JProperty>();
                var recommendation = new ItemRecommendation
                {
                    ItemId = property.Name,
                    Score = item.First.First.Value<float>()
                };
                recommendations.Add(recommendation);
            }
            return recommendations;
        
        } 
        
        public async Task<List<ItemRecommendation>> GetItemRecommendationsAsync(string userId, int maxNumberOfItems)
        {
            var request = new JObject();
            request[Constants.UserId] = userId;
            request[Constants.MaxNumberOfItems] = maxNumberOfItems;
            var body = request.ToString(Formatting.None);
            var response = (JObject) (await ExecuteAsync(Constants.EngineResource, Method.POST, body));
            var recommendations = new List<ItemRecommendation>();
            foreach (var item in response[Constants.Items])
            {
                var property = item.First.Value<JProperty>();
                var recommendation = new ItemRecommendation
                {
                    ItemId = property.Name,
                    Score = item.First.First.Value<float>()
                };
                recommendations.Add(recommendation);
            }
            return recommendations;
        
        }
    }
}
