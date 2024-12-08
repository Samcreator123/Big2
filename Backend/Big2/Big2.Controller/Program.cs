var builder = WebApplication.CreateBuilder(args);

// 開啟OpenApi的文件設定，後續的AddSwaggerUI才能生成對應的Swagger UI
builder.Services.AddOpenApiDoc();

// 處理依賴注入的物件
builder.Services.AddApplicationServices();

// 註冊版本控制
builder.Services.AddApiVersioning();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerInDevelopment();

// 統一處理錯誤
app.UseBig2ExceptionHandler();

// 使用 Minimal APIs 風格，而非 builder.Services.AddControllers() 的控制器
// 使用版本控制
app.NewVersionedApi("Big2");

app.Run();
