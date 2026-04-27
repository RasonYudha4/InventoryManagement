using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Infrastructure.Services.AI;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ForecastController : ControllerBase
{
    private readonly InventoryForecastingService _forecaster;

    public ForecastController(InventoryForecastingService forecaster)
    {
        _forecaster = forecaster;
    }

    [HttpGet("predict/{sku}")]
    public IActionResult GetForecastForProduct(string sku)
    {
        // IN THE REAL WORLD: You would query your ApplicationDbContext here
        // to get the actual historical sales for this specific SKU over the last 30+ days.
        
        // FOR TESTING: We will generate fake historical data to prove the math works.
        var fakeHistory = GenerateMockHistory();

        try
        {
            // Ask ML.NET to predict the next 7 days
            var prediction = _forecaster.PredictNextWeekDemand(fakeHistory);
            
            // Sum up the array to get the total units needed for the week
            float totalNeededNextWeek = prediction.ForecastedSales.Sum();

            return Ok(new 
            { 
                Sku = sku,
                TotalUnitsNeededNext7Days = Math.Round(totalNeededNextWeek),
                DailyBreakdown = prediction.ForecastedSales 
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Forecasting failed: {ex.Message}");
        }
    }

    // A quick helper to generate 60 days of fake sales data so you can test it immediately
    private List<DailyStockDemand> GenerateMockHistory()
    {
        var history = new List<DailyStockDemand>();
        var random = new Random();
        for (int i = 60; i > 0; i--)
        {
            history.Add(new DailyStockDemand 
            { 
                Date = DateTime.Now.AddDays(-i), 
                // Fake sales: between 5 and 15 units a day
                UnitsSold = random.Next(5, 15) 
            });
        }
        return history;
    }
}