namespace Infrastructure.Entities;

public class Qualification
{
	public int QualificationId { get; set; }
	public string QualificationName { get; set; }

	public ICollection<Student> StudentsWithPreviousQualification { get; set; }
	public ICollection<Student> StudentsWithMotherQualification { get; set; }
	public ICollection<Student> StudentsWithFatherQualification { get; set; }
}
