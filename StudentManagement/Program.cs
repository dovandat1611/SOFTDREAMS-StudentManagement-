using Grpc.Net.Client.Web;
using ProtoBuf.Grpc.Client;
using Grpc.Net.Client;
using AntDesign;
using StudentManagement.Common.IServices;


var builder = WebApplication.CreateBuilder(args);

string grpcUrl = builder.Configuration.GetSection("GrpcServer")["Url"] ?? throw new InvalidOperationException("Cannot find gRPC URL!");

// AntDesign
builder.Services.AddAntDesign();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddSingleton(serviceProvider =>
{
    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
    return GrpcChannel.ForAddress(grpcUrl, new GrpcChannelOptions { HttpHandler = httpHandler });
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

await app.RunAsync();
