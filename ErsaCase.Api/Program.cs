using ErsaCase.Repository;
using ErsaCase.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = builder.Configuration;
//IWebHostEnvironment environment = builder.Environment;

// Add services to the container.


//? Jwt token configuration
//var JwtKey = Encoding.ASCII.GetBytes(Encoding.UTF8.GetString(Convert.FromBase64String(Configuration["TokenSecret"])));
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options => {
//        options.RequireHttpsMetadata = false;
//        options.SaveToken = true;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(JwtKey),
//            ValidateIssuer = false,
//            ValidateAudience = false,
//            //ValidateLifetime=true // E�er s�resi ge�mi� bir token gelirse invalid yap�lmas�n� sa�l�yor.
//        };
//    });

//// Custome Authorization Handler
//builder.Services.AddAuthorization(options => {
//    options.AddPolicy("ActionUserPolicy", policy => {
//        policy.AddRequirements(new AuthorizationRequirement());
//    });
//});


//// Add AddHttpContextAccessor for Handler
//builder.Services.AddHttpContextAccessor();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddRepositories(Configuration);
builder.Services.AddCustomeServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
