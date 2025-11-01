using Gerencia.Core.Dtos;
using System.Net.Http.Json;

namespace Gerencia.Presentacion.MVC.Services
{
    public class Tarea_Api
    {
        private readonly HttpClient _http;
        public Tarea_Api(HttpClient http) => _http = http;

        public async Task<List<TareaDto>> GetAllAsync()
            => await _http.GetFromJsonAsync<List<TareaDto>>("api/Tarea") ?? new();

        public async Task<TareaDto?> GetByIdAsync(int id)
            => await _http.GetFromJsonAsync<TareaDto>($"api/Tarea/{id}");

        public async Task<TareaDto?> CreateAsync(TareaDto tarea)
        {
            var resp = await _http.PostAsJsonAsync("api/Tarea", tarea);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<TareaDto>();
        }

        public async Task UpdateAsync(TareaDto tarea)
        {
            var resp = await _http.PutAsJsonAsync($"api/Tarea/{tarea.TareaId}", tarea);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/Tarea/{id}");
            resp.EnsureSuccessStatusCode();
        }
    }
}
