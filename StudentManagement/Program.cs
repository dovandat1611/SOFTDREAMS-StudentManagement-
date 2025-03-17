using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NHibernate;
using NHibernate.Cfg;
using StudentManagement.Data;
using StudentManagement.NHibernate;
using StudentManagement.NHibernate.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddSingleton<ISessionFactory>(provider =>
{
    var cfg = new Configuration();
    cfg.Configure(@"D:\SOFTDREAMS-TRAINING\StudentManagement\StudentManagement.NHibernate\ConfigDB\nhibernate.cfg.xml");
    cfg.AddFile(@"D:\SOFTDREAMS-TRAINING\StudentManagement\StudentManagement.NHibernate\Mappings\ManageStudent.hdm.xml");
    return cfg.BuildSessionFactory();
});

builder.Services.AddScoped<NHibernate.ISession>(provider =>
    provider.GetRequiredService<ISessionFactory>().OpenSession());

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
