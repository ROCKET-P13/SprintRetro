using Amazon.Lambda.AspNetCoreServer;

namespace SprintRetroAPI;

public class LambdaEntryPoint : APIGatewayHttpApiV2ProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        builder.UseStartup<LambdaStartup>();
    }
}