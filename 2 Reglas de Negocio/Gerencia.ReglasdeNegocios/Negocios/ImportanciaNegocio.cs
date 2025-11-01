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
    public class ImportanciaNegocio
    {
        private readonly AppDbContext _context;
        public ImportanciaNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ImportanciaDto>> ObtenerTodosAsync()
        {
            List<ImportanciaDto> importanciasDto = new List<ImportanciaDto>();

            var importancias = await _context.Importancias.ToListAsync();

            importanciasDto = importancias.Select(p => new ImportanciaDto
            {
                ImportanciaId = p.ImportanciaId,
                Nombre = p.Nombre
            }).ToList();

            return importanciasDto;
        }
    }
}
