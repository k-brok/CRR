using CRR.Shared.DTOs;
using CRR.Shared.Entities;
using CRR.Shared.Mappers;
using Radzen;
using System.Net.Http.Json;

namespace CRR.APP.Services;

public class TripService
{
    private readonly HttpClient _httpClient;
    private const string Endpoint = "api/trips";

    public TripService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Trip>> GetAllAsync()
    {
        List<TripDto> TripDtos = await _httpClient.GetFromJsonAsync<List<TripDto>>(Endpoint);
        List<Trip> TripList = TripDtos.Select(t => t.ToEntity()).ToList();
        return TripList;
    }

    public async Task<List<Trip>> GetFromYear(string Year = null)
    {
        if (Year == null)
            Year = DateTime.Now.Year.ToString();
        List<TripDto> TripDtos = await _httpClient.GetFromJsonAsync<List<TripDto>>($"{Endpoint}/FromYear?year={Year}");
        List<Trip> TripList = TripDtos.Select(t => t.ToEntity()).ToList();
        return TripList;
    }
    public async Task<bool> CreateAsync(Trip trip)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoint, trip.ToDto());
        return response.IsSuccessStatusCode;
    }

    public async Task<Trip> GetLatestAsync()
    {
        var response = await _httpClient.GetAsync(Endpoint + "/latest");
        if (response.IsSuccessStatusCode)
        {
            var LatestTrip = await response.Content.ReadFromJsonAsync<TripDto>();
            return LatestTrip.ToEntity();
        }
        return null;
    }
    public async Task<IEnumerable<Trip>> GetFilteredAsync(
        Guid? id = null,
        Guid? fromId = null,
        Guid? toId = null,
        string? remark = null,
        int? departureMileage = null,
        int? arrivalMileage = null,
        DateTime? departureFrom = null,
        DateTime? departureTo = null,
        DateTime? arrivalFrom = null,
        DateTime? arrivalTo = null,
        int? privateMileage = null,
        Guid? carId = null,
        string? carPlateNumber = null)
    {
        var queryParams = new List<string>();

        if (id.HasValue) queryParams.Add($"id={id}");
        if (fromId.HasValue) queryParams.Add($"fromId={fromId}");
        if (toId.HasValue) queryParams.Add($"toId={toId}");
        if (!string.IsNullOrWhiteSpace(remark)) queryParams.Add($"remark={Uri.EscapeDataString(remark)}");
        if (departureMileage.HasValue) queryParams.Add($"departureMileage={departureMileage}");
        if (arrivalMileage.HasValue) queryParams.Add($"arrivalMileage={arrivalMileage}");
        if (departureFrom.HasValue) queryParams.Add($"departureFrom={departureFrom:O}");
        if (departureTo.HasValue) queryParams.Add($"departureTo={departureTo:O}");
        if (arrivalFrom.HasValue) queryParams.Add($"arrivalFrom={arrivalFrom:O}");
        if (arrivalTo.HasValue) queryParams.Add($"arrivalTo={arrivalTo:O}");
        if (privateMileage.HasValue) queryParams.Add($"privateMileage={privateMileage}");
        if (carId.HasValue) queryParams.Add($"carId={carId}");
        if (!string.IsNullOrWhiteSpace(carPlateNumber)) queryParams.Add($"carPlateNumber={Uri.EscapeDataString(carPlateNumber)}");

        var url = Endpoint;
        if (queryParams.Any())
            url += "/Filter?" + string.Join("&", queryParams);

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            //Console.WriteLine(response.Content);
            return Enumerable.Empty<Trip>();
        }
            

        var tripDtos = await response.Content.ReadFromJsonAsync<IEnumerable<TripDto>>();
        return tripDtos.Select(dto => dto.ToEntity());
    }
}
