// Program.cs
using Prometheus.Client.AspNetCore; // <-- 1. 新增命名空間
using Prometheus.Client.MetricServer;

var builder = WebApplication.CreateBuilder(args);

// 2. 註冊 Prometheus Client 服務
builder.Services.AddMetrics(); 

var app = builder.Build();

// 3. 註冊 Prometheus Middleware (在 UseRouting 之前)
app.UsePrometheusMetrics(); 

// 4. 暴露 Metrics 端點，通常是 /metrics
app.UseMetricServer("/metrics"); 

// 原有的應用程式端點
app.MapGet("/", () => "Hello, TSMC DevOps Candidate! (from C# App)");

// ... 其他 MapGet/MapPost

app.Run();