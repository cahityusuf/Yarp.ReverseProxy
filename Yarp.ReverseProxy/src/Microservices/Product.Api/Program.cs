var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "iPhone 8", "iPhone 11", "iPhone 12", "iPhone 13", "iPhone 14"
};

app.MapGet("/getall", () =>
{
    var forecast = summaries.ToArray();

    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();
