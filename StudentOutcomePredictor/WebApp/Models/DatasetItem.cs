namespace WebApp.Models;

public record DatasetItem
{
	public string Name { get; init; }

	public DateTime Created { get; init; }

	public string ParsedDateTime => Created.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss");
}
