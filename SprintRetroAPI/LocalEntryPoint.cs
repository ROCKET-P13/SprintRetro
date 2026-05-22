namespace SprintRetroAPI;

public static class LocalEntryPoint
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

		if (string.IsNullOrEmpty(connectionString))
		{
			throw new InvalidOperationException("Connection string is not set");
		}

		builder.Services.AddApplication(connectionString);

		var app = builder.Build();
		app.MapControllers();
		app.Run();
	}
}