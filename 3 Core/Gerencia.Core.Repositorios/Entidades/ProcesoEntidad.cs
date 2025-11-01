using System.ComponentModel.DataAnnotations;

namespace Gerencia.Core.Repositorios.Entidades
{
    [Flags]
    public enum noDispositivos
    {
        Ninguno = 0,
        Mensajero = 1 << 0,
        Bicicleta = 1 << 2, //=2
        Motocicleta = 1 << 2, //=4
        Automovil = 1 << 3, //8
        Camioneta = 1 << 4, //16
        Trailer = 1 << 5, //32
    }
    public enum EstadoEnum
    {
        Incompleto = 1,
        EnCurso = 2,
        Completado = 3
    }

    public enum ProyectosEj
    {
        Proyecto1 = 7,
        Proyecto2 = 2,
        Proyecto3 = 3
    }

    public class ProcesoEntidad
    {
        [Display(Name = "ID")]
        public int ProcesoId { get; set; }

        [Display(Name = "Proyecto")]
        public int ProyectoId { get; set; }
        //public Proyecto? Proyecto { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Importancia")]
        [Required]
        public int ImportanciaId { get; set; }
        public Importancia? Importancia { get; set; }

        [Display(Name = "Entregables")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public string? Entregables { get; set; }

        [Display(Name = "Fecha de termino estimada")]
        [DataType(DataType.Date)]
        public DateTime FechaTerminoEstimada { get; set; }

        [Display(Name = "Fecha de termino real")]
        [DataType(DataType.Date)]
        public DateTime? FechaTerminoReal { get; set; }

        [Display(Name = "Estado")]
        public int EstadoId { get; set; }
        public Estado? Estado { get; set; }

        [Display(Name = "Finalizado")]
        public bool Finalizado { get; set; } = false;
    }
}
