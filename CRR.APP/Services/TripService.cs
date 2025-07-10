using CRR.Shared.DTOs;
using CRR.Shared.Entities;
using CRR.Shared.Mappers;
using System.Net.Http.Json;

namespace CRR.APP.Services;

public class TripService
{
    private readonly HttpClient _httpClient;
    private const string Endpoint = "api/tripes";

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

    public async Task<bool> CreateAsync(Trip trip)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoint, trip.ToDto());
        return response.IsSuccessStatusCode;
    }

    // etc...
}
