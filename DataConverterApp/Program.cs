using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataConverterApp.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<HistoryService>();
builder.Services.AddScoped<ConversionService>();


builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


var app = builder.Build();


var supportedCultures = new[] { "en-US", "uk-UA" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("uk-UA")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();