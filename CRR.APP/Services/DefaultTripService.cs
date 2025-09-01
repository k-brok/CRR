using CRR.Shared.DTOs;
using CRR.Shared.Entities;
using CRR.Shared.Mappers;
using System.Net.Http.Json;

namespace CRR.APP.Services;

public class DefaultTripService
{
    private readonly HttpClient _httpClient;
    private const string Endpoint = "api/defaultTrips";

    public DefaultTripService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DefaultTrip>> GetAllAsync()
    {
        List<DefaultTripDto> DefaultTripDtos = await _httpClient.GetFromJsonAsync<List<DefaultTripDto>>(Endpoint);
        List<DefaultTrip> DefaultTripList = DefaultTripDtos.Select(t => t.ToEntity()).ToList();
        return DefaultTripList;
    }
    public async Task<DefaultTrip?> GetFromAddresses(Guid From, Guid To)
    {
        var result = await _httpClient.GetAsync($"{Endpoint}/FromAddresses?from={From}&to={To}");
        if (result.IsSuccessStatusCode)
        {
            DefaultTripDto tripdto = await result.Content.ReadFromJsonAsync<DefaultTripDto>();
            return tripdto.ToEntity();
        }
        return null;
    }

    public async Task<bool> CreateAsync(DefaultTrip defaultTrip)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoint, defaultTrip.ToDto());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SaveAsync(DefaultTrip defaultTrip)
    {
        var response = await _httpClient.PutAsJsonAsync(Endpoint + "/" + defaultTrip.Id, defaultTrip.ToDto());
        return response.IsSuccessStatusCode;
    }
}
