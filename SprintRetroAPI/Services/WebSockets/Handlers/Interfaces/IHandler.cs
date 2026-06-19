using System.Text.Json;

namespace SprintRetroAPI.Services.WebSockets.Handlers.Interfaces;

public interface IHandler
{
	Task<object?> Handle (string connectionId, JsonElement payload);
}