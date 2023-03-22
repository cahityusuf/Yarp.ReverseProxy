using Microsoft.OpenApi.Models;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Reverse Proxy",
//        Version = "v1",
//        Description = "Gateway"
//    });
//});

builder.Configuration.AddJsonFile(
    "appsettings.Production.json");

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        b =>
        {
            b.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(transforms =>
    {
        transforms.AddRequestTransform(transform =>
        {
            var correlationId = Guid.NewGuid().ToString("N");
            transform.ProxyRequest.Headers.Add("x-request-id", correlationId);

            return ValueTask.CompletedTask;
        });
    });


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Koç Üniversitesi ReverseProxy Server");

app.UseCors();

app.MapReverseProxy();

//app.UseSwagger()
//    .UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint($"/customer/swagger/v1/swagger.json",
//            "Customer Api");
//        c.SwaggerEndpoint($"/product/swagger/v1/swagger.json",
//            "Product Api");
//    });

app.Run();