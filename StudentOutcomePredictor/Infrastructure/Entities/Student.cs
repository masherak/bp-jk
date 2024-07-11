namespace Infrastructure.Entities;

public class Student
{
	public int StudentId { get; set; }
	public int MaritalStatus { get; set; }
	public int ApplicationMode { get; set; }
	public int ApplicationOrder { get; set; }
	public int CourseId { get; set; }
	public int DaytimeEveningAttendance { get; set; }
	public int PreviousQualificationId { get; set; }
	public int Nationality { get; set; }
	public int MotherQualificationId { get; set; }
	public int FatherQualificationId { get; set; }
	public int MotherOccupation { get; set; }
	public int FatherOccupation { get; set; }

	public PredictedLabel PredictedLabel { get; set; }
	public Course Course { get; set; }
	public Qualification PreviousQualification { get; set; }
	public Qualification MotherQualification { get; set; }
	public Qualification FatherQualification { get; set; }
	public ICollection<StudentPerformance> StudentPerformances { get; set; }
	public ICollection<EconomicIndicator> EconomicIndicators { get; set; }
}
