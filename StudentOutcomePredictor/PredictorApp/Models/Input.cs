using Microsoft.ML.Data;

namespace PredictorApp.Models;

public record Input
{
    [LoadColumn(0)]
    public int Age { get; set; }

    [LoadColumn(1)]
    public string FieldOfStudy { get; set; }

    [LoadColumn(2)]
    public int Year { get; set; }

    [LoadColumn(3)]
    public string Subject { get; set; }

    [LoadColumn(4)]
    public float Grade { get; set; }
}
