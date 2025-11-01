using Gerencia.Core.Dtos;
using System.Net.Http.Json;

namespace Gerencia.Presentacion.MVC.Services
{
    public class Proceso_Api
    {
        private readonly HttpClient _http;
        public Proceso_Api(HttpClient http) => _http = http;

        public async Task<List<ProcesoDto>> GetAllAsync()
            => await _http.GetFromJsonAsync<List<ProcesoDto>>("api/Proceso") ?? new();

        public async Task<ProcesoDto?> GetByIdAsync(int id)
            => await _http.GetFromJsonAsync<ProcesoDto>($"api/Proceso/{id}");

        public async Task<ProcesoDto?> CreateAsync(ProcesoDto proceso)
        {
            var resp = await _http.PostAsJsonAsync("api/Proceso", proceso);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<ProcesoDto>();
        }

        public async Task UpdateAsync(ProcesoDto proceso)
        {
            var resp = await _http.PutAsJsonAsync($"api/Proceso/{proceso.ProcesoId}", proceso);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/Proceso/{id}");
            resp.EnsureSuccessStatusCode();
        }
    }
}
