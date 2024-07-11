namespace WebApp.Models;

public record StudentPredictionItem
{
	public int StudentId { get; init; }

	public string PredictedLabel { get; init; }
}
