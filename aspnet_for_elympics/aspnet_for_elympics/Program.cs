using Aspnet_for_elympics;
using Aspnet_for_elympics.Controllers;
using Aspnet_for_elympics.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));
builder.Services.AddDbContext<ASPForElympicsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AppConnectionString")));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddHttpClient();
builder.Services.AddScoped<IRandomNumberService, RandomNumberService>();
builder.Services.AddScoped<INumberService, NumberService>();
builder.Services.AddScoped<IRequestManager, RequestManager>();
builder.Services.AddScoped<NumberController>();

builder.Services.AddControllers();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    RunMigration();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

void RunMigration()
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ASPForElympicsDbContext>();
    dbContext.Database.Migrate();
}