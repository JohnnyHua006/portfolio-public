using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Options;
using StockApi.Models;

namespace StockApi.Services;

public sealed class TickerService : ITickerService
{
    private readonly ElasticsearchClient _client;
    private readonly string _index;

    public TickerService(ElasticsearchClient client, IOptions<ElasticOptions> opts)
    {
        _client = client;
        _index  = opts.Value.IndexName;
    }

    public async Task<TickerStats?> GetLatestAsync(string ticker, CancellationToken ct = default)
    {
        var response = await _client.SearchAsync<TickerStats>(s => s
            .Index(_index)
            .Query(q => q.Term(t => t.Ticker.Suffix("keyword"), ticker))
            .Size(1)
            .Sort(ss => ss.Descending(f => f.Date)), ct);

        return response.Documents.FirstOrDefault();
    }

    public async Task<IEnumerable<TickerStats>> GetTopVolumeAsync(DateTime date, int limit = 10, CancellationToken ct = default)
    {
        var response = await _client.SearchAsync<TickerStats>(s => s
            .Index(_index)
            .Query(q => q
                .DateRange(r => r
                    .Field(f => f.Date)
                    .Gte(date)
                    .Lt(date.AddDays(1)))
            )
            .Size(limit)
            .Sort(ss => ss.Descending(f => f.Volume)), ct);

        return response.Documents;
    }
}

public sealed class ElasticOptions
{
    public string Uri       { get; init; } = null!;
    public string IndexName { get; init; } = "stocks";
}