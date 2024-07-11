using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using PredictiveApp.Models;
using PredictiveApp.Services;
using PredictiveApp.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<StudentDataRequestValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<PredictionService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("/predict", async (
		[FromBody] StudentDataRequest request,
		IValidator<StudentDataRequest> validator,
		IWebHostEnvironment environment,
		PredictionService predictionService) =>
	{
		var validationResult = await validator.ValidateAsync(request);

		if (!validationResult.IsValid)
		{
			return Results.BadRequest(validationResult.Errors);
		}

		if (!predictionService.IsTrained())
		{
			var datasetPath = Path.Combine(environment.ContentRootPath, "dataset.csv");

			var dataset = File.ReadAllBytes(datasetPath);

			await predictionService.TrainAsync(dataset, PipelineTypeEnum.Default, TrainerTypeEnum.OneVersusAllWithFastForest);
		}

		var response = predictionService.Predict(request);

		return Results.Json(response);
	})
	.WithOpenApi()
	.Produces<PredictionResult>()
	.Produces<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest);


app.Run();
