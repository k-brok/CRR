using Microsoft.JSInterop;
using Radzen;
namespace CRR.APP.Services;

public class GPSService
{
    private readonly IGeolocationService geolocationService;
    private readonly NotificationService _notify;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public GPSService(IGeolocationService _gps,NotificationService notificationService)
    {
        geolocationService = _gps;
        _notify = notificationService;
        geolocationService.GetCurrentPosition(this,nameof(OnPositionReceived),nameof(OnPositionError));
    }
    [JSInvokable]
    public void OnPositionReceived(GeolocationPosition position)
    {
        Latitude = position.Coords.Latitude;
    }
    [JSInvokable]
    public void OnPositionError(GeolocationPositionError positionError)
    {
        _notify.Notify(new NotificationMessage{
                Severity = NotificationSeverity.Error,
                Summary = "GPS error!",
                Detail = "Kan GPS locatie niet vinden!"
            });
    }
}