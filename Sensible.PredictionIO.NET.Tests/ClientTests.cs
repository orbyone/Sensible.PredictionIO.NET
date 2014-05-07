using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sensible.PredictionIO.NET.Tests
{
    [TestClass]
    public class ClientTests
    {
        private const string ApiUrl = "http://127.0.0.1:8000";
        private const string AppKey = "yourappkey";

        [TestMethod]
        public void TestUserAction()
        {
            var client = new Client(ApiUrl, AppKey);
            var builder = client.UserActionRequestBuilder(
                new UserAction
                {
                    UserId = "1",
                    ItemId = "1",
                    Action = UserAction.Actions.View
                });
            var request = builder.Build();
            var response = request.Execute(builder.RestRequest);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        [TestMethod]
        public void TestItemRecommendation()
        {
            var client = new Client(ApiUrl, AppKey);
            var builder = client.ItemRecGetTopNRequestBuilder(
                new RecommendationEngineRequest
                {
                    UserId = "1",
                    Engine = "itemrec",
                    NumberOfItems = 10,
                });
            var request = builder.Build();
            var response = request.Execute<EngineResponse>(builder.RestRequest);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestAddUser()
        {
            var client = new Client(ApiUrl, AppKey);
            var builder = client.AddUserRequestBuilder(new User { UserId = "13",Coordinates = new List<double> {23, 37}});
            var request = builder.Build();
            var response = request.Execute(builder.RestRequest);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        [TestMethod]
        public void TestAddItem()
        {
            var client = new Client(ApiUrl, AppKey);
            var builder = client.AddItemRequestBuilder(
                new Item
                {
                    ItemId = "52",
                    Coordinates = new List<double> { 23,37},
                    Inactive = true,
                    ItemTypes = new List<string> { "1", "2"},
                    Price = 10,
                    Profit = 20
                }
                );
            var request = builder.Build();
            var response = request.Execute(builder.RestRequest);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        [TestMethod]
        public void TestGetUser()
        {
            var client = new Client(ApiUrl, AppKey);
            var builder = client.GetUserRequestBuilder("13");
            var request = builder.Build();
            var response = request.Execute<User>(builder.RestRequest);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            var client = new Client(ApiUrl, AppKey);
            var builder = client.DeleteUserRequestBuilder("13");
            var request = builder.Build();
            var response = request.Execute(builder.RestRequest);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestGetItem()
        {
            var client = new Client(ApiUrl, AppKey);
            var builder = client.GetItemRequestBuilder("52");
            var request = builder.Build();
            var response = request.Execute<Item>(builder.RestRequest);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestDeleteItem()
        {
            var client = new Client(ApiUrl, AppKey);
            var builder = client.DeleteItemRequestBuilder("52");
            var request = builder.Build();
            var response = request.Execute(builder.RestRequest);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestItemSimilarity()
        {
            var client = new Client(ApiUrl, AppKey);
            var builder = client.ItemSimGetTopNRequestBuilder(
                new SimilarityEngineRequest
                {
                    ItemId = "1",
                    Engine = "itemsim",
                    NumberOfItems = 10,
                });
            var request = builder.Build();
            var response = request.Execute<EngineResponse>(builder.RestRequest);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
