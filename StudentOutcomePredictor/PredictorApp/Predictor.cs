using Microsoft.ML;
using Microsoft.ML.Data;
using PredictorApp.Models;

namespace PredictorApp;

public static class Predictor
{
    public static async Task<ITransformer> TrainAsync()
    {
        var mlContext = new MLContext();

        var trainingDataView = mlContext.Data.LoadFromTextFile<Input>(
            path: PredictorHelper.DataPath,
            hasHeader: true,
            separatorChar: ',');

        var dataProcessPipeline = mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: PredictorHelper.EncodedFieldOfStudyOutputColumnName, inputColumnName: PredictorHelper.FieldOfStudyInputColumnName)
            .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: PredictorHelper.EncodedSubjectOutputColumnName, inputColumnName: PredictorHelper.SubjectInputColumnName))
            .Append(mlContext.Transforms.Conversion.ConvertType(outputColumnName: PredictorHelper.StudentIdInputOutputColumnName, inputColumnName: PredictorHelper.StudentIdInputOutputColumnName, outputKind: DataKind.Single))
            .Append(mlContext.Transforms.Conversion.ConvertType(outputColumnName: PredictorHelper.AgeInputOutputColumnName, inputColumnName: PredictorHelper.AgeInputOutputColumnName, outputKind: DataKind.Single))
            .Append(mlContext.Transforms.Conversion.ConvertType(outputColumnName: PredictorHelper.YearInputOutputColumnName, inputColumnName: PredictorHelper.YearInputOutputColumnName, outputKind: DataKind.Single))
            .Append(mlContext.Transforms.Concatenate(PredictorHelper.FeaturesColumnName, PredictorHelper.StudentIdInputOutputColumnName, PredictorHelper.AgeInputOutputColumnName, PredictorHelper.YearInputOutputColumnName, PredictorHelper.EncodedFieldOfStudyOutputColumnName, PredictorHelper.EncodedSubjectOutputColumnName))
            .Append(mlContext.Transforms.NormalizeMinMax(PredictorHelper.FeaturesColumnName));

        var trainer = mlContext.Regression.Trainers.FastTree(labelColumnName: PredictorHelper.GradeColumnName, featureColumnName: PredictorHelper.FeaturesColumnName);

        var trainingPipeline = dataProcessPipeline.Append(trainer);

        return await Task.Run(() => trainingPipeline.Fit(trainingDataView));
    }
}

