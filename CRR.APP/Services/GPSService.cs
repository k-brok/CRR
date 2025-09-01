using Microsoft.JSInterop;
using Radzen;
namespace CRR.APP.Services;

public class GPSService
{
    private readonly IGeolocationService geolocationService;
    private readonly NotificationService _notify;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    private bool IsRunning { get; set; } = false;
    public GPSService(IGeolocationService _gps, NotificationService notificationService)
    {
        geolocationService = _gps;
        _notify = notificationService;
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
    }
    [JSInvokable]
    public void OnPositionError(GeolocationPositionError positionError)
    {
        IsRunning = false;
        _notify.Notify(new NotificationMessage{
                Severity = NotificationSeverity.Error,
                Summary = "GPS error!",
                Detail = "Kan GPS locatie niet vinden!"
            });
    }
}