using StockApi.Models;

namespace StockApi.Services;

public interface ITickerService
{
    Task<TickerStats?> GetLatestAsync(string ticker, CancellationToken ct = default);
    Task<IEnumerable<TickerStats>> GetTopVolumeAsync(DateTime date, int limit = 10, CancellationToken ct = default);
}