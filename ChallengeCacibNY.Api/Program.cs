using ChallengeCacibNY.Core.Data;
using ChallengeCacibNY.Core.Logic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IStackDataManager, StackDataManager>();
builder.Services.AddScoped<IItemChecker, ItemChecker>();
builder.Services.AddScoped<ICalculator, Calculator>();
builder.Services.AddScoped<IStackAdder, StackAdder>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
