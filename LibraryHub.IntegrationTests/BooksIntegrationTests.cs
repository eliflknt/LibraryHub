using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace LibraryHub.IntegrationTests
{
    public class BooksIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public BooksIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetBooks_WithPagination_ReturnsPagedResult()
        {
            var response = await _client.GetAsync(
                "/api/Books?page=1&pageSize=5");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();

            using var json = JsonDocument.Parse(content);

            var root = json.RootElement;

            Assert.Equal(1, root.GetProperty("page").GetInt32());
            Assert.Equal(5, root.GetProperty("pageSize").GetInt32());

            var items = root.GetProperty("items");

            Assert.True(items.GetArrayLength() <= 5);

            Assert.True(root.GetProperty("totalCount").GetInt32() >= 0);
            Assert.True(root.GetProperty("totalPages").GetInt32() >= 0);
        }

        [Fact]
        public async Task GetBooks_WithSearch_ReturnsMatchingBook()
        {
            var response = await _client.GetAsync(
                "/api/Books?page=1&pageSize=10&search=Satranç");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();

            using var json = JsonDocument.Parse(content);

            var root = json.RootElement;

            var items = root.GetProperty("items");

            Assert.NotEmpty(items.EnumerateArray());

            foreach (var item in items.EnumerateArray())
            {
                var title = item
                    .GetProperty("baslik")
                    .GetString();

                Assert.Contains(
                    "Satranç",
                    title,
                    StringComparison.OrdinalIgnoreCase);
            }

            Assert.Equal(
                1,
                root.GetProperty("totalCount").GetInt32());
        }
    }
}