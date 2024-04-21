using Infrastructure.Enums;

namespace Infrastructure.Entities;

public class PredictionHistory
{
	public int Id { get; set; }

	public int DatasetFileId { get; set; }

	public DatasetFile DatasetFile { get; set; }

	public PipelineTypeEnum PipelineTypeId { get; set; }

	public PipelineType PipelineType { get; set; }

	public TrainerTypeEnum TrainerTypeId { get; set; }

	public TrainerType TrainerType { get; set; }

	public int StudyFieldId { get; set; }

	public StudyField StudyField { get; set; }

	public int YearId { get; set; }

	public StudyYear Year { get; set; }

	public int SubjectId { get; set; }

	public Subject Subject { get; set; }

	public float PredictedGrade { get; set; }

	public DateTime Created { get; set; }
}
