using System.ComponentModel.DataAnnotations;
using Gerencia.Core.Repositorios.Entidades;

namespace Gerencia.Presentacion.MVC.Models.ViewModels.Proyecto
{
    public class ProyectoFormViewModel
    {
        [Required]
        public ProyectoEntidad Proyecto { get; set; } = new ProyectoEntidad();
    }
}

