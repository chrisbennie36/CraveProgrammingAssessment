using Domains.Orders.QueryModels;
using System;
using Xunit;

namespace OrderingApiPerformanceTests
{
    public class OrderControllerPerformanceTests
    {
        private string baseUrl = "/api/v2/webhooksubscriptionmanager";

        private Guid subscriptionId = Guid.NewGuid();   //ToDo: Get from populated database

        /// <summary>
        /// Always run the warmup first to get more accurate performance test durations.
        /// </summary>
        [Fact]
        [Trait("TestCategory", "PerformanceTest")]
        public void _Warmup()
        {
            // Warmup
            PerformanceTestHelper.Warmup(GetOrder_ShouldTakeLessThan_250ms);
            PerformanceTestHelper.Warmup(GetOrders_ShouldTakeLessThan_500ms);
        }

        [Fact]
        [Trait("TestCategory", "PerformanceTest")]
        public void GetOrder_ShouldTakeLessThan_250ms()
        {
            PerformanceTestHelper.MeasureCallTime(GetOrder, 250);
        }

        [Fact]
        [Trait("TestCategory", "PerformanceTest")]
        public void GetOrders_ShouldTakeLessThan_500ms()
        {
            PerformanceTestHelper.MeasureCallTime(GetOrders, 500);

        }

        private void GetOrder()
        {
            GetPostPutDelete.Get<OrderQueryModel>(baseUrl + $"/{subscriptionId}");
        }

        private void GetOrders()
        {
            GetPostPutDelete.Get<OrdersQueryModel>(baseUrl);
        }

        /*private IEnumerable<Guid> AddSubscriptions(int subscriptionAmount)
        {
            var subscriptionIds = new List<Guid>();

            for (int i = 1; i <= subscriptionAmount; i++)
            {
                var addWebhookSubscriptionCommand = new AddWebhookSubscriptionCommand
                {
                    Name = "subscriptionName",
                    IsActive = true,
                    Secret = "INT TEST SECRET",
                    CustomHeaders = new List<HeaderDto>
                    {
                        new HeaderDto
                        {
                            Id = Guid.NewGuid(),
                            Key = "INT TEST KEY",
                            Value = "INT TEST VALUE"
                        }
                    },
                    Webhooks = new List<WebhookDto>
                    {
                        new WebhookDto
                        {
                            Id = Guid.NewGuid(),
                            Name = "Webhook INT TEST"
                        }
                    },
                    WebhookUrl = "http://localhost/inttest"
                };

                var subscriptionId = GetPostPutDelete.Post<AddWebhookSubscriptionCommand, Guid>(baseUrl, addWebhookSubscriptionCommand);
                subscriptionIds.Add(subscriptionId);
            }

            return subscriptionIds;
        }*/
    }
}
