using GameOfLife.Domain.Dto;
using GameOfLife.Domain.Enumerations;
using GameOfLife.Domain.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameOfLife.Infrastructure.Services
{
    public class FieldsService : IFieldsService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public FieldsService(
            HttpClient httpClient,
            JsonSerializerOptions jsonSerializerOptions
        )
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public async Task<Guid> Add(UInt16 size, Byte density, BehaviorEnum behaviorEnum)
        {
            String requestContent = JsonSerializer.Serialize(new { size, density, behaviorEnum });

            var response = await _httpClient.PostAsync("/fields/", new StringContent(requestContent, Encoding.UTF8, "application/json"));

            String responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                String guidString = responseContent.Trim('"');

                return new Guid(guidString);
            }
            else
            {
                throw new HttpRequestException("Failed to add new field", new Exception(responseContent));
            }
        }

        public async Task<FieldDto> Get(Guid id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"fields/{id}");

            String responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<FieldDto>(responseContent, _jsonSerializerOptions);
            }
            else
            {
                throw new HttpRequestException("Failed to get field", new Exception(responseContent));
            }
        }

        public async Task Next(Guid id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"fields/next/{id}");

            String responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                //
            }
            else
            {
                throw new HttpRequestException("Failed to next field", new Exception(responseContent));
            }
        }

        public async Task Delete(Guid id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"fields/{id}");

            String responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                //
            }
            else
            {
                throw new HttpRequestException("Failed to delete field", new Exception(responseContent));
            }

        }
    }
}
