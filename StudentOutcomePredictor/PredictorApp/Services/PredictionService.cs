using Infrastructure.Enums;
using Microsoft.ML;
using Microsoft.ML.Data;
using PredictorApp.Models;

namespace PredictorApp.Services;

public class PredictionService
{
	private TrainingResult? _trainingResult;
	private string? _datasetName;
	private PipelineTypeEnum? _pipelineType;
	private TrainerTypeEnum? _trainerType;

	private readonly MLContext _context = new();

	public string? GetDatasetName => _datasetName;

	public PipelineTypeEnum? GetPipelineType => _pipelineType;

	public TrainerTypeEnum? GetTrainerType => _trainerType;

	public bool IsTrained()
	{
		return _trainingResult != null;
	}

	public void Reset()
	{
		_trainingResult = null;
		_datasetName = null;
		_pipelineType = null;
		_trainerType = null;
	}

	public async Task<MulticlassClassificationMetrics> TrainAsync(byte[] dataset, string datasetName, PipelineTypeEnum pipelineType, TrainerTypeEnum trainerType)
	{
		_datasetName = datasetName;
		_pipelineType = pipelineType;
		_trainerType = trainerType;

		_trainingResult = await Task.Run(() => Predictor.Train(dataset, pipelineType, trainerType));

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
