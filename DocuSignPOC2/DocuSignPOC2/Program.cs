
using DocuSignPOC2;
using DocuSignPOC2.DocuSignHandling.Services;
using DocuSignPOC2.Helpers;
using DocuSignPOC2.Services.IDocuSignEnvelope;
using DocuSignPOC2.Services.IESignAdminCache;
using DocuSignPOC2.Services.IPoc;
using DocuSignPOC2.Services.IUser;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Mvc;

try
{
    Log.Logger = new LoggerConfiguration()
    .CreateBootstrapLogger();
}
finally
{

    Log.CloseAndFlush();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
var localConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var elephantDblConnectionString = ElephantDbHelper.GetConnectionString(builder.Configuration);
builder.Services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(elephantDblConnectionString));

builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.PostgreSQL(elephantDblConnectionString, "Logs", needAutoCreateTable: true).MinimumLevel.Information();
});


builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IeSignAdminCacheService, ESignAdminCacheService>();
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDocuSignEnvelopeService, DocuSignEnvelopeService>();
builder.Services.AddScoped<IeSignAdminCacheService, ESignAdminCacheService>();
builder.Services.AddScoped<IESignAdminService, ESignAdminService>();
builder.Services.AddScoped<IDocuSignService, DocuSignService>();
builder.Services.AddScoped<IPocService, PocService>();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<DataContext>();
context?.Database.Migrate();


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.UseDefaultFiles();
app.UseStaticFiles();


app.MapControllers();

app.MapFallbackToController("Index", "Fallback");
app.UseSerilogRequestLogging();
app.Run();
