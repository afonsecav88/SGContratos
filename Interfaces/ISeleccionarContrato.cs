using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaGestionContratos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestionContratos.Services
{

    public interface ISeleccionarContrato
    {
        public  List<SelectListItem> RetornarListadoContratos();

    }
}
