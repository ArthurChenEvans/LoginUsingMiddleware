using LoginUsingMiddleware.CustomMiddlewares;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseLogin();
app.Run();
