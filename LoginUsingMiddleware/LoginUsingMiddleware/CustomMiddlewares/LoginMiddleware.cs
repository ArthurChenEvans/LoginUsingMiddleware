using Microsoft.Extensions.Primitives;

namespace LoginUsingMiddleware.CustomMiddlewares
{
    public class LoginMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            StreamReader reader = new StreamReader(context.Request.Body);

            var body = await reader.ReadToEndAsync();

            Dictionary<string, StringValues> queryParams =
                Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);


            if (queryParams != null)
            {
                bool emailIsValid = false;
                bool passwordIsValid = false;

                foreach (var kvp in queryParams)
                {
                    if (kvp.Key == "email" && kvp.Value == "admin@example.com")
                    {
                        emailIsValid = true;
                    }
                    else if (kvp.Key == "password" && kvp.Value == "admin1234")
                    {
                        passwordIsValid = true;
                    }
                }

                if (emailIsValid && passwordIsValid)
                {
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync("Successful login");
                }
                else
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Invalid login");
                }
            }

            await next(context);
        }
    }

    public static class Login
    {
        public static IApplicationBuilder LoginExtension(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoginMiddleware>();
        }
    }
}
