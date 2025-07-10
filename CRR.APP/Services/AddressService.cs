using CRR.Shared.DTOs;
using CRR.Shared.Entities;
using CRR.Shared.Mappers;
using System.Net.Http.Json;

namespace CRR.APP.Services;

public class AddressService
{
    private readonly HttpClient _httpClient;
    private const string Endpoint = "api/addresses";

    public AddressService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Address>> GetAllAsync()
    {
        List<AddressDto> AddressDtos = await _httpClient.GetFromJsonAsync<List<AddressDto>>(Endpoint);
        List<Address> AddressList = AddressDtos.Select(t => t.ToEntity()).ToList();
        return AddressList;
    }

    public async Task<bool> CreateAsync(Address address)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoint, address.ToDto());
        return response.IsSuccessStatusCode;
    }

    // etc...
}
