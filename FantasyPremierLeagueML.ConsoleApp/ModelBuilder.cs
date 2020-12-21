// This file was auto-generated by ML.NET Model Builder. 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using FantasyPremierLeagueML.Model;

namespace FantasyPremierLeagueML.ConsoleApp
{
    public static class ModelBuilder
    {
        private static string TRAIN_DATA_FILEPATH = @"C:\GitHub\JonnyRivers\FantasyPremierLeague\FantasyPremierLeague.Testbed\bin\Debug\netcoreapp3.1\data.csv";
        private static string MODEL_FILE = ConsumeModel.MLNetModelPath;

        // Create MLContext to be shared across the model creation workflow objects 
        // Set a random seed for repeatable/deterministic results across multiple trainings.
        private static MLContext mlContext = new MLContext(seed: 1);

        public static void CreateModel()
        {
            // Load Data
            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: TRAIN_DATA_FILEPATH,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Build training pipeline
            IEstimator<ITransformer> trainingPipeline = BuildTrainingPipeline(mlContext);

            // Train Model
            ITransformer mlModel = TrainModel(mlContext, trainingDataView, trainingPipeline);

            // Evaluate quality of Model
            Evaluate(mlContext, trainingDataView, trainingPipeline);

            // Save model
            SaveModel(mlContext, mlModel, MODEL_FILE, trainingDataView.Schema);
        }

        public static IEstimator<ITransformer> BuildTrainingPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations 
            var dataProcessPipeline = mlContext.Transforms.Categorical.OneHotEncoding(new[] { new InputOutputColumnPair("isvalid2017", "isvalid2017"), new InputOutputColumnPair("isvalidgw-less-1", "isvalidgw-less-1"), new InputOutputColumnPair("athomegw-less-1", "athomegw-less-1"), new InputOutputColumnPair("isvalidgw-less-2", "isvalidgw-less-2"), new InputOutputColumnPair("athomegw-less-2", "athomegw-less-2"), new InputOutputColumnPair("isvalidgw-less-3", "isvalidgw-less-3"), new InputOutputColumnPair("athomegw-less-3", "athomegw-less-3"), new InputOutputColumnPair("isvalidgw-less-4", "isvalidgw-less-4"), new InputOutputColumnPair("athomegw-less-4", "athomegw-less-4"), new InputOutputColumnPair("isvalidgw-less-5", "isvalidgw-less-5"), new InputOutputColumnPair("athomegw-less-5", "athomegw-less-5"), new InputOutputColumnPair("isvalidgw-less-6", "isvalidgw-less-6"), new InputOutputColumnPair("athomegw-less-6", "athomegw-less-6"), new InputOutputColumnPair("isvalidgw-less-7", "isvalidgw-less-7"), new InputOutputColumnPair("athomegw-less-7", "athomegw-less-7"), new InputOutputColumnPair("isvalidgw-less-8", "isvalidgw-less-8"), new InputOutputColumnPair("athomegw-less-8", "athomegw-less-8"), new InputOutputColumnPair("isvalidgw-less-9", "isvalidgw-less-9"), new InputOutputColumnPair("athomegw-less-9", "athomegw-less-9"), new InputOutputColumnPair("isvalidgw-less-10", "isvalidgw-less-10"), new InputOutputColumnPair("athomegw-less-10", "athomegw-less-10"), new InputOutputColumnPair("isvalidgw-less-11", "isvalidgw-less-11"), new InputOutputColumnPair("pointsgw-less-11", "pointsgw-less-11"), new InputOutputColumnPair("athomegw-less-11", "athomegw-less-11"), new InputOutputColumnPair("isvalidgw-less-12", "isvalidgw-less-12"), new InputOutputColumnPair("minutesgw-less-12", "minutesgw-less-12"), new InputOutputColumnPair("pointsgw-less-12", "pointsgw-less-12"), new InputOutputColumnPair("athomegw-less-12", "athomegw-less-12"), new InputOutputColumnPair("isvalidgw-less-13", "isvalidgw-less-13"), new InputOutputColumnPair("home", "home") })
                                      .Append(mlContext.Transforms.Categorical.OneHotHashEncoding(new[] { new InputOutputColumnPair("minutes2017", "minutes2017"), new InputOutputColumnPair("points2017", "points2017"), new InputOutputColumnPair("threat2017", "threat2017"), new InputOutputColumnPair("isvalid2018", "isvalid2018"), new InputOutputColumnPair("minutes2018", "minutes2018"), new InputOutputColumnPair("points2018", "points2018"), new InputOutputColumnPair("threat2018", "threat2018"), new InputOutputColumnPair("isvalid2019", "isvalid2019"), new InputOutputColumnPair("minutes2019", "minutes2019"), new InputOutputColumnPair("points2019", "points2019"), new InputOutputColumnPair("threat2019", "threat2019"), new InputOutputColumnPair("difficultygw-less-1", "difficultygw-less-1"), new InputOutputColumnPair("minutesgw-less-2", "minutesgw-less-2"), new InputOutputColumnPair("pointsgw-less-2", "pointsgw-less-2"), new InputOutputColumnPair("difficultygw-less-2", "difficultygw-less-2"), new InputOutputColumnPair("minutesgw-less-3", "minutesgw-less-3"), new InputOutputColumnPair("pointsgw-less-3", "pointsgw-less-3"), new InputOutputColumnPair("difficultygw-less-3", "difficultygw-less-3"), new InputOutputColumnPair("minutesgw-less-4", "minutesgw-less-4"), new InputOutputColumnPair("pointsgw-less-4", "pointsgw-less-4"), new InputOutputColumnPair("difficultygw-less-4", "difficultygw-less-4"), new InputOutputColumnPair("minutesgw-less-5", "minutesgw-less-5"), new InputOutputColumnPair("pointsgw-less-5", "pointsgw-less-5"), new InputOutputColumnPair("difficultygw-less-5", "difficultygw-less-5"), new InputOutputColumnPair("minutesgw-less-6", "minutesgw-less-6"), new InputOutputColumnPair("pointsgw-less-6", "pointsgw-less-6"), new InputOutputColumnPair("threatgw-less-6", "threatgw-less-6"), new InputOutputColumnPair("difficultygw-less-6", "difficultygw-less-6"), new InputOutputColumnPair("minutesgw-less-7", "minutesgw-less-7"), new InputOutputColumnPair("pointsgw-less-7", "pointsgw-less-7"), new InputOutputColumnPair("threatgw-less-7", "threatgw-less-7"), new InputOutputColumnPair("difficultygw-less-7", "difficultygw-less-7"), new InputOutputColumnPair("minutesgw-less-8", "minutesgw-less-8"), new InputOutputColumnPair("pointsgw-less-8", "pointsgw-less-8"), new InputOutputColumnPair("threatgw-less-8", "threatgw-less-8"), new InputOutputColumnPair("difficultygw-less-8", "difficultygw-less-8"), new InputOutputColumnPair("minutesgw-less-9", "minutesgw-less-9"), new InputOutputColumnPair("pointsgw-less-9", "pointsgw-less-9"), new InputOutputColumnPair("threatgw-less-9", "threatgw-less-9"), new InputOutputColumnPair("difficultygw-less-9", "difficultygw-less-9"), new InputOutputColumnPair("minutesgw-less-10", "minutesgw-less-10"), new InputOutputColumnPair("pointsgw-less-10", "pointsgw-less-10"), new InputOutputColumnPair("threatgw-less-10", "threatgw-less-10"), new InputOutputColumnPair("difficultygw-less-10", "difficultygw-less-10"), new InputOutputColumnPair("minutesgw-less-11", "minutesgw-less-11"), new InputOutputColumnPair("threatgw-less-11", "threatgw-less-11"), new InputOutputColumnPair("difficultygw-less-11", "difficultygw-less-11"), new InputOutputColumnPair("influencegw-less-12", "influencegw-less-12"), new InputOutputColumnPair("creativitygw-less-12", "creativitygw-less-12"), new InputOutputColumnPair("threatgw-less-12", "threatgw-less-12"), new InputOutputColumnPair("difficultygw-less-12", "difficultygw-less-12"), new InputOutputColumnPair("minutesgw-less-13", "minutesgw-less-13"), new InputOutputColumnPair("pointsgw-less-13", "pointsgw-less-13"), new InputOutputColumnPair("influencegw-less-13", "influencegw-less-13"), new InputOutputColumnPair("creativitygw-less-13", "creativitygw-less-13"), new InputOutputColumnPair("threatgw-less-13", "threatgw-less-13"), new InputOutputColumnPair("athomegw-less-13", "athomegw-less-13"), new InputOutputColumnPair("difficultygw-less-13", "difficultygw-less-13"), new InputOutputColumnPair("diff", "diff") }))
                                      .Append(mlContext.Transforms.Concatenate("Features", new[] { "isvalid2017", "isvalidgw-less-1", "athomegw-less-1", "isvalidgw-less-2", "athomegw-less-2", "isvalidgw-less-3", "athomegw-less-3", "isvalidgw-less-4", "athomegw-less-4", "isvalidgw-less-5", "athomegw-less-5", "isvalidgw-less-6", "athomegw-less-6", "isvalidgw-less-7", "athomegw-less-7", "isvalidgw-less-8", "athomegw-less-8", "isvalidgw-less-9", "athomegw-less-9", "isvalidgw-less-10", "athomegw-less-10", "isvalidgw-less-11", "pointsgw-less-11", "athomegw-less-11", "isvalidgw-less-12", "minutesgw-less-12", "pointsgw-less-12", "athomegw-less-12", "isvalidgw-less-13", "home", "minutes2017", "points2017", "threat2017", "isvalid2018", "minutes2018", "points2018", "threat2018", "isvalid2019", "minutes2019", "points2019", "threat2019", "difficultygw-less-1", "minutesgw-less-2", "pointsgw-less-2", "difficultygw-less-2", "minutesgw-less-3", "pointsgw-less-3", "difficultygw-less-3", "minutesgw-less-4", "pointsgw-less-4", "difficultygw-less-4", "minutesgw-less-5", "pointsgw-less-5", "difficultygw-less-5", "minutesgw-less-6", "pointsgw-less-6", "threatgw-less-6", "difficultygw-less-6", "minutesgw-less-7", "pointsgw-less-7", "threatgw-less-7", "difficultygw-less-7", "minutesgw-less-8", "pointsgw-less-8", "threatgw-less-8", "difficultygw-less-8", "minutesgw-less-9", "pointsgw-less-9", "threatgw-less-9", "difficultygw-less-9", "minutesgw-less-10", "pointsgw-less-10", "threatgw-less-10", "difficultygw-less-10", "minutesgw-less-11", "threatgw-less-11", "difficultygw-less-11", "influencegw-less-12", "creativitygw-less-12", "threatgw-less-12", "difficultygw-less-12", "minutesgw-less-13", "pointsgw-less-13", "influencegw-less-13", "creativitygw-less-13", "threatgw-less-13", "athomegw-less-13", "difficultygw-less-13", "diff", "influence2017", "creativity2017", "influence2018", "creativity2018", "influence2019", "creativity2019", "minutesgw-less-1", "pointsgw-less-1", "influencegw-less-1", "creativitygw-less-1", "threatgw-less-1", "influencegw-less-2", "creativitygw-less-2", "threatgw-less-2", "influencegw-less-3", "creativitygw-less-3", "threatgw-less-3", "influencegw-less-4", "creativitygw-less-4", "threatgw-less-4", "influencegw-less-5", "creativitygw-less-5", "threatgw-less-5", "influencegw-less-6", "creativitygw-less-6", "influencegw-less-7", "creativitygw-less-7", "influencegw-less-8", "creativitygw-less-8", "influencegw-less-9", "creativitygw-less-9", "influencegw-less-10", "creativitygw-less-10", "influencegw-less-11", "creativitygw-less-11", "value" }))
                                      .Append(mlContext.Transforms.NormalizeMinMax("Features", "Features"))
                                      .AppendCacheCheckpoint(mlContext);
            // Set the training algorithm 
            var trainer = mlContext.Regression.Trainers.Sdca(labelColumnName: @"points", featureColumnName: "Features");

            var trainingPipeline = dataProcessPipeline.Append(trainer);

            return trainingPipeline;
        }

        public static ITransformer TrainModel(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {
            Console.WriteLine("=============== Training  model ===============");

            ITransformer model = trainingPipeline.Fit(trainingDataView);

            Console.WriteLine("=============== End of training process ===============");
            return model;
        }

        private static void Evaluate(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {
            // Cross-Validate with single dataset (since we don't have two datasets, one for training and for evaluate)
            // in order to evaluate and get the model's accuracy metrics
            Console.WriteLine("=============== Cross-validating to get model's accuracy metrics ===============");
            var crossValidationResults = mlContext.Regression.CrossValidate(trainingDataView, trainingPipeline, numberOfFolds: 5, labelColumnName: "points");
            PrintRegressionFoldsAverageMetrics(crossValidationResults);
        }

        private static void SaveModel(MLContext mlContext, ITransformer mlModel, string modelRelativePath, DataViewSchema modelInputSchema)
        {
            // Save/persist the trained model to a .ZIP file
            Console.WriteLine($"=============== Saving the model  ===============");
            mlContext.Model.Save(mlModel, modelInputSchema, GetAbsolutePath(modelRelativePath));
            Console.WriteLine("The model is saved to {0}", GetAbsolutePath(modelRelativePath));
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        public static void PrintRegressionMetrics(RegressionMetrics metrics)
        {
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Metrics for Regression model      ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       LossFn:        {metrics.LossFunction:0.##}");
            Console.WriteLine($"*       R2 Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Absolute loss: {metrics.MeanAbsoluteError:#.##}");
            Console.WriteLine($"*       Squared loss:  {metrics.MeanSquaredError:#.##}");
            Console.WriteLine($"*       RMS loss:      {metrics.RootMeanSquaredError:#.##}");
            Console.WriteLine($"*************************************************");
        }

        public static void PrintRegressionFoldsAverageMetrics(IEnumerable<TrainCatalogBase.CrossValidationResult<RegressionMetrics>> crossValidationResults)
        {
            var L1 = crossValidationResults.Select(r => r.Metrics.MeanAbsoluteError);
            var L2 = crossValidationResults.Select(r => r.Metrics.MeanSquaredError);
            var RMS = crossValidationResults.Select(r => r.Metrics.RootMeanSquaredError);
            var lossFunction = crossValidationResults.Select(r => r.Metrics.LossFunction);
            var R2 = crossValidationResults.Select(r => r.Metrics.RSquared);

            Console.WriteLine($"*************************************************************************************************************");
            Console.WriteLine($"*       Metrics for Regression model      ");
            Console.WriteLine($"*------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"*       Average L1 Loss:       {L1.Average():0.###} ");
            Console.WriteLine($"*       Average L2 Loss:       {L2.Average():0.###}  ");
            Console.WriteLine($"*       Average RMS:           {RMS.Average():0.###}  ");
            Console.WriteLine($"*       Average Loss Function: {lossFunction.Average():0.###}  ");
            Console.WriteLine($"*       Average R-squared:     {R2.Average():0.###}  ");
            Console.WriteLine($"*************************************************************************************************************");
        }
    }
}
