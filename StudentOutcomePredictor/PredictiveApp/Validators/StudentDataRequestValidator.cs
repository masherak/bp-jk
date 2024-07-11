using FluentValidation;
using Infrastructure.Models;
using PredictiveApp.Models;

namespace PredictiveApp.Validators;

public class StudentDataRequestValidator : AbstractValidator<StudentDataRequest>
{
	public StudentDataRequestValidator()
	{
		RuleFor(x => x.Nationality).NotEmpty();
		RuleFor(x => x.Gender).NotEmpty();
		RuleFor(x => x.ScholarshipHolder).NotEmpty();
		RuleFor(x => x.AgeAtEnrollment).GreaterThan(0);
		RuleFor(x => x.International).NotEmpty();
		RuleFor(x => x.CurricularUnits1StSemCredited).GreaterThanOrEqualTo(0);
		RuleFor(x => x.CurricularUnits1StSemEnrolled).GreaterThanOrEqualTo(0);
		RuleFor(x => x.CurricularUnits1StSemEvaluations).GreaterThanOrEqualTo(0);
		RuleFor(x => x.CurricularUnits1StSemApproved).GreaterThanOrEqualTo(0);
		RuleFor(x => x.CurricularUnits1StSemGrade).GreaterThanOrEqualTo(0);
		RuleFor(x => x.CurricularUnits1StSemWithoutEvaluations).GreaterThanOrEqualTo(0);
	}
}
