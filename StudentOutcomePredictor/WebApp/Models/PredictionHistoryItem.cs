namespace WebApp.Models;

public record PredictionHistoryItem
{
	public string DatasetName { get; init; }

	public string PipelineType { get; init; }

	public string TrainerType { get; init; }

	public string StudyFieldName { get; init; }

	public int StudyYear { get; init; }

	public string SubjectName { get; init; }

	public int Age { get; init; }

	public float PredictedGrade { get; init; }

	public DateTime Created { get; init; }

	public string ParsedCreated => Created.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss");
}
