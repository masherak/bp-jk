using Microsoft.ML;
using Microsoft.ML.Data;
using PredictorApp.Models;

namespace PredictorApp.Services;

public class PredictionService
{
	private TrainingResult? _trainingResult;
	private readonly MLContext _context = new();

	public bool IsTrained()
	{
		return _trainingResult != null;
	}

	public async Task<MulticlassClassificationMetrics> TrainAsync()
	{
		_trainingResult = await Predictor.TrainAsync();

		return _trainingResult.Metrics;
	}

	public PredictionResult PredictGrade(int age, string fieldOfStudy, int year, string subject)
	{
		if (_trainingResult == null)
		{
			throw new InvalidOperationException("TrainingResult is null, call method 'TrainAsync' first");
		}

		var predictionEngine = _context.Model.CreatePredictionEngine<Input, Output>(_trainingResult.Transformer);

		var input = new Input
		{
			Age = age,
			FieldOfStudy = fieldOfStudy,
			Year = year,
			Subject = subject
		};

		var output = predictionEngine.Predict(input);

		var maxProbability = output.PredictedGrades.Max();

		var predictedClassIndex = Array.IndexOf(output.PredictedGrades, maxProbability);

		var predictedGrade = predictedClassIndex + 1;

		return new PredictionResult
		{
			PredictedGrade = predictedGrade,
			Metrics = _trainingResult.Metrics
		};
	}

	public MulticlassClassificationMetrics? GetMetrics()
	{
		return _trainingResult?.Metrics;
	}
}
