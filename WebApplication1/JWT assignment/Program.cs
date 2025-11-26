using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

var jwtSection = builder.Configuration.GetSection("Jwt");
var key = jwtSection.GetValue<string>("Key") ?? throw new Exception("Missing JWT key");
var issuer = jwtSection.GetValue<string>("Issuer") ?? "JwtRoleDemo";
var audience = jwtSection.GetValue<string>("Audience") ?? "JwtRoleDemoClients";
var durationMinutes = jwtSection.GetValue<int?>("DurationMinutes") ?? 60;

builder.Services.AddControllers();

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

var users = new List<User>
{
    new User { Username = "alice", Password = "password1", Roles = new[] { "User" } },
    new User { Username = "bob", Password = "password2", Roles = new[] { "Admin", "User" } }
};
builder.Services.AddSingleton<IEnumerable<User>>(users);

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }
