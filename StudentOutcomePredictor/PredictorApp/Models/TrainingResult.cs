using Microsoft.ML;
using Microsoft.ML.Data;

namespace PredictorApp.Models;

public record TrainingResult
{
	public ITransformer Transformer { get; init; }

	public MulticlassClassificationMetrics Metrics { get; init; }
}
