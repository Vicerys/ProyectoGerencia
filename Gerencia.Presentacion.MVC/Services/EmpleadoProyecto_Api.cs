using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Infrastructure;

namespace Gerencia.Presentacion.MVC.Services
{
    public class EmpleadoProyecto_Api
    {
        private readonly HttpClient _http;
        public EmpleadoProyecto_Api(HttpClient http) => _http = http;

        public async Task<List<EmpleadoProyectoDto>> GetAllAsync()
            => await _http.GetFromJsonAsync<List<EmpleadoProyectoDto>>("apiEmpleadoProyecto") ?? new();

        public async Task<EmpleadoProyectoDto?> GetByIdAsync(int id)
            => await _http.GetFromJsonAsync<EmpleadoProyectoDto>($"api/EmpleadoProyecto/{id}");

        public async Task<EmpleadoProyectoDto?> CreateAsync(EmpleadoProyectoDto empleadoProyecto)
        {
            var resp = await _http.PostAsJsonAsync("api/EmpleadoProyecto/Agregar", empleadoProyecto);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<EmpleadoProyectoDto>();
        }

        public async Task UpdateAsync(EmpleadoProyectoDto empleadoProyecto)
        {
            var resp = await _http.PutAsJsonAsync($"api/EmpleadoProyecto/Actualizar", empleadoProyecto);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/EmpleadoProyecto/Eliminar");
            resp.EnsureSuccessStatusCode();
        }

        public async Task<PaginatedList<EmpleadoProyectoDto>> GetFilteredAsync(EmpleadoProyectoFilterDto filtro)
        {
            var resp = await _http.PostAsJsonAsync("api/EmpleadoProyecto/Filtrar", filtro);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<PaginatedList<EmpleadoProyectoDto>>();
        }
    }
}
