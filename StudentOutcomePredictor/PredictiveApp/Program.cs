using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

app.MapPost("/predict", async ([FromBody] StudentDataRequest request, IValidator<StudentDataRequest> validator, IWebHostEnvironment environment, PredictionService predictionService) =>
	{
		var validationResult = await validator.ValidateAsync(request);

		if (!validationResult.IsValid)
		{
			var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

			return Results.BadRequest(errors);
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
	.WithOpenApi();


app.Run();
