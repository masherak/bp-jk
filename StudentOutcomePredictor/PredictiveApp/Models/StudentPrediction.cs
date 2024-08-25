using Microsoft.ML.Data;

namespace PredictiveApp.Models;

public record StudentPrediction
{
	[ColumnName("PredictedLabel")] public string PredictedLabel { get; set; }
	[ColumnName("Score")] public float[] Score { get; set; }
}
