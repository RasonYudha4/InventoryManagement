using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace InventoryManagement.Infrastructure.Services.AI;

public class InventoryForecastingService
{
    private readonly MLContext _mlContext;

    public InventoryForecastingService()
    {
        // Initialize the ML environment
        _mlContext = new MLContext();
    }

    public DemandForecast PredictNextWeekDemand(List<DailyStockDemand> historicalData)
    {
        // 1. Load the historical data into ML.NET's memory
        IDataView dataView = _mlContext.Data.LoadFromEnumerable(historicalData);

        // 2. Configure the Time Series Algorithm (SSA)
        var forecastingPipeline = _mlContext.Forecasting.ForecastBySsa(
            outputColumnName: nameof(DemandForecast.ForecastedSales),
            inputColumnName: nameof(DailyStockDemand.UnitsSold),
            windowSize: 7,       // Look at data in 7-day chunks (weekly trends)
            seriesLength: 30,    // We need at least 30 days of historical data
            trainSize: historicalData.Count, 
            horizon: 7,          // Predict the next 7 days!
            confidenceLevel: 0.95f, // 95% confidence bounds
            confidenceLowerBoundColumn: nameof(DemandForecast.LowerBound),
            confidenceUpperBoundColumn: nameof(DemandForecast.UpperBound));

        // 3. Train the Model (This takes milliseconds!)
        var model = forecastingPipeline.Fit(dataView);

        // 4. Create a Prediction Engine
        var forecastingEngine = model.CreateTimeSeriesEngine<DailyStockDemand, DemandForecast>(_mlContext);

        // 5. Ask the engine for the future
        var prediction = forecastingEngine.Predict();
        
        return prediction;
    }
}