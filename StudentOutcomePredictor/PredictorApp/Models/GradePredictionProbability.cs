namespace PredictorApp.Models;

public record GradePredictionProbability
{
	public float Grade { get; init; }

	public float Probability { get; init; }
}
