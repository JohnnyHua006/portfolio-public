using Microsoft.AspNetCore.Mvc;
using StockApi.Services;

namespace StockApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TickersController : ControllerBase
{
    private readonly ITickerService _svc;
    public TickersController(ITickerService svc) => _svc = svc;

    /// <summary>Latest available stats for a given ticker symbol.</summary>
    [HttpGet("{ticker}/latest")]
    public async Task<IActionResult> Latest(string ticker, CancellationToken ct)
        => (await _svc.GetLatestAsync(ticker, ct)) is { } stats ? Ok(stats) : NotFound();

    /// <summary>Top‑N tickers by volume for a given date (UTC).</summary>
    [HttpGet("top-volume")]
    public async Task<IActionResult> TopVolume([FromQuery] DateTime date, [FromQuery] int limit = 10, CancellationToken ct = default)
        => Ok(await _svc.GetTopVolumeAsync(date, limit, ct));
}