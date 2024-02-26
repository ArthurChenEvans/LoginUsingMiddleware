using LoginUsingMiddleware.CustomMiddlewares;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<LoginMiddleware>();  
var app = builder.Build();

app.UseWhen(
    context => context.Request.Method == "POST",
    app =>
    {
        app.UseLogin();
    });

app.UseWhen(
    context => context.Request.Method == "GET",
    app => app.Run(async context =>
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("No response");
    })
);
    
app.UseLogin();
app.Run();
