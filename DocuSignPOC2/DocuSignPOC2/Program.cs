
using DocuSignPOC2;
using DocuSignPOC2.Services.IDocuSignEnvelope;
using DocuSignPOC2.Services.IESignAdmin;
using DocuSignPOC2.Services.IUser;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
builder.Services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console().ReadFrom.Configuration(context.Configuration);
});


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

app.MapControllers();

app.Run();
