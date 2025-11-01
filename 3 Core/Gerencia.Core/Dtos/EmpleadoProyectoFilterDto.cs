namespace Gerencia.Core.Dtos
{
    public class EmpleadoProyectoFilterDto
    {
        public int? EmpleadoProyectoId { get; set; }
        public string? NombreLike { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; } = 0;
    }
}