namespace InventoryManagement.Infrastructure.Services.AI;

public class DailyStockDemand
{
    public DateTime Date { get; set; }
    public float UnitsSold { get; set; }
}

public class DemandForecast
{
    public float[] ForecastedSales { get; set; } = [];
    public float[] LowerBound { get; set; } = [];
    public float[] UpperBound { get; set; } = [];
}