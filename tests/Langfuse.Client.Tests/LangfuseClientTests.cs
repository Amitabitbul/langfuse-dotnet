using System.Net;
using System.Text.Json;
using Langfuse.Client;
using Langfuse.Core;
using Moq;
using Moq.Protected;
using Xunit;

namespace Langfuse.Client.Tests;

public class LangfuseClientTests
{
    [Fact]
    public async Task GetPromptAsync_WithSpacesInName_EncodesSpacesAsPercent20()
    {
        // Arrange
        var promptName = "test prompt";
        var expectedPath = "/api/public/v2/prompts/test%20prompt";
        string? actualRequestPath = null;

        var mockResponse = new
        {
            id = "test-id",
            name = promptName,
            version = 1,
            type = "text",
            prompt = "Test content",
            labels = new string[] { "production" },
            tags = new string[0],
            config = new { },
            createdAt = DateTime.UtcNow.ToString("o"),
            updatedAt = DateTime.UtcNow.ToString("o")
        };

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
            {
                actualRequestPath = request.RequestUri?.PathAndQuery;
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(mockResponse))
                };
            });

        var httpClient = new HttpClient(mockHandler.Object)
        {
            BaseAddress = new Uri("https://cloud.langfuse.com")
        };

        var options = new LangfuseClientOptions
        {
            BaseUrl = "https://cloud.langfuse.com",
            PublicKey = "test-key",
            SecretKey = "test-secret"
        };

        using var client = new LangfuseClient(options, httpClient);

        // Act
        var result = await client.GetPromptAsync(promptName);

        // Assert
        Assert.NotNull(actualRequestPath);
        Assert.Equal(expectedPath, actualRequestPath);
        Assert.Equal(promptName, result.Name);
    }

    [Fact]
    public async Task GetPromptAsync_WithoutSpacesInName_EncodesCorrectly()
    {
        // Arrange
        var promptName = "test-prompt";
        var expectedPath = "/api/public/v2/prompts/test-prompt";
        string? actualRequestPath = null;

        var mockResponse = new
        {
            id = "test-id",
            name = promptName,
            version = 1,
            type = "text",
            prompt = "Test content",
            labels = new string[] { "production" },
            tags = new string[0],
            config = new { },
            createdAt = DateTime.UtcNow.ToString("o"),
            updatedAt = DateTime.UtcNow.ToString("o")
        };

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
            {
                actualRequestPath = request.RequestUri?.PathAndQuery;
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(mockResponse))
                };
            });

        var httpClient = new HttpClient(mockHandler.Object)
        {
            BaseAddress = new Uri("https://cloud.langfuse.com")
        };

        var options = new LangfuseClientOptions
        {
            BaseUrl = "https://cloud.langfuse.com",
            PublicKey = "test-key",
            SecretKey = "test-secret"
        };

        using var client = new LangfuseClient(options, httpClient);

        // Act
        var result = await client.GetPromptAsync(promptName);

        // Assert
        Assert.NotNull(actualRequestPath);
        Assert.Equal(expectedPath, actualRequestPath);
        Assert.Equal(promptName, result.Name);
    }

    [Fact]
    public async Task GetPromptAsync_WithSpacesInNameAndVersion_EncodesCorrectly()
    {
        // Arrange
        var promptName = "my test prompt";
        var version = 2;
        var expectedPath = "/api/public/v2/prompts/my%20test%20prompt?version=2";
        string? actualRequestPath = null;

        var mockResponse = new
        {
            id = "test-id",
            name = promptName,
            version = version,
            type = "text",
            prompt = "Test content",
            labels = new string[] { "production" },
            tags = new string[0],
            config = new { },
            createdAt = DateTime.UtcNow.ToString("o"),
            updatedAt = DateTime.UtcNow.ToString("o")
        };

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
            {
                actualRequestPath = request.RequestUri?.PathAndQuery;
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(mockResponse))
                };
            });

        var httpClient = new HttpClient(mockHandler.Object)
        {
            BaseAddress = new Uri("https://cloud.langfuse.com")
        };

        var options = new LangfuseClientOptions
        {
            BaseUrl = "https://cloud.langfuse.com",
            PublicKey = "test-key",
            SecretKey = "test-secret"
        };

        using var client = new LangfuseClient(options, httpClient);

        // Act
        var result = await client.GetPromptAsync(promptName, version: version);

        // Assert
        Assert.NotNull(actualRequestPath);
        Assert.Equal(expectedPath, actualRequestPath);
        Assert.Equal(promptName, result.Name);
        Assert.Equal(version, result.Version);
    }

    [Fact]
    public async Task GetPromptAsync_WithSpacesInLabel_EncodesCorrectly()
    {
        // Arrange
        var promptName = "test prompt";
        var label = "my label";
        var expectedPath = "/api/public/v2/prompts/test%20prompt?label=my%20label";
        string? actualRequestPath = null;

        var mockResponse = new
        {
            id = "test-id",
            name = promptName,
            version = 1,
            type = "text",
            prompt = "Test content",
            labels = new string[] { label },
            tags = new string[0],
            config = new { },
            createdAt = DateTime.UtcNow.ToString("o"),
            updatedAt = DateTime.UtcNow.ToString("o")
        };

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
            {
                actualRequestPath = request.RequestUri?.PathAndQuery;
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(mockResponse))
                };
            });

        var httpClient = new HttpClient(mockHandler.Object)
        {
            BaseAddress = new Uri("https://cloud.langfuse.com")
        };

        var options = new LangfuseClientOptions
        {
            BaseUrl = "https://cloud.langfuse.com",
            PublicKey = "test-key",
            SecretKey = "test-secret"
        };

        using var client = new LangfuseClient(options, httpClient);

        // Act
        var result = await client.GetPromptAsync(promptName, label: label);

        // Assert
        Assert.NotNull(actualRequestPath);
        Assert.Equal(expectedPath, actualRequestPath);
        Assert.Equal(promptName, result.Name);
    }

    [Fact]
    public async Task GetPromptAsync_WithSpecialCharactersInName_EncodesCorrectly()
    {
        // Arrange
        var promptName = "test/prompt&name=value";
        // Uri.EscapeDataString encodes special characters properly
        var expectedPath = "/api/public/v2/prompts/test%2Fprompt%26name%3Dvalue";
        string? actualRequestPath = null;

        var mockResponse = new
        {
            id = "test-id",
            name = promptName,
            version = 1,
            type = "text",
            prompt = "Test content",
            labels = new string[] { "production" },
            tags = new string[0],
            config = new { },
            createdAt = DateTime.UtcNow.ToString("o"),
            updatedAt = DateTime.UtcNow.ToString("o")
        };

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
            {
                actualRequestPath = request.RequestUri?.PathAndQuery;
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(mockResponse))
                };
            });

        var httpClient = new HttpClient(mockHandler.Object)
        {
            BaseAddress = new Uri("https://cloud.langfuse.com")
        };

        var options = new LangfuseClientOptions
        {
            BaseUrl = "https://cloud.langfuse.com",
            PublicKey = "test-key",
            SecretKey = "test-secret"
        };

        using var client = new LangfuseClient(options, httpClient);

        // Act
        var result = await client.GetPromptAsync(promptName);

        // Assert
        Assert.NotNull(actualRequestPath);
        Assert.Equal(expectedPath, actualRequestPath);
        Assert.Equal(promptName, result.Name);
    }
}
