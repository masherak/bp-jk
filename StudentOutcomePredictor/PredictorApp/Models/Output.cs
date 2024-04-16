using Microsoft.ML.Data;

namespace PredictorApp.Models;

public record Output
{
	[ColumnName("Score")]
	public float[] PredictedGrades { get; set; }
}
