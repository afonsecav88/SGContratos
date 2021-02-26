//agregado para metodo de exportar a excel
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionContratos.Controllers;
using SistemaGestionContratos.Interfaces;
using System.Threading.Tasks;
//agregado para metodo de la notificaciones
using static SistemaGestionContratos.Controllers.NotificacionesController.Enum;

namespace SistemaGestionContratos.Models
{
    //agregado para proteccion de rutas
    [Authorize]
    public class ContratosController : NotificacionesController
    {

        private readonly IContratos _icontratos;
        private readonly IExportaciones _iexportaciones;

        public ContratosController(IContratos icontratos, IExportaciones iexportaciones)

        {
            _icontratos = icontratos;
            _iexportaciones = iexportaciones;
        }


        //Metodo que retorna la vista Index
        //Metodo Refactorizado 
        public async Task<IActionResult> Index(string busqString, string currentFilter, int? page)
        {
            //utilizado para recoger de la vista la palabra a buscar
            ViewData["filtro"] = busqString;
            //utilizado para recoger de la vista la pagina actual del paginador
            ViewBag.paginactual = page;

            if (busqString != null)
            {
                page = 1;
            }
            else
            {
                busqString = currentFilter;
            }
            //Llamando al metodo BusquedaContrato de la Clase CContratos que Implementa la Interface IContratos      
            var contratos = _icontratos.BusquedaContrato(busqString);
            ; int pageSize = 10;
            return View(await CPaginacion<Contratos>.CreateAsync(contratos.AsNoTracking(), page ?? 1, pageSize));
        }



        // GET: Contratos/Details/5
        //Metodo Refactorizado 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Llamando al metodo DetallarContrato de la Clase CContratos que Implementa la Interface IContratos
            var contratos = await _icontratos.DetallarContrato(id);

            if (contratos == null)
            {
                return NotFound();
            }

            return View(contratos);
        }

        // GET: Contratos/Create
        public IActionResult Create()
        {

            return View();
        }


        // POST: Contratos/Create
        //Metodo Refactorizado 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,Proveedor,Modalidad,Firma,Vence,Cuc,Mn,Aprobacion,Observacion")] Contratos contratos)
        {
            //Comprobando el modelo que se envio de la peticion Get
            if (ModelState.IsValid)
            {
                //Llamando al metodo CrearContrato de la Clase CContratos que Implementa la Interface IContratos
                await _icontratos.CrearContrato(contratos);
                //Metodo agregado para mostrar notificaciones
                Alerta("Contrato creado correctamente.", NotificationType.success);
                return RedirectToAction(nameof(Create));
            }
            return View(contratos);
        }


        // GET: Contratos/Edit/5
        //Metodo Refactorizado
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Llamando al metodo Editar (con la sobrecarga de Id) de la Clase CContratos que Implementa la Interface IContratos
            var contratos = await _icontratos.EditarContrato(id);
            if (contratos == null)
            {
                return NotFound();
            }
            return View(contratos);
        }


        // POST: Contratos/Edit/5
        //Metodo Refactorizado 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Proveedor,Modalidad,Firma,Vence,Cuc,Mn,Aprobacion,Observacion")] Contratos contratos)
        {
            if (id != contratos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Llamando al metodo Editar (con la sobrecarga de Contratos) de la Clase CContratos que Implementa la Interface IContratos
                    await _icontratos.EditarContrato(contratos);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //Llamando al metodo ExisteContrato de la Clase CContratos que Implementa la Interface IContratos
                    if (!_icontratos.ExisteContrato(contratos.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                Alerta("Contrato editado correctamente.", NotificationType.success);
                return RedirectToAction(nameof(Index));
            }
            return View(contratos);
        }

        // GET: Contratos/Delete/5
        //Metodo Refactorizado  
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Llamando al metodo EliminarContrato(Sobrecarga de Id que puede se nulo) de la Clase CContratos que Implementa la Interface IContratos
            var contratos = await _icontratos.EliminarContrato(id);

            if (contratos == null)
            {
                return NotFound();
            }
            Alerta("Si pulsas en botón eliminar,este contrato se eliminará de forma permanente.", NotificationType.warning);
            return View(contratos);
        }


        // POST: Contratos/Delete/5
        //Metodo Refactorizado         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Llamando al metodo EliminarContrato(Sobrecarga de Id no nulo) de la Clase CContratos que Implementa la Interface IContratos
            await _icontratos.EliminarContrato(id);
            Alerta("Contrato eliminado correctamente.", NotificationType.success);
            return RedirectToAction(nameof(Index));
        }



        //Metodo agregado para exportar a excel usando paquete nuget EPPlus
        //Metodo Refactorizado   
        public async Task<IActionResult> ExportarExcel()
        {
            string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Llamando al metodo ExportarExcel de la Clase CExportaciones que Implementa la Interface IExportaciones
            return File(await _iexportaciones.ExportarExcel(), excelContentType, "Listado_Contratos.xlsx");
        }


        //Metodo para exportar a PDF usando JSReport
        //Metodo Refactorizado   
        [MiddlewareFilter(typeof(JsReportPipeline))]
        public async Task<IActionResult> ExportarPDF()
        {
            HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf);
            //Llamando al metodo ListarContratos de la Clase CContratos que Implementa la Interface IContratos
            ViewBag.ListadoContratos = await _icontratos.ListarContratos();

            return View();

        }







    }
}
