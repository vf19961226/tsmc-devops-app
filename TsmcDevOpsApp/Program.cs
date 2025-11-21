using Prometheus;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 啟用 HTTP Request Metrics
app.UseHttpMetrics();

// 暴露 /metrics
app.MapMetrics("/metrics");

app.MapGet("/", () => "Hello, TSMC DevOps Candidate! (from C# App)");

app.Run();