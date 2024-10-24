using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using SalesManagementApi.ExceptionHandling;
using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Repo;
using SalesManagementLibrary.Repo.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()
    );
});

builder.Services.AddControllers(
//    options =>
//{
//    options.Filters.Add(new ValidateModelAttribute());
//}
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthorization(opts =>
//{
//    opts.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
//});

//builder
//    .Services.AddAuthentication("Bearer")
//    .AddJwtBearer(opts =>
//    {
//        opts.TokenValidationParameters = new()
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
//            ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.ASCII.GetBytes(
//                    builder.Configuration.GetValue<string>("Authentication:SecretKey")
//                )
//            )
//        };
//    });

builder.Services.AddSingleton<IDapperDataAccess, DapperDataAccess>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IPaymentStatusRepository, PaymentStatusRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<ICustomerTypeRepository, CustomerTypeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

// Add cutom exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
