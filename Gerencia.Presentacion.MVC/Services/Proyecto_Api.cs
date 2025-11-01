using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Infrastructure;
using System.Net.Http.Json;

namespace Gerencia.Presentacion.MVC.Services
{
    public class Proyecto_Api
    {
        private readonly HttpClient _http;
        public Proyecto_Api(HttpClient http) => _http = http;

        public async Task<List<ProyectoDto>> GetAllAsync()
            => await _http.GetFromJsonAsync<List<ProyectoDto>>("api/proyecto") ?? new();

        public async Task<ProyectoDto?> GetByIdAsync(int id)
            => await _http.GetFromJsonAsync<ProyectoDto>($"api/proyecto/{id}");

        public async Task<ProyectoDto?> CreateAsync(ProyectoDto proyecto)
        {
            var resp = await _http.PostAsJsonAsync("api/proyecto", proyecto);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<ProyectoDto>();
        }

        public async Task UpdateAsync(ProyectoDto proyecto)
        {
            var resp = await _http.PutAsJsonAsync($"api/proyecto/{proyecto.ProyectoId}", proyecto);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/proyecto/{id}");
            resp.EnsureSuccessStatusCode();
        }

        public async Task<PaginatedList<ProyectoDto>> GetFilteredAsync(ProyectoFilterDto filtro)
        {
            var resp = await _http.PostAsJsonAsync("api/proyecto/filtrar", filtro);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<PaginatedList<ProyectoDto>>();
        }
    }
}
