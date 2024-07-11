namespace Infrastructure.Entities;

public class PredictedLabel
{
	public int PredictedLabelId { get; set; }

	public string Label { get; set; }

	public ICollection<Student> Students { get; set; }
}
