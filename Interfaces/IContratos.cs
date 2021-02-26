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

        //Metodo usado para Crear Contratos(POST)
        Task CrearContrato(Contratos contratos);



        //Metodo usado para Eliminar Contratos(GET)
        Task<Contratos> EliminarContrato(int? id);

        //Metodo usado para Eliminar Contratos(POST)
        Task EliminarContrato(int id);



        //Metodo usado para Editar Contratos(GET)
        Task<Contratos> EditarContrato(int? id);

        //Metodo usado para Editar Contratos(POST)
        Task EditarContrato(Contratos contratos);



        //Metodo usado para Detallar Contratos(GET)
        Task<Contratos> DetallarContrato(int? id);


        //Metodo usado para Validar si Existe un Contrato
        bool ExisteContrato(int id);


        //Metodo usado para Listar  Contratos
        Task<List<Contratos>> ListarContratos();

        //Metodo usado para Buscar Contratos
        IQueryable<Contratos> BusquedaContrato(string busqString);
       

    }
}
