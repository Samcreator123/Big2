var builder = WebApplication.CreateBuilder(args);

// �}��OpenApi�����]�w�A����AddSwaggerUI�~��ͦ�������Swagger UI
builder.Services.AddOpenApiDoc();

// �B�z�̿�`�J������
builder.Services.AddApplicationServices();

// ���U��������
builder.Services.AddApiVersioning();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerInDevelopment();

// �Τ@�B�z���~
app.UseBig2ExceptionHandler();

// �ϥ� Minimal APIs ����A�ӫD builder.Services.AddControllers() �����
// �ϥΪ�������
app.NewVersionedApi("Big2");

app.Run();
