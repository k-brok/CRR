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
		
        builder.Services.AddScoped<AddressService>();
        builder.Services.AddScoped<TripService>();

		builder.Services.AddRadzenComponents();

        await builder.Build().RunAsync();
    }
}
