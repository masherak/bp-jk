namespace Infrastructure.Entities;

public class StudentPerformance
{
	public int PerformanceId { get; set; }
	public int StudentId { get; set; }
	public int CurricularUnits1StSemCredited { get; set; }
	public int CurricularUnits1StSemEnrolled { get; set; }
	public int CurricularUnits1StSemEvaluations { get; set; }
	public int CurricularUnits1StSemApproved { get; set; }
	public float CurricularUnits1StSemGrade { get; set; }
	public int CurricularUnits1StSemWithoutEvaluations { get; set; }
	public int CurricularUnits2NdSemCredited { get; set; }
	public int CurricularUnits2NdSemEnrolled { get; set; }
	public int CurricularUnits2NdSemEvaluations { get; set; }
	public int CurricularUnits2NdSemApproved { get; set; }
	public float CurricularUnits2NdSemGrade { get; set; }
	public int CurricularUnits2NdSemWithoutEvaluations { get; set; }

	public Student Student { get; set; }
}
