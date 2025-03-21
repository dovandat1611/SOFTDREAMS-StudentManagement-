using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NHibernate;
using NHibernate.Cfg;
using StudentManagement.gRPC.Services;
using StudentManagement.NHibernate;
using StudentManagement.NHibernate.Mappings;
using StudentManagement.NHibernate.UnitOfWork;
using StudentManagement.gRPC.AutoMapper;
using ProtoBuf.Grpc.Client;
using StudentManagement.gRPC.IServices;
using StudentManagement.NHibernate.IRepositories;
using StudentManagement.NHibernate.Repositories;
using ProtoBuf.Grpc.Server;
using AntDesign;

var builder = WebApplication.CreateBuilder(args);


// AntDesign
builder.Services.AddAntDesign();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


// NHibernate Configuration
builder.Services.AddSingleton<ISessionFactory>(provider =>
{
    return Fluently.Configure()
        .Database(MsSqlConfiguration.MsSql2012
            .ConnectionString(builder.Configuration.GetConnectionString("DefautConnection"))
            .ShowSql())
        .Mappings(m => m.FluentMappings
        .AddFromAssemblyOf<SinhVienMap>()
        .AddFromAssemblyOf<GiaoVienMap>()
        .AddFromAssemblyOf<LopHocMap>()
        )
        .BuildSessionFactory();
});


// Đăng ký ISession
builder.Services.AddScoped(factory =>
{
    var sessionFactory = factory.GetRequiredService<ISessionFactory>();
    return sessionFactory.OpenSession();
});

builder.Services.AddScoped<INotificationService, NotificationService>();


// Repository
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSingleton(serviceProvider =>
{
    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
    return GrpcChannel.ForAddress("https://localhost:7275", new GrpcChannelOptions { HttpHandler = httpHandler });
});

builder.Services.AddSingleton<ISinhVienProto>(serviceProvider =>
{
    var channel = serviceProvider.GetRequiredService<GrpcChannel>(); 
    return channel.CreateGrpcService<ISinhVienProto>();
});

builder.Services.AddSingleton<ILopHocProto>(serviceProvider =>
{
    var channel = serviceProvider.GetRequiredService<GrpcChannel>();
    return channel.CreateGrpcService<ILopHocProto>();
});

builder.Services.AddSingleton<IGiaoVienProto>(serviceProvider =>
{
    var channel = serviceProvider.GetRequiredService<GrpcChannel>();
    return channel.CreateGrpcService<IGiaoVienProto>();
});


// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// gRPC Configuration

builder.Services.AddGrpc();
builder.Services.AddCodeFirstGrpc();
builder.Services.AddGrpcReflection();

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
app.UseGrpcWeb();
app.MapGrpcService<SinhVienService>().EnableGrpcWeb();
app.MapGrpcService<LopHocService>().EnableGrpcWeb();
app.MapGrpcService<GiaoVienService>().EnableGrpcWeb();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
