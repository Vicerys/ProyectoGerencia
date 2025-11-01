using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Gerencia.Presentacion.MVC.Models;

namespace Gerencia.Presentacion.MVC.Models.ViewModels.Proyecto
{
    public class ProyectoFormViewModel
    {
        [Required]
        public Models.Proyecto Proyecto { get; set; } = new Models.Proyecto();
    }
}

