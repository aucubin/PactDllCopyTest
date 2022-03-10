using NUnit.Framework;
using PactNet;
using System.Net.Http;
using System.Net;

namespace PactDllCopyTest
{
    [TestFixture]
    class Test
    {
        private IPactBuilderV3 pact;

        [SetUp]
        public void Setup()
        {
            pact = Pact.V3("consumer", "provider", new PactConfig()).UsingNativeBackend();
        }
        [Test]
        public void IsOddTest()
        {
            pact
                .UponReceiving("A Get Request for IsOdd")
                    .WithRequest(HttpMethod.Get, "/api/IsOdd")
                    .WithQuery("value", "99")
                .WillRespond()
                    .WithStatus(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json")
                    .WithJsonBody(new
                    {
                        isOdd = false
                    });

            pact.Verify(ctx =>
            {
                var client = new Client(ctx.MockServerUri);

                IsOddObject result = client.IsOdd(99).Result;

                Assert.AreEqual(false, result.isOdd);
            });
        } 
    }
}
