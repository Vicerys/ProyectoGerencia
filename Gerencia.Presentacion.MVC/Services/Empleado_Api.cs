using Gerencia.Core.Dtos;

namespace Gerencia.Presentacion.MVC.Services
{
    public class Empleado_Api
    {
        private readonly HttpClient _http;
        public Empleado_Api(HttpClient http)
        {

            _http = http; 
        }
            

        public async Task<List<EmpleadoDto>> GetAllAsync()
            => await _http.GetFromJsonAsync<List<EmpleadoDto>>("api/Empleado");

        public async Task<EmpleadoDto?> GetByIdAsync(int id)
            => await _http.GetFromJsonAsync<EmpleadoDto>($"api/Empleado/{id}");

        public async Task<EmpleadoDto?> CreateAsync(EmpleadoDto empleado)
        {
            var resp = await _http.PostAsJsonAsync("api/Empleado/AgregarEmpleado", empleado);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<EmpleadoDto>();
        }

        public async Task UpdateAsync(EmpleadoDto empleado)
        {
            var resp = await _http.PutAsJsonAsync($"api/empleado/ActualizarEmpleado", empleado);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/Empleado/EliminarEmpleado");
            resp.EnsureSuccessStatusCode();
        }

        public async Task<EmpleadoDto?> LoginAsync(InicioSesionDto login)
        {
            var resp = await _http.PostAsJsonAsync("api/Empleado/IniciodeSesiones", login);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<EmpleadoDto>();
        }
    }
}
