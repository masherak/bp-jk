namespace Infrastructure.Entities;

public class StudyField
{
	public int Id { get; set; }

	public string Name { get; set; }

	public ICollection<Subject> Subjects { get; set; }
}
