using System.ComponentModel.DataAnnotations;

namespace PredictiveApp.Models;

public record StudentDataRequest
{
	public string MaritalStatus { get; set; }

	public string ApplicationMode { get; set; }

	public float ApplicationOrder { get; set; }

	public string Course { get; set; }

	public string Attendance { get; set; }

	public string PreviousQualification { get; set; }

	public string Nationality { get; set; }

	public string MothersQualification { get; set; }

	public string FathersQualification { get; set; }

	public string MothersOccupation { get; set; }

	public string FathersOccupation { get; set; }

	public string Displaced { get; set; }

	public string EducationalSpecialNeeds { get; set; }

	public string Debtor { get; set; }

	public string TuitionFeesUpToDate { get; set; }

	public string Gender { get; set; }

	public string ScholarshipHolder { get; set; }

	public float AgeAtEnrollment { get; set; }

	public string International { get; set; }

	public float CurricularUnits1StSemCredited { get; set; }

	public float CurricularUnits1StSemEnrolled { get; set; }

	public float CurricularUnits1StSemEvaluations { get; set; }

	public float CurricularUnits1StSemApproved { get; set; }

	public float CurricularUnits1StSemGrade { get; set; }

	public float CurricularUnits1StSemWithoutEvaluations { get; set; }

	public float CurricularUnits2NdSemCredited { get; set; }

	public float CurricularUnits2NdSemEnrolled { get; set; }

	public float CurricularUnits2NdSemEvaluations { get; set; }

	public float CurricularUnits2NdSemApproved { get; set; }

	public float CurricularUnits2NdSemGrade { get; set; }

	public float CurricularUnits2NdSemWithoutEvaluations { get; set; }

	public float UnemploymentRate { get; set; }

	public float InflationRate { get; set; }

	public float Gdp { get; set; }
}
