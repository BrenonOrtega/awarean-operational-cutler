using Awarean.Operational.Cutler.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories(builder.Configuration);

if(builder.Configuration.GetValue<bool>("RunFoodMigrationScript"))
    builder.Services.AddHostedService<MongoDbInserter>();

builder.Host.ConfigureApplicationLogging();

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
