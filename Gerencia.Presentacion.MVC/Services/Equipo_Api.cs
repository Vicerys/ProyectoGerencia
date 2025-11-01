using Gerencia.Core.Dtos;

namespace Gerencia.Presentacion.MVC.Services
{
    public class Equipo_Api
    {
        private readonly HttpClient _http;
        public Equipo_Api(HttpClient http) => _http = http;

        public async Task<List<EquipoDto>> GetAllAsync()
            => await _http.GetFromJsonAsync<List<EquipoDto>>("api/Equipo") ?? new();

        public async Task<EquipoDto?> GetByIdAsync(int id)
            => await _http.GetFromJsonAsync<EquipoDto>($"api/Equipo/{id}");

        public async Task<EquipoDto?> CreateAsync(EquipoDto equipo)
        {
            var resp = await _http.PostAsJsonAsync("api/proyecto", equipo);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<EquipoDto>();
        }

        public async Task UpdateAsync(EquipoDto equipo)
        {
            var resp = await _http.PutAsJsonAsync($"api/Equipo/{equipo.EquipoId}", equipo);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/Equipo/{id}");
            resp.EnsureSuccessStatusCode();
        }
    }
}
