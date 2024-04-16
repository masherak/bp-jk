namespace Infrastructure.Entities;

public class StudyYear
{
	public int Id { get; set; }

	public int Year { get; set; }

	public ICollection<PredictionHistory> PredictionHistories { get; set; }
}
