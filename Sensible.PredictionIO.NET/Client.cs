using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensible.PredictionIO.NET
{
    public class Client:AbstractRequestBuilder
    {
        public Client(string apiUrl, string appKey):base(apiUrl, appKey)
        {
        }

        public UserActionRequestBuilder UserActionRequestBuilder(UserAction userAction)
        {
            var builder = new UserActionRequestBuilder(_apiUrl, _appKey, userAction);
            return builder;
        }

        public ItemRecGetTopNRequestBuilder ItemRecGetTopNRequestBuilder(RecommendationEngineRequest recommendationEngineRequest)
        {
            var builder = new ItemRecGetTopNRequestBuilder(_apiUrl, _appKey, recommendationEngineRequest);
            return builder;
        }

        public AddUserRequestBuilder AddUserRequestBuilder(User user)
        {
            var builder = new AddUserRequestBuilder(_apiUrl, _appKey, user);
            return builder;
        }

        public AddItemRequestBuilder AddItemRequestBuilder(Item item)
        {
            var builder = new AddItemRequestBuilder(_apiUrl, _appKey, item);
            return builder;
        } 
        
        public GetUserRequestBuilder GetUserRequestBuilder(string userId)
        {
            var builder = new GetUserRequestBuilder(_apiUrl, _appKey, userId);
            return builder;
        }

        public DeleteUserRequestBuilder DeleteUserRequestBuilder(string userId)
        {
            var builder = new DeleteUserRequestBuilder(_apiUrl, _appKey, userId);
            return builder;
        }

        public GetItemRequestBuilder GetItemRequestBuilder(string itemId)
        {
            var builder = new GetItemRequestBuilder(_apiUrl, _appKey, itemId);
            return builder;
        }

        public DeleteItemRequestBuilder DeleteItemRequestBuilder(string userId)
        {
            var builder = new DeleteItemRequestBuilder(_apiUrl, _appKey, userId);
            return builder;
        }
        public ItemSimGetTopNRequestBuilder ItemSimGetTopNRequestBuilder(SimilarityEngineRequest similarityEngineRequest)
        {
            var builder = new ItemSimGetTopNRequestBuilder(_apiUrl, _appKey, similarityEngineRequest);
            return builder;
        }
    }
}
