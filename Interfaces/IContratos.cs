using SistemaGestionContratos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaGestionContratos.Interfaces
{
    public interface IContratos
    {

   
        Task CrearContrato(Contratos contratos);


        Task<Contratos> EliminarContrato(int? id);


        Task EliminarContrato(int id);


        Task<Contratos> EditarContrato(int? id);


        Task EditarContrato(Contratos contratos);


        Task<Contratos> DetallarContrato(int? id);



        bool ExisteContrato(int id);


        Task<List<Contratos>> ListarContratos();

  
        IQueryable<Contratos> BusquedaContrato(string busqString);
       

    }
}
