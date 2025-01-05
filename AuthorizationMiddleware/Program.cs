using AuthorizationMiddleware.RequirementAuthorization;
using DemoAPI.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {

        ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
        ValidAudience = builder.Configuration["JWTSettings:Audience"],
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("OnlyAdmin", build =>
    {
        build.RequireRole("Admin");
        build.RequireClaim(ClaimTypes.Email, "sushil01@yopmail.com");
    });
    option.AddPolicy("AdminOrUser", build =>
    {
        build.RequireRole(new string[] {"Admin","User"});
    });
    option.AddPolicy("AdminAndUserr", build =>
    {
        build.RequireRole("Admin");
        build.RequireRole("User");
    });
    option.AddPolicy("RequirementElectronics", option =>
    {
        option.Requirements.Add(new DepartmentRequirement("Administrator"));
    });
});
builder.Services.AddSingleton<IAuthorizationHandler, DepartmentAuthorizationHandler>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<RefreshTokenContext>(option =>
{
    option.UseInMemoryDatabase("RefreshTokenDb");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
