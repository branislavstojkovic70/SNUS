using Microsoft.EntityFrameworkCore;
using SNUS_PROJECT;
using SNUS_PROJECT.Data;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<Seed>();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Singleton);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddScoped<IAlarmRepository, AlarmRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAnalogInputRepository, AnalogInputRepository>();
builder.Services.AddScoped<IAnalogOutputRepository, AnalogOutputRepository>();
builder.Services.AddScoped<IDigitalOutputRepository, DigitalOutputRepository>();
builder.Services.AddScoped<IDigitalInputRepository, DigitalInputRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

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
