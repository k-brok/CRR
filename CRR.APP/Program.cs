using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using CRR.APP.Services;
namespace CRR.APP;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });
		
        builder.Services.AddHttpClient("PublicApi", client =>
        {
            client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        });

        builder.Services.AddHttpClient("openStreetMaps", client => client.BaseAddress = new Uri("https://nominatim.openstreetmap.org/"));
		
        builder.Services.AddScoped<AddressService>();
        builder.Services.AddScoped<TripService>();
        builder.Services.AddScoped<CarService>();
        builder.Services.AddScoped<DefaultTripService>();

        builder.Services.AddScoped<GPSService>();

        builder.Services.AddGeolocationServices();

		builder.Services.AddRadzenComponents();

        await builder.Build().RunAsync();
    }
}
