using Alba;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Client.Tests;

public class ApiClientTests
{
    [Fact]
    public async Task Can_Get_Todos()
    {
        await using var host = await AlbaHost.For<Program>();
        var serverBaseAddress = host.GetTestClient().BaseAddress;

        var services = new ServiceCollection();
        services.AddRefitClient<IApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = serverBaseAddress)
            .ConfigurePrimaryHttpMessageHandler(host.Server.CreateHandler);

        var provider = services.BuildServiceProvider();
        var sut = provider.GetRequiredService<IApiClient>();

        var results = await sut.GetTodos();
        Assert.NotNull(results);
        Assert.NotEmpty(results);
    }
}