namespace Infrastructure.Entities;

public class Subject
{
	public int Id { get; set; }

	public string Name { get; set; }

	public int StudyFieldId { get; set; }

	public StudyField StudyField { get; set; }

	public ICollection<PredictionHistory> PredictionHistories { get; set; }
}
