using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Model;
using Microsoft.AspNetCore.Hosting;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PaymentDetailContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(Option => Option.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();

app.Run();
