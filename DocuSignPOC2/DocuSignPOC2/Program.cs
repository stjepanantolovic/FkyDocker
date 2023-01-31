
using DocuSignPOC2.Services.IDocuSignEnvelope;
using DocuSignPOC2.Services.IESignAdmin;
using DocuSignPOC2.Services.IUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IeSignAdminService, ESignAdminService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDocuSignEnvelopeService, DocuSignEnvelopeService>();
builder.Services.AddScoped<IeSignAdminService, ESignAdminService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
