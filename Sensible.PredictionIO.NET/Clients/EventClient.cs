using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Sensible.PredictionIO.NET.Domain;

namespace Sensible.PredictionIO.NET.Clients
{
    public class EventClient : BaseClient
    {
        public int AppId { get; private set; }

        public EventClient(string eventUrl, int appId, string accessKey)
            : base(eventUrl, accessKey)
        {
            ApiUrl = eventUrl;
            AppId = appId;
        }

        public bool CheckServerStatus()
        {
            var response = Execute(Constants.RootResource, Method.GET, null);
            var value = response[Constants.Status].Value<string>();
            return value == Constants.Alive;
        }

        public string SetUser(string userId, Dictionary<string, object> properties = null)
        {
            var request = new Event(AppId, Constants.SetEvent, userId, Constants.User, null, null, properties);
            var body = request.ToString(Formatting.None);
            var response = Execute(Constants.EventsResource, Method.POST, body);
            return response[Constants.EventId].Value<string>();
        }

        public string UnsetUser(string userId, Dictionary<string, object> properties)
        {
            if (properties == null || !properties.Any())
            {
                throw new InvalidOperationException("Properties cannot be null or empty for unset operation");
            }
            var request = new Event(AppId, Constants.UnsetEvent, userId, Constants.User, null, null, properties);
            var body = request.ToString(Formatting.None);
            var response = Execute(Constants.EventsResource, Method.POST, body);
            return response[Constants.EventId].Value<string>();
        }


        public async Task<string> SetUserAsync(string userId, Dictionary<string, object> properties = null)
        {
            var request = new Event(AppId, Constants.SetEvent, userId, Constants.User, null, null, properties);
            var body = request.ToString(Formatting.None);
            var response = await ExecuteAsync(Constants.EventsResource, Method.POST, body).ConfigureAwait(false);
            return response[Constants.EventId].Value<string>();
        }

        public async Task<string> UnsetUserAsync(string userId, Dictionary<string, object> properties)
        {
            if (properties == null || !properties.Any())
            {
                throw new InvalidOperationException("Properties cannot be null or empty for unset operation");
            }
            var request = new Event(AppId, Constants.UnsetEvent, userId, Constants.User, null, null, properties);
            var body = request.ToString(Formatting.None);
            var response = await ExecuteAsync(Constants.EventsResource, Method.POST, body).ConfigureAwait(false);
            return response[Constants.EventId].Value<string>();
        }

        public string SetItem(string itemId, string[] itemTypes, Dictionary<string, object> properties = null)
        {
            if (!itemTypes.Any())
            {
                throw new InvalidOperationException("Item must have at least one item type");
            }
            var jItemTypes = new JArray();
            foreach (var itemType in itemTypes)
            {
                jItemTypes.Add(itemType);
            }
            if (properties == null)
            {
                properties = new Dictionary<string, object> { { Constants.ItemTypes, jItemTypes } };
            }
            else
            {
                properties.Add(Constants.ItemTypes, jItemTypes);
            }
            var request = new Event(AppId, Constants.SetEvent, itemId, Constants.Item, null, null, properties);
            var body = request.ToString(Formatting.None);
            var response = Execute(Constants.EventsResource, Method.POST, body);
            return response[Constants.EventId].Value<string>();
        }
        
        public string UnsetItem(string itemId, Dictionary<string, object> properties)
        {
            if (properties == null || !properties.Any())
            {
                throw new InvalidOperationException("Properties cannot be null or empty for unset operation");
            }
            var request = new Event(AppId, Constants.UnsetEvent, itemId, Constants.Item, null, null, properties);
            var body = request.ToString(Formatting.None);
            var response = Execute(Constants.EventsResource, Method.POST, body);
            return response[Constants.EventId].Value<string>();
        }

        public async Task<string> SetItemAsync(string itemId, string[] itemTypes, Dictionary<string, object> properties = null)
        {
            if (!itemTypes.Any())
            {
                throw new InvalidOperationException("Item must have at least one item type");
            }
            var jItemTypes = new JArray();
            foreach (var itemType in itemTypes)
            {
                jItemTypes.Add(itemType);
            }
            if (properties == null)
            {
                properties = new Dictionary<string, object> { { Constants.ItemTypes, jItemTypes } };
            }
            else
            {
                properties.Add(Constants.ItemTypes, jItemTypes);
            }
            var request = new Event(AppId, Constants.SetEvent, itemId, Constants.Item, null, null, properties);
            var body = request.ToString(Formatting.None);
            var response = await ExecuteAsync(Constants.EventsResource, Method.POST, body).ConfigureAwait(false);
            return response[Constants.EventId].Value<string>();
        }

        public async Task<string> UnsetItemAsync(string itemId, Dictionary<string, object> properties)
        {
            if (properties == null || !properties.Any())
            {
                throw new InvalidOperationException("Properties cannot be null or empty for unset operation");
            }
            var request = new Event(AppId, Constants.UnsetEvent, itemId, Constants.Item, null, null, properties);
            var body = request.ToString(Formatting.None);
            var response = await ExecuteAsync(Constants.EventsResource, Method.POST, body).ConfigureAwait(false);
            return response[Constants.EventId].Value<string>();
        }

        public string SetActionItem(string userId, string itemId, string action, int? rating = null)
        {
            var properties = new Dictionary<string, object>();
            if (rating.HasValue)
            {
                properties.Add(Constants.Rating, rating.Value);
            }
            var request = new Event(AppId, action, userId, Constants.User, itemId, Constants.Item, properties);
            var body = request.ToString(Formatting.None);
            var response = Execute(Constants.EventsResource, Method.POST, body);
            return response[Constants.EventId].Value<string>();
        }

        public async Task<string> SetActionItemAsync(string userId, string itemId, string action, int? rating = null)
        {
            var properties = new Dictionary<string, object>();
            if (rating.HasValue)
            {
                properties.Add(Constants.Rating, rating.Value);
            }
            var request = new Event(AppId, action, userId, Constants.User, itemId, Constants.Item, properties);
            var body = request.ToString(Formatting.None);
            var response = await ExecuteAsync(Constants.EventsResource, Method.POST, body).ConfigureAwait(false);
            return response[Constants.EventId].Value<string>();
        }

        public string RateItem(string userId, string itemId, int rating)
        {
            return SetActionItem(userId, itemId, Constants.Actions.Rate, rating);
        }

        public async Task<string> RateItemAsync(string userId, string itemId, int rating)
        {
            return await SetActionItemAsync(userId, itemId, Constants.Actions.Rate, rating).ConfigureAwait(false);
        }

        public string DeleteItem(string itemId)
        {
            var request = new Event(AppId, Constants.DeleteEvent, itemId, Constants.Item, null, null, null);
            var body = request.ToString(Formatting.None);
            var response = Execute(Constants.EventsResource, Method.POST, body);
            return response[Constants.EventId].Value<string>();
        }
        
        public async Task<string> DeleteItemAsync(string itemId)
        {
            var request = new Event(AppId, Constants.DeleteEvent, itemId, Constants.Item, null, null, null);
            var body = request.ToString(Formatting.None);
            var response = await ExecuteAsync(Constants.EventsResource, Method.POST, body).ConfigureAwait(false);
            return response[Constants.EventId].Value<string>();
        }
        
        public string DeleteUser(string userId)
        {
            var request = new Event(AppId, Constants.DeleteEvent, userId, Constants.User, null, null, null);
            var body = request.ToString(Formatting.None);
            var response = Execute(Constants.EventsResource, Method.POST, body);
            return response[Constants.EventId].Value<string>();
        }

        public async Task<string> DeleteUserAsync(string userId)
        {
            var request = new Event(AppId, Constants.DeleteEvent, userId, Constants.User, null, null, null);
            var body = request.ToString(Formatting.None);
            var response = await ExecuteAsync(Constants.EventsResource, Method.POST, body).ConfigureAwait(false);
            return response[Constants.EventId].Value<string>();
        }
    }
}
