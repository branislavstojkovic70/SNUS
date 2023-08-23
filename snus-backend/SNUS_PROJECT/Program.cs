using Microsoft.EntityFrameworkCore;
using SNUS_DRIVER;
using SNUS_PROJECT;
using SNUS_PROJECT.Data;
using SNUS_PROJECT.Hubs;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<Seed>();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Singleton);
builder.Services.AddControllers();
builder.Services.AddScoped<IAlarmRepository, AlarmRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAnalogInputRepository, AnalogInputRepository>();
builder.Services.AddScoped<IAnalogOutputRepository, AnalogOutputRepository>();
builder.Services.AddScoped<IDigitalOutputRepository, DigitalOutputRepository>();
builder.Services.AddScoped<IDigitalInputRepository, DigitalInputRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<DataSender>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowCredentials().AllowAnyHeader();
        });
});

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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors("AllowOrigins");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.MapHub<AlarmHub>("/hubs/alarmHub");
app.MapHub<TagHub>("/hubs/tagHub");

app.Run();