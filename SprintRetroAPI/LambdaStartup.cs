namespace SprintRetroAPI;

public class LambdaStartup(IConfiguration configuration)
{
	private readonly IConfiguration _configuration = configuration;

	public void ConfigureServices(IServiceCollection services)
	{
		var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

		if (string.IsNullOrEmpty(connectionString))
		{
			throw new Exception("Connection string not found");
		}

		services.AddApplication(
			connectionString
			?? throw new InvalidOperationException("Missing connection string")
		);
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		app.UseRouting();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}
}