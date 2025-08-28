using CRR.Shared.DTOs;
using CRR.Shared.Entities;
using CRR.Shared.Mappers;
using System.Net.Http.Json;

namespace CRR.APP.Services;

public class CarService
{
    private readonly HttpClient _httpClient;
    private const string Endpoint = "api/Cars";

    public CarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Car> GetAsync(Guid CarId)
    {
        CarDto CarDtos = await _httpClient.GetFromJsonAsync<CarDto>(Endpoint + "/" + CarId);
        return CarDtos.ToEntity();
    }
    public async Task<List<Car>> GetAllAsync()
    {
        List<CarDto> CarDtos = await _httpClient.GetFromJsonAsync<List<CarDto>>(Endpoint);
        List<Car> CarList = CarDtos.Select(t => t.ToEntity()).ToList();
        return CarList;
    }
    public async Task<Car> GetDefaultAsync()
    {
        var result = await _httpClient.GetAsync(Endpoint + "/default");
        if (result.IsSuccessStatusCode)
        {
            var DefaultCar = await result.Content.ReadFromJsonAsync<CarDto>();
            return DefaultCar.ToEntity();
        }
        return null;
    }

    public async Task<bool> CreateAsync(Car Car)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoint, Car.ToDto());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SaveAsync(Car Car)
    {
        var response = await _httpClient.PutAsJsonAsync(Endpoint + "/" + Car.Id, Car.ToDto());
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetDefaultAsync(Car Car)
    {
        var response = await _httpClient.PutAsJsonAsync(Endpoint + "/default/" + Car.Id, Car.ToDto());
        return response.IsSuccessStatusCode;
    }

    public async Task<Car> GetByPlatenumberAsync(string Platenumber)
    {
        var result = await _httpClient.GetAsync(Endpoint + "/PlateNumber/" + Platenumber);
        if (result.IsSuccessStatusCode)
        {
            var DefaultCar = await result.Content.ReadFromJsonAsync<CarDto>();
            return DefaultCar.ToEntity();
        }
        return null;
    }
}
