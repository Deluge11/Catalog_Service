using Catalog_Service_Application;
using Catalog_Service_Infrastructure;
using Catalog_Service_API;
using Catalog_Service_API.Filters;
using Catalog_Service_API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(op =>
{
    op.Filters.Add<PermissionBaseAuthorizationFilter>();
});

builder.Services.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.UseMiddleware<CreateClaimIdentityMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
