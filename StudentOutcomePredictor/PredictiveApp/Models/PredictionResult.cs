using Microsoft.ML.Data;

namespace PredictiveApp.Models;

public record PredictionResult
{
	public string PredictedLabel { get; set; }

	public List<PredictionProbability> PredictionProbabilities { get; set; }

	public MulticlassClassificationMetrics Metrics { get; set; }
}

public class PredictionProbability
{
	public string Label { get; set; }

	public float Probability { get; set; }
}
