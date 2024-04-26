using System.Text;
using Infrastructure.Enums;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using PredictorApp.Models;
using PredictorApp.Sources;

namespace PredictorApp;

public static class Predictor
{
    public static TrainingResult Train(byte[] dataset, PipelineTypeEnum pipelineType, TrainerTypeEnum trainerType)
    {
        var mlContext = new MLContext();

        var inMemoryMultiStreamSource = new InMemoryMultiStreamSource(dataset);

        var textLoader = mlContext.Data.CreateTextLoader<Input>(hasHeader: true, separatorChar: ',');

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

    private static EstimatorChain<NormalizingTransformer> ResolvePipeline(MLContext mlContext, PipelineTypeEnum pipelineType)
    {
	    return pipelineType switch
	    {
		    PipelineTypeEnum.Default => mlContext.Transforms.Conversion
		    .MapValueToKey(PredictorHelper.Label, PredictorHelper.GradeInputOutputColumnName)
		    .Append(mlContext.Transforms.Categorical
			    .OneHotEncoding(PredictorHelper.EncodedFieldOfStudyOutputColumnName, PredictorHelper.FieldOfStudyInputColumnName))
		    .Append(mlContext.Transforms.Categorical
			    .OneHotEncoding(PredictorHelper.EncodedSubjectOutputColumnName, PredictorHelper.SubjectInputColumnName))
		    .Append(mlContext.Transforms.Conversion
			    .ConvertType(PredictorHelper.EncodedFieldOfStudyOutputColumnName, PredictorHelper.EncodedFieldOfStudyOutputColumnName, outputKind: DataKind.Single))
		    .Append(mlContext.Transforms.Conversion
			    .ConvertType(PredictorHelper.EncodedSubjectOutputColumnName, PredictorHelper.EncodedSubjectOutputColumnName, outputKind: DataKind.Single))
		    .Append(mlContext.Transforms.Conversion
			    .ConvertType(PredictorHelper.AgeInputOutputColumnName, PredictorHelper.AgeInputOutputColumnName, outputKind: DataKind.Single))
		    .Append(mlContext.Transforms.Conversion
			    .ConvertType(PredictorHelper.YearInputOutputColumnName, PredictorHelper.YearInputOutputColumnName, outputKind: DataKind.Single))
		    .Append(mlContext.Transforms
			    .Concatenate(
				    PredictorHelper.FeaturesColumnName,
				    PredictorHelper.AgeInputOutputColumnName,
				    PredictorHelper.YearInputOutputColumnName,
				    PredictorHelper.EncodedFieldOfStudyOutputColumnName,
				    PredictorHelper.EncodedSubjectOutputColumnName))
		    .Append(mlContext.Transforms.NormalizeMinMax(PredictorHelper.FeaturesColumnName)),
		    _ => throw new ArgumentOutOfRangeException(nameof(pipelineType), pipelineType, null)
	    };
    }

    private static IEstimator<ITransformer> ResolveTrainer(MLContext mlContext, TrainerTypeEnum trainerType)
    {
	    return trainerType switch
	    {
		    TrainerTypeEnum.PairwiseCouplingWithFastForest => mlContext.MulticlassClassification.Trainers
			    .PairwiseCoupling(mlContext.BinaryClassification.Trainers
				    .FastForest()),
		    TrainerTypeEnum.OneVersusAllWithFastForest => mlContext.MulticlassClassification.Trainers
			    .OneVersusAll(mlContext.BinaryClassification.Trainers
				    .FastForest()),
		    TrainerTypeEnum.PairwiseCouplingWithAveragedPerceptron => mlContext.MulticlassClassification.Trainers
			    .PairwiseCoupling(mlContext.BinaryClassification.Trainers
				    .AveragedPerceptron()),
		    TrainerTypeEnum.OneVersusAllWithAveragedPerceptron => mlContext.MulticlassClassification.Trainers
			    .OneVersusAll(mlContext.BinaryClassification.Trainers
				    .AveragedPerceptron()),
		    _ => throw new ArgumentOutOfRangeException(nameof(trainerType), trainerType, null)
	    };
    }
}

