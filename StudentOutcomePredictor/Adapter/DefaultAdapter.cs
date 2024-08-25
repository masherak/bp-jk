using System.Net.Http.Json;
using Adapter.Interfaces;
using Adapter.Responses;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Adapter;

public class DefaultAdapter(
	ApplicationDbContext context,
	IHttpClientFactory httpClientFactory,
	ILogger<DefaultAdapter> logger) : IAdapter
{
	private readonly Uri _predictiveBaseAppUrl = new("https://app-prediction.azurewebsites.net");
	private const string PredictiveEndpointName = "predict";

	public async Task ProcessAsync()
	{
		var students = await context.Students
			.Where(_ => _.StudentPerformances.Count != 0 && _.EconomicIndicators.Count != 0)
            .Select(student => new {
	            Entity = student,
	            StudentDataRequest = new StudentDataRequest
	            {
	                MaritalStatus = student.MaritalStatus.ToString(),
	                ApplicationMode = student.ApplicationMode.ToString(),
	                ApplicationOrder = student.ApplicationOrder,
	                Course = student.Course.CourseName,
	                Attendance = student.DaytimeEveningAttendance == 1 ? "Daytime" : "Evening",
	                PreviousQualification = student.PreviousQualification.QualificationName,
	                Nationality = student.Nationality.ToString(),
	                MothersQualification = student.MotherQualification.QualificationName,
	                FathersQualification = student.FatherQualification.QualificationName,
	                MothersOccupation = student.MotherOccupation.ToString(),
	                FathersOccupation = student.FatherOccupation.ToString(),
	                CurricularUnits1StSemCredited = student.StudentPerformances.First().CurricularUnits1StSemCredited,
	                CurricularUnits1StSemEnrolled = student.StudentPerformances.First().CurricularUnits1StSemEnrolled,
	                CurricularUnits1StSemEvaluations = student.StudentPerformances.First().CurricularUnits1StSemEvaluations,
	                CurricularUnits1StSemApproved = student.StudentPerformances.First().CurricularUnits1StSemApproved,
	                CurricularUnits1StSemGrade = student.StudentPerformances.First().CurricularUnits1StSemGrade,
	                CurricularUnits1StSemWithoutEvaluations = student.StudentPerformances.First().CurricularUnits1StSemWithoutEvaluations,
	                CurricularUnits2NdSemCredited = student.StudentPerformances.First().CurricularUnits2NdSemCredited,
	                CurricularUnits2NdSemEnrolled = student.StudentPerformances.First().CurricularUnits2NdSemEnrolled,
	                CurricularUnits2NdSemEvaluations = student.StudentPerformances.First().CurricularUnits2NdSemEvaluations,
	                CurricularUnits2NdSemApproved = student.StudentPerformances.First().CurricularUnits2NdSemApproved,
	                CurricularUnits2NdSemGrade = student.StudentPerformances.First().CurricularUnits2NdSemGrade,
	                CurricularUnits2NdSemWithoutEvaluations = student.StudentPerformances.First().CurricularUnits2NdSemWithoutEvaluations,
	                UnemploymentRate = student.EconomicIndicators.First().UnemploymentRate,
	                InflationRate = student.EconomicIndicators.First().InflationRate,
	                Gdp = student.EconomicIndicators.First().Gdp
	            }
            })
            .ToListAsync();

		var httpClient = httpClientFactory.CreateClient();

		httpClient.BaseAddress = _predictiveBaseAppUrl;

		foreach (var student in students)
		{
			try
			{
				var response = await httpClient.PostAsJsonAsync(PredictiveEndpointName, student.StudentDataRequest);

				response.EnsureSuccessStatusCode();

				var predictedLabelResponse = await response.Content.ReadFromJsonAsync<PredictedLabelResponse>()
				                             ?? throw new InvalidOperationException("Response is not valid");

				var predictedLabel = new PredictedLabel
				{
					Students = new[] { student.Entity },
					Label = predictedLabelResponse.PredictedLabel
				};

				await context.PredictedLabels.AddAsync(predictedLabel);
			}
			catch (Exception ex)
			{
				logger.LogCritical(ex, "Error during prediction of student id {StudentId}", student.Entity.StudentId);
			}
		}

		await context.SaveChangesAsync();
	}
}
