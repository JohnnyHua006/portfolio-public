namespace StockApi.Models;

public class TickerStats
{
    public string   Ticker     { get; init; } = default!;  // e.g. "AAPL"
    public DateTime Date       { get; init; }
    public double   Open       { get; init; }
    public double   Close      { get; init; }
    public long     Volume     { get; init; }
    public double   MarketCap  { get; init; }
}