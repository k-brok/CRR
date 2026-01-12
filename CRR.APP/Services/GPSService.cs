using CRR.Shared.DTOs;
using System.Net.Http;
using Microsoft.JSInterop;

namespace CRR.APP.Services;

public class GPSService
{
    private readonly HttpClient _httpClient;
    private readonly IGeolocationService geolocationService;
    public double? Latitude { get; set; } = null;
    public double? Longitude { get; set; } = null;
    public bool IsRunning { get; set; } = false;

    public event Action OnGPSUpdate;
    public void NotifyGPSUpdate() => OnGPSUpdate?.Invoke();

    public GPSService(IGeolocationService _gps, IHttpClientFactory clientFactory)
    {
        geolocationService = _gps;
        _httpClient = clientFactory.CreateClient("openStreetMaps");
        GetGPSLocation();
    }

    public async Task GetGPSLocation()
    {
        IsRunning = true;
        geolocationService.GetCurrentPosition(this, nameof(OnPositionReceived), nameof(OnPositionError));
        
        while (!IsRunning)
        {
            await Task.Delay(25);
        }
    }

    [JSInvokable]
    public void OnPositionReceived(GeolocationPosition position)
    {
        IsRunning = false;
        Latitude = position.Coords.Latitude;
        Longitude = position.Coords.Longitude;
        GetCurrentAddress();
        NotifyGPSUpdate();
    }

    [JSInvokable]
    public void OnPositionError(GeolocationPositionError positionError)
    {
        IsRunning = false;
        NotifyGPSUpdate();
    }
    
    public async Task<AddressDto?> GetCurrentAddress()
    {
        if(Latitude == null || Longitude == null)
            return null;
        var result = await _httpClient.GetAsync($"reverse?format=json&lat={Latitude}&lon={Longitude}&zoom=18&addressdetails=1");
        Console.WriteLine(await result.Content.ReadAsStringAsync());
        return null;
    }
}