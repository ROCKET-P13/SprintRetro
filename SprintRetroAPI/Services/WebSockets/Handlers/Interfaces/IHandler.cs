using System.Text.Json;

namespace SprintRetroAPI.Services.WebSockets.Handlers.Interfaces;

public interface IHandler
{
	Task Handle (string connectionId, JsonElement payload);
}