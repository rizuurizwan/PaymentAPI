//using Microsoft.CodeAnalysis.Options;
//using Microsoft.EntityFrameworkCore;
//using PaymentAPI.Model;
//using Microsoft.AspNetCore.Hosting;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<PaymentDetailContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

////deploy
////builder.WebHost.ConfigureKestrel(options =>
////{
////    options.ListenAnyIP(5000); // or use port from environment
////});

////var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

////


//var app = builder.Build();
////app.Urls.Add($"http://*:{port}");
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseCors(Option => Option.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
//app.UseStaticFiles();
//app.UseAuthorization();

//app.MapControllers();
//app.UseStaticFiles();

//app.Run();


using Microsoft.EntityFrameworkCore;
using PaymentAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// DB context
builder.Services.AddDbContext<PaymentDetailContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
        policy.WithOrigins("http://localhost:4200", "https://jernastan.netlify.app")
              .AllowAnyMethod()
              .AllowAnyHeader());
});
var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}

// Enable Swagger **in all environments**, not just development
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

//app.UseCors("AllowSpecificOrigins");
app.UseCors(Option => Option.WithOrigins("http://localhost:4200", "https://jernastan.netlify.app").AllowAnyMethod().AllowAnyHeader());
app.UseStaticFiles();

app.UseAuthorization();

// Add this endpoint for a quick test in the browser
app.MapGet("/", () => "PaymentAPI is running...");

app.MapControllers();

app.Run();

