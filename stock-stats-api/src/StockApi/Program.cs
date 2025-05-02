using Elastic.Clients.Elasticsearch;
using StockApi.Services;

var builder = WebApplication.CreateBuilder(args);

// strongly‑typed config
builder.Services.Configure<ElasticOptions>(builder.Configuration.GetSection("Elastic"));

// Elasticsearch client (NEST v8 – Elastic.Clients.Elasticsearch)
var opts = builder.Configuration.GetSection("Elastic").Get<ElasticOptions>()!;
var settings = new ElasticsearchClientSettings(new Uri(opts.Uri))
                  .DefaultIndex(opts.IndexName);
builder.Services.AddSingleton(new ElasticsearchClient(settings));

// DI wiring
builder.Services.AddScoped<ITickerService, TickerService>();

// MVC + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();