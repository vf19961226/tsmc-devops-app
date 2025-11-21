// Program.cs
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 實作 / 路徑，回傳練習要求的訊息
app.MapGet("/", () => "Hello, TSMC DevOps Candidate! (from C# App)");

// 為了讓 Prometheus 收集 Metrics，我們通常會加入一個 Metrics Endpoint
// 在這裡先保持簡單，之後的步驟我們再來加強 Metrics 收集。

app.Run();