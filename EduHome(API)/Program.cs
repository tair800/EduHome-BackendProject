using EduHome_API_.Data;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

//builder.Services.AddControllers();
//builder.Services.AddFluentValidationAutoValidation()
//    .AddFluentValidationClientsideAdapters()
//    .AddValidatorsFromAssemblyContaining<ProductCreateValidator>();

builder.Services.AddFluentValidationRulesToSwagger();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
//{
//    options.Password = new()
//    {
//        RequiredLength = 8,
//        RequireUppercase = true,
//        RequireLowercase = true,
//        RequireDigit = true,
//        RequireNonAlphanumeric = true
//    };
//    //options.Lockout = new()
//    //{
//    //    MaxFailedAccessAttempts = 5,
//    //    AllowedForNewUsers = true,
//    //    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5)
//    //};
//    //options.User = new()
//    //{
//    //    //todo:email confirm sondurmusen
//    //    RequireUniqueEmail = true,
//    //};
//    //options.SignIn.RequireConfirmedEmail = true;

//}).AddDefaultTokenProviders().AddEntityFrameworkStores<ShopAppDbContext>();



builder.Services.AddDbContext<EduApiDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));

});

builder.Services.AddHttpContextAccessor();
//builder.Services.AddAutoMapper(opt =>
//      opt.AddProfile(new MapperProfile(new HttpContextAccessor()))
//);
//builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly); birden chox automapper elave etmek uchun ancag bunu ishdetmek bes edir

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

//builder.Services.Configure<JwtSettings>(config.GetSection("Jwt"));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Shop App (API)",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
