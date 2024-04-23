using Microsoft.ML.Data;

namespace PredictorApp.Models;

public record PredictionResult
{
	public float PredictedGrade { get; init; }

	public ICollection<GradePredictionProbability> GradePredictionProbabilities { get; init; }

	public MulticlassClassificationMetrics Metrics { get; init; }
}
