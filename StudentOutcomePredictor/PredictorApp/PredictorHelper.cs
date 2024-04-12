namespace PredictorApp;

public static class PredictorHelper
{
	public static readonly string DataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "student_data.csv");
	public const string EncodedFieldOfStudyOutputColumnName = "EncodedFieldOfStudy";
	public const string FieldOfStudyInputColumnName = "FieldOfStudy";
	public const string EncodedSubjectOutputColumnName = "EncodedSubject";
	public const string SubjectInputColumnName = "Subject";
	public const string StudentIdInputOutputColumnName = "StudentId";
	public const string AgeInputOutputColumnName = "Age";
	public const string YearInputOutputColumnName = "Year";
	public const string FeaturesColumnName = "Features";
	public const string GradeColumnName = "Grade";
}
