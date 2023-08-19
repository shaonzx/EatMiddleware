var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


//short circuiting middleware
/*app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Eat the first response");
});
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Eat the second response");
});*/

//chaining middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Eat the first response");
    //will short circuit if bellow request delegate is excluded.
    await next(context);
});

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("\nEat the Second response");
    await next(context);
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("\nEat the short circuit");
});

app.Run();
