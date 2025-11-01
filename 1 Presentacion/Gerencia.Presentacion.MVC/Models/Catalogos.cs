using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gerencia.Presentacion.MVC.Models
{
    public class TipoPuesto
    {
        [Key]
        [DisplayName("ID Tipo de Puesto")]
        public int PuestoId { get; set; }
        [Required,StringLength(50)]
        public string Nombre { get; set; } = string.Empty;
    }

    public class Estado
    {
        [Key]
        [DisplayName("ID de Estado")]
        public int EstadoId { get; set; }
        [Required, StringLength(20)]
        public string Nombre { get; set; } = string.Empty;
    }

    public class Disponibilidad
    {
        [Key]
        [DisplayName("ID de Disponibilidad")]
        public int DisponibilidadId { get; set; }
        [Required, StringLength(20)]
        public string Nombre { get; set; } = string.Empty;
    }

    public class Importancia
    {
        [Key]
        [DisplayName("ID de Importancia")]
        public int ImportanciaId { get; set; }
        [Required, StringLength(20)]
        public string Nombre { get; set; } = string.Empty;
    }
}
