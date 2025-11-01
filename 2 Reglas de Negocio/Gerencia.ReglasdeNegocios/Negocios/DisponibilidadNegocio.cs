using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerencia.ReglasdeNegocios.Negocios
{
    public class DisponibilidadNegocio
    {
        private readonly AppDbContext _context;
        public DisponibilidadNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DisponibilidadDto>> ObtenerTodosAsync()
        {
            List<DisponibilidadDto> disponibilidadesDto = new List<DisponibilidadDto>();

            var disponibilidades = await _context.Disponibilidades.ToListAsync();

            disponibilidadesDto = disponibilidades.Select(p => new DisponibilidadDto
            {
                DisponibilidadId = p.DisponibilidadId,
                Nombre = p.Nombre
            }).ToList();

            return disponibilidadesDto;
        }
    }
}
