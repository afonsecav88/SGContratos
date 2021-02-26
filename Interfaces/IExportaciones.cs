using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestionContratos.Interfaces
{
    public interface IExportaciones
    {
        Task<byte[]> ExportarExcel();
    }
}
