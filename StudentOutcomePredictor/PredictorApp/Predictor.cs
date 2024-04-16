using Microsoft.ML;
using Microsoft.ML.Data;
using PredictorApp.Models;

namespace PredictorApp;

public static class Predictor
{
    public static async Task<TrainingResult> TrainAsync()
    {
        var mlContext = new MLContext();

        var trainingDataView = mlContext.Data.LoadFromTextFile<Input>(
            path: PredictorHelper.DataPath,
            hasHeader: true,
            separatorChar: ',');

        var dataProcessPipeline = mlContext.Transforms.Conversion
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
		        .ConvertType(PredictorHelper.StudentIdInputOutputColumnName, PredictorHelper.StudentIdInputOutputColumnName, outputKind: DataKind.Single))
	        .Append(mlContext.Transforms.Conversion
		        .ConvertType(PredictorHelper.AgeInputOutputColumnName, PredictorHelper.AgeInputOutputColumnName, outputKind: DataKind.Single))
	        .Append(mlContext.Transforms.Conversion
		        .ConvertType(PredictorHelper.YearInputOutputColumnName, PredictorHelper.YearInputOutputColumnName, outputKind: DataKind.Single))
	        .Append(mlContext.Transforms
		        .Concatenate(
					PredictorHelper.FeaturesColumnName,
			        PredictorHelper.StudentIdInputOutputColumnName,
			        PredictorHelper.AgeInputOutputColumnName,
			        PredictorHelper.YearInputOutputColumnName,
			        PredictorHelper.EncodedFieldOfStudyOutputColumnName,
			        PredictorHelper.EncodedSubjectOutputColumnName))
	        .Append(mlContext.Transforms.NormalizeMinMax(PredictorHelper.FeaturesColumnName));

        var averagePerceptronTrainer = mlContext.BinaryClassification.Trainers.AveragedPerceptron();

        var oneVersusAllTrainer = mlContext.MulticlassClassification.Trainers.OneVersusAll(averagePerceptronTrainer);

        var trainingPipeline = dataProcessPipeline.Append(oneVersusAllTrainer);

        var transformer = await Task.Run(() => trainingPipeline.Fit(trainingDataView));

        var predictions = transformer.Transform(trainingDataView);

        var metrics = mlContext.MulticlassClassification.Evaluate(predictions);

        return new TrainingResult
        {
	        Transformer = transformer,
	        Metrics = metrics
        };
    }
}

