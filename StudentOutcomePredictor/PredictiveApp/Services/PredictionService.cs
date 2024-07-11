using Infrastructure.Enums;
using Infrastructure.Models;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using PredictiveApp.Models;
using PredictiveApp.Sources;

namespace PredictiveApp.Services;

public class PredictionService
{
	private TrainingResult? _trainingResult;

	private readonly MLContext _context = new();

	public bool IsTrained()
	{
		return _trainingResult != null;
	}

	public void Reset()
	{
		_trainingResult = null;
	}

	public async Task<MulticlassClassificationMetrics> TrainAsync(byte[] dataset, PipelineTypeEnum pipelineType, TrainerTypeEnum trainerType)
	{
		_trainingResult = await Task.Run(() => Train(dataset, pipelineType, trainerType));

		return _trainingResult.Metrics;
	}

	public PredictionResult Predict(StudentDataRequest data)
	{
		return Predict(
			data.MaritalStatus,
			data.ApplicationMode,
			data.ApplicationOrder,
			data.Course,
			data.Attendance,
			data.PreviousQualification,
			data.Nationality,
			data.MothersQualification,
			data.FathersQualification,
			data.MothersOccupation,
			data.FathersOccupation,
			data.Displaced,
			data.EducationalSpecialNeeds,
			data.Debtor,
			data.TuitionFeesUpToDate,
			data.Gender,
			data.ScholarshipHolder,
			data.AgeAtEnrollment,
			data.International,
			data.CurricularUnits1StSemCredited,
			data.CurricularUnits1StSemEnrolled,
			data.CurricularUnits1StSemEvaluations,
			data.CurricularUnits1StSemApproved,
			data.CurricularUnits1StSemGrade,
			data.CurricularUnits1StSemWithoutEvaluations,
			data.CurricularUnits2NdSemCredited,
			data.CurricularUnits2NdSemEnrolled,
			data.CurricularUnits2NdSemEvaluations,
			data.CurricularUnits2NdSemApproved,
			data.CurricularUnits2NdSemGrade,
			data.CurricularUnits2NdSemWithoutEvaluations,
			data.UnemploymentRate,
			data.InflationRate,
			data.Gdp);
	}

	public PredictionResult Predict(
	    string maritalStatus,
	    string applicationMode,
	    float applicationOrder,
	    string course,
	    string attendance,
	    string previousQualification,
	    string nationality,
	    string mothersQualification,
	    string fathersQualification,
	    string mothersOccupation,
	    string fathersOccupation,
	    string displaced,
	    string educationalSpecialNeeds,
	    string debtor,
	    string tuitionFeesUpToDate,
	    string gender,
	    string scholarshipHolder,
	    float ageAtEnrollment,
	    string international,
	    float curricularUnits1StSemCredited,
	    float curricularUnits1StSemEnrolled,
	    float curricularUnits1StSemEvaluations,
	    float curricularUnits1StSemApproved,
	    float curricularUnits1StSemGrade,
	    float curricularUnits1StSemWithoutEvaluations,
	    float curricularUnits2NdSemCredited,
	    float curricularUnits2NdSemEnrolled,
	    float curricularUnits2NdSemEvaluations,
	    float curricularUnits2NdSemApproved,
	    float curricularUnits2NdSemGrade,
	    float curricularUnits2NdSemWithoutEvaluations,
	    float unemploymentRate,
	    float inflationRate,
	    float gdp)
{
    if (_trainingResult == null)
    {
        throw new InvalidOperationException("TrainingResult is null, call method 'TrainAsync' first");
    }

    var predictionEngine = _context.Model.CreatePredictionEngine<StudentData, StudentPrediction>(_trainingResult.Transformer);

    var input = new StudentData
    {
        MaritalStatus = maritalStatus,
        ApplicationMode = applicationMode,
        ApplicationOrder = applicationOrder,
        Course = course,
        Attendance = attendance,
        PreviousQualification = previousQualification,
        Nationality = nationality,
        MothersQualification = mothersQualification,
        FathersQualification = fathersQualification,
        MothersOccupation = mothersOccupation,
        FathersOccupation = fathersOccupation,
        Displaced = displaced,
        EducationalSpecialNeeds = educationalSpecialNeeds,
        Debtor = debtor,
        TuitionFeesUpToDate = tuitionFeesUpToDate,
        Gender = gender,
        ScholarshipHolder = scholarshipHolder,
        AgeAtEnrollment = ageAtEnrollment,
        International = international,
        CurricularUnits1StSemCredited = curricularUnits1StSemCredited,
        CurricularUnits1StSemEnrolled = curricularUnits1StSemEnrolled,
        CurricularUnits1StSemEvaluations = curricularUnits1StSemEvaluations,
        CurricularUnits1StSemApproved = curricularUnits1StSemApproved,
        CurricularUnits1StSemGrade = curricularUnits1StSemGrade,
        CurricularUnits1StSemWithoutEvaluations = curricularUnits1StSemWithoutEvaluations,
        CurricularUnits2NdSemCredited = curricularUnits2NdSemCredited,
        CurricularUnits2NdSemEnrolled = curricularUnits2NdSemEnrolled,
        CurricularUnits2NdSemEvaluations = curricularUnits2NdSemEvaluations,
        CurricularUnits2NdSemApproved = curricularUnits2NdSemApproved,
        CurricularUnits2NdSemGrade = curricularUnits2NdSemGrade,
        CurricularUnits2NdSemWithoutEvaluations = curricularUnits2NdSemWithoutEvaluations,
        UnemploymentRate = unemploymentRate,
        InflationRate = inflationRate,
        Gdp = gdp
    };

    var output = predictionEngine.Predict(input);

    var scores = output.Score;

    var classNames = new[] { "Dropout", "Graduate", "Still Enrolled" };
    var probabilities = scores.ToArray();
    var gradePredictionProbabilities = probabilities.Select((probability, index) => new PredictionProbability
    {
        Label = classNames[index],
        Probability = probability * 100
    }).OrderBy(p => p.Label).ToList();

    return new PredictionResult
    {
        PredictedLabel = output.Label,
        PredictionProbabilities = gradePredictionProbabilities,
        Metrics = _trainingResult.Metrics
    };
}



	public MulticlassClassificationMetrics? GetMetrics()
	{
		return _trainingResult?.Metrics;
	}

	private static TrainingResult Train(byte[] dataset, PipelineTypeEnum pipelineType, TrainerTypeEnum trainerType)
    {
        var mlContext = new MLContext();

        var inMemoryMultiStreamSource = new InMemoryMultiStreamSource(dataset);

        var textLoader = mlContext.Data.CreateTextLoader<StudentData>(hasHeader: true, separatorChar: ',');

        var trainingDataView = textLoader.Load(inMemoryMultiStreamSource);

        var dataProcessPipeline = ResolvePipeline(mlContext, pipelineType);

        var trainer = ResolveTrainer(mlContext, trainerType);

        var trainingPipeline = dataProcessPipeline.Append(trainer);

        var transformer = trainingPipeline.Fit(trainingDataView);

        var predictions = transformer.Transform(trainingDataView);

        var metrics = mlContext.MulticlassClassification.Evaluate(predictions);

        return new TrainingResult
        {
	        Transformer = transformer,
	        Metrics = metrics
        };
    }

    private static EstimatorChain<KeyToValueMappingTransformer> ResolvePipeline(MLContext mlContext, PipelineTypeEnum pipelineType)
    {
	    return pipelineType switch
	    {
		    PipelineTypeEnum.Default => mlContext.Transforms.Conversion.MapValueToKey(nameof(StudentData.Target))
            .Append(mlContext.Transforms.Categorical.OneHotEncoding([
	            new InputOutputColumnPair(nameof(StudentData.MaritalStatus)),
                new InputOutputColumnPair(nameof(StudentData.ApplicationMode)),
                new InputOutputColumnPair(nameof(StudentData.Course)),
                new InputOutputColumnPair(nameof(StudentData.Attendance)),
                new InputOutputColumnPair(nameof(StudentData.PreviousQualification)),
                new InputOutputColumnPair(nameof(StudentData.Nationality)),
                new InputOutputColumnPair(nameof(StudentData.MothersQualification)),
                new InputOutputColumnPair(nameof(StudentData.FathersQualification)),
                new InputOutputColumnPair(nameof(StudentData.MothersOccupation)),
                new InputOutputColumnPair(nameof(StudentData.FathersOccupation)),
                new InputOutputColumnPair(nameof(StudentData.Displaced)),
                new InputOutputColumnPair(nameof(StudentData.EducationalSpecialNeeds)),
                new InputOutputColumnPair(nameof(StudentData.Debtor)),
                new InputOutputColumnPair(nameof(StudentData.TuitionFeesUpToDate)),
                new InputOutputColumnPair(nameof(StudentData.Gender)),
                new InputOutputColumnPair(nameof(StudentData.ScholarshipHolder)),
                new InputOutputColumnPair(nameof(StudentData.International))
            ]))
            .Append(mlContext.Transforms.Concatenate("Features",
                nameof(StudentData.MaritalStatus),
                nameof(StudentData.ApplicationMode),
                nameof(StudentData.ApplicationOrder),
                nameof(StudentData.Course),
                nameof(StudentData.Attendance),
                nameof(StudentData.PreviousQualification),
                nameof(StudentData.Nationality),
                nameof(StudentData.MothersQualification),
                nameof(StudentData.FathersQualification),
                nameof(StudentData.MothersOccupation),
                nameof(StudentData.FathersOccupation),
                nameof(StudentData.Displaced),
                nameof(StudentData.EducationalSpecialNeeds),
                nameof(StudentData.Debtor),
                nameof(StudentData.TuitionFeesUpToDate),
                nameof(StudentData.Gender),
                nameof(StudentData.ScholarshipHolder),
                nameof(StudentData.AgeAtEnrollment),
                nameof(StudentData.International),
                nameof(StudentData.CurricularUnits1StSemCredited),
                nameof(StudentData.CurricularUnits1StSemEnrolled),
                nameof(StudentData.CurricularUnits1StSemEvaluations),
                nameof(StudentData.CurricularUnits1StSemApproved),
                nameof(StudentData.CurricularUnits1StSemGrade),
                nameof(StudentData.CurricularUnits1StSemWithoutEvaluations),
                nameof(StudentData.CurricularUnits2NdSemCredited),
                nameof(StudentData.CurricularUnits2NdSemEnrolled),
                nameof(StudentData.CurricularUnits2NdSemEvaluations),
                nameof(StudentData.CurricularUnits2NdSemApproved),
                nameof(StudentData.CurricularUnits2NdSemGrade),
                nameof(StudentData.CurricularUnits2NdSemWithoutEvaluations),
                nameof(StudentData.UnemploymentRate),
                nameof(StudentData.InflationRate),
                nameof(StudentData.Gdp)))
            .Append(mlContext.Transforms.NormalizeMinMax("Features"))
            .Append(mlContext.Transforms.Conversion.MapKeyToValue(nameof(StudentPrediction.Label))),
		    _ => throw new ArgumentOutOfRangeException(nameof(pipelineType), pipelineType, null)
	    };
    }

    private static IEstimator<ITransformer> ResolveTrainer(MLContext mlContext, TrainerTypeEnum trainerType)
    {
	    return trainerType switch
	    {
		    TrainerTypeEnum.OneVersusAllWithFastForest => mlContext.MulticlassClassification.Trainers
			    .OneVersusAll(mlContext.BinaryClassification.Trainers
				    .FastForest()),
		    _ => throw new ArgumentOutOfRangeException(nameof(trainerType), trainerType, null)
	    };
    }
}
