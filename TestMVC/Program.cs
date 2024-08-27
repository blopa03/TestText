using Serilog.Events;
using Serilog;
using TestMVC.Services;

var outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} {Message} {NewLine}{Exception}";
Log.Logger = new LoggerConfiguration()
                       .MinimumLevel.Information()
                       .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                       .Enrich.FromLogContext()
                       .WriteTo.File(@$"C:\temp\test\test_.txt", LogEventLevel.Error, outputTemplate, rollingInterval: RollingInterval.Day)
                       .CreateLogger();
try
{

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    // Add services to the container.
    builder.Services.AddControllersWithViews();
    builder.Services.AddScoped<ITextService, TextService>();
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
}
catch (Exception ex)
{
    Log.Fatal($"Error while starting - {ex.Message}");
}
finally
{
    await Log.CloseAndFlushAsync();
}

