namespace Infrastructure.Entities;

public class Course
{
	public int CourseId { get; set; }
	public string CourseName { get; set; }

	public ICollection<Student> Students { get; set; }
}

