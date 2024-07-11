using Microsoft.ML.Data;

namespace PredictiveApp.Models;

public record StudentPrediction
{
	[ColumnName("Label")] public string Label { get; set; }
	[ColumnName("Score")] public float[] Score { get; set; }
}
