using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using StudentManagement.NHibernate.Mappings;
using StudentManagement.Service.Services;
using StudentManagement.NHibernate.UnitOfWork;
using StudentManagement.Common.AutoMapper;
using ProtoBuf.Grpc.Server;
using StudentManagement.Common.IServices;
using Grpc.AspNetCore.Web;

var builder = WebApplication.CreateBuilder(args);


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


// Inject ISession
builder.Services.AddScoped(factory =>
{
    var sessionFactory = factory.GetRequiredService<ISessionFactory>();
    return sessionFactory.OpenSession();
});

// Inject Repo
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AUTO MAPPER
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


// Proto Service
builder.Services.AddScoped<ISinhVienProto, SinhVienService>();
builder.Services.AddScoped<ILopHocProto, LopHocService>();
builder.Services.AddScoped<IGiaoVienProto, GiaoVienService>();

//// gRPC Configuration
builder.Services.AddGrpc();
builder.Services.AddCodeFirstGrpc();
builder.Services.AddGrpcReflection();

var app = builder.Build();

app.UseGrpcWeb(); 

app.MapGrpcService<GiaoVienService>().EnableGrpcWeb();
app.MapGrpcService<LopHocService>().EnableGrpcWeb();
app.MapGrpcService<SinhVienService>().EnableGrpcWeb();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
