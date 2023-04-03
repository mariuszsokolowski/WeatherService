using Microsoft.EntityFrameworkCore;
using WeatherService.Data;
using WeatherService.Data.Automapper;
using WeatherService.Data.Services.CityServices;
using WeatherService.Data.Services.WeatherServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Get ConfigruationManger for JSON in appsettings.json
ConfigurationManager configuration = builder.Configuration;

//TODO: change default connection to userSercret
//Set DBContext for application
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

//Add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(CityProfile));


//TODO: Change DI to Autofac
//Add DI to applicationA
builder.Services.AddTransient<IWeatherService, WeatherService.Data.Services.WeatherServices.WeatherService>();
builder.Services.AddTransient<ICityService, CityService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
