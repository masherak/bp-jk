using Microsoft.ML;
using PredictorApp;
using PredictorApp.Models;

namespace WebApp.Services;

public class PredictionService
{
	private ITransformer? _transformer;
	private readonly MLContext _context = new();

	public async Task TrainAsync()
	{
		_transformer = await Predictor.TrainAsync();
	}

	public float PredictGrade(int age, string fieldOfStudy, int year, string subject)
	{
		if (_transformer == null)
		{
			throw new InvalidOperationException("Transformer is null, call method 'TrainAsync' first");
		}

		var predictionEngine = _context.Model.CreatePredictionEngine<Input, Output>(_transformer);

		var input = new Input
		{
			Age = age,
			FieldOfStudy = fieldOfStudy,
			Year = year,
			Subject = subject
		};

		return predictionEngine.Predict(input).PredictedGrade;
	}
}
