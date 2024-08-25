using Microsoft.ML.Data;

namespace PredictiveApp.Models;

public record StudentData
{
    [LoadColumn(0)] public string MaritalStatus { get; set; }
    [LoadColumn(1)] public string ApplicationMode { get; set; }
    [LoadColumn(2)] public float ApplicationOrder { get; set; }
    [LoadColumn(3)] public string Course { get; set; }
    [LoadColumn(4)] public string Attendance { get; set; }
    [LoadColumn(5)] public string PreviousQualification { get; set; }
    [LoadColumn(6)] public string Nationality { get; set; }
    [LoadColumn(7)] public string MothersQualification { get; set; }
    [LoadColumn(8)] public string FathersQualification { get; set; }
    [LoadColumn(9)] public string MothersOccupation { get; set; }
    [LoadColumn(10)] public string FathersOccupation { get; set; }
    [LoadColumn(11)] public string Displaced { get; set; }
    [LoadColumn(12)] public string EducationalSpecialNeeds { get; set; }
    [LoadColumn(13)] public string Debtor { get; set; }
    [LoadColumn(14)] public string TuitionFeesUpToDate { get; set; }
    [LoadColumn(15)] public string Gender { get; set; }
    [LoadColumn(16)] public string ScholarshipHolder { get; set; }
    [LoadColumn(17)] public float AgeAtEnrollment { get; set; }
    [LoadColumn(18)] public string International { get; set; }
    [LoadColumn(19)] public float CurricularUnits1StSemCredited { get; set; }
    [LoadColumn(20)] public float CurricularUnits1StSemEnrolled { get; set; }
    [LoadColumn(21)] public float CurricularUnits1StSemEvaluations { get; set; }
    [LoadColumn(22)] public float CurricularUnits1StSemApproved { get; set; }
    [LoadColumn(23)] public float CurricularUnits1StSemGrade { get; set; }
    [LoadColumn(24)] public float CurricularUnits1StSemWithoutEvaluations { get; set; }
    [LoadColumn(25)] public float CurricularUnits2NdSemCredited { get; set; }
    [LoadColumn(26)] public float CurricularUnits2NdSemEnrolled { get; set; }
    [LoadColumn(27)] public float CurricularUnits2NdSemEvaluations { get; set; }
    [LoadColumn(28)] public float CurricularUnits2NdSemApproved { get; set; }
    [LoadColumn(29)] public float CurricularUnits2NdSemGrade { get; set; }
    [LoadColumn(30)] public float CurricularUnits2NdSemWithoutEvaluations { get; set; }
    [LoadColumn(31)] public float UnemploymentRate { get; set; }
    [LoadColumn(32)] public float InflationRate { get; set; }
    [LoadColumn(33)] public float Gdp { get; set; }
    [LoadColumn(34)] public string Label { get; set; }
}
