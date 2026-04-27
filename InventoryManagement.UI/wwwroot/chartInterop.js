window.renderForecastChart = (canvasId, labels, historyData, forecastData) => {
    const canvas = document.getElementById(canvasId);
    if (!canvas) return;
    
    const ctx = canvas.getContext('2d');

    // 1. CRITICAL SPA FIX: Destroy the old chart before drawing a new one
    if (window.myForecastChart != null) {
        window.myForecastChart.destroy();
    }

    // 2. Safe Padding Logic (Handles products with ZERO history)
    let paddedForecast = [];
    
    if (historyData && historyData.length > 0) {
        paddedForecast = Array(historyData.length - 1).fill(null);
        paddedForecast.push(historyData[historyData.length - 1]); 
    }
    
    paddedForecast.push(...forecastData);

    window.myForecastChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [
                {
                    label: 'Historical Sales',
                    data: historyData,
                    borderColor: '#3b82f6', // Blue
                    backgroundColor: 'rgba(59, 130, 246, 0.1)',
                    fill: true,
                    tension: 0.3
                },
                {
                    label: 'AI Forecast (Next 7 Days)',
                    data: paddedForecast,
                    borderColor: '#ef4444', // Red
                    borderDash: [5, 5],     // Dashed line
                    tension: 0.3,
                    pointBackgroundColor: '#ef4444'
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            interaction: { mode: 'index', intersect: false }
        }
    });
};