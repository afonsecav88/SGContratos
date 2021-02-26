using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionContratos.Controllers;
using SistemaGestionContratos.Interfaces;
using System.Threading.Tasks;
using static SistemaGestionContratos.Controllers.NotificacionesController.Enum;

namespace SistemaGestionContratos.Models

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


        public async Task<IActionResult> Index(string busqString, string currentFilter, int? page)
        {
        
            ViewData["filtro"] = busqString;
            ViewBag.paginactual = page;

            if (busqString != null)
            {
                page = 1;
            }
            else
            {
                busqString = currentFilter;
            }
      
            var contratos = _icontratos.BusquedaContrato(busqString);
            ; int pageSize = 10;
            return View(await CPaginacion<Contratos>.CreateAsync(contratos.AsNoTracking(), page ?? 1, pageSize));
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,Proveedor,Modalidad,Firma,Vence,Cuc,Mn,Aprobacion,Observacion")] Contratos contratos)
        {
     
            if (ModelState.IsValid)
            {        
                await _icontratos.CrearContrato(contratos);      s
                Alerta("Contrato creado correctamente.", NotificationType.success);
                return RedirectToAction(nameof(Create));
            }
            return View(contratos);
        }


        // GET: Contratos/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
      
            var contratos = await _icontratos.EditarContrato(id);
            if (contratos == null)
            {
                return NotFound();
            }
            return View(contratos);
        }


        // POST: Contratos/Edit/5
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
                 await _icontratos.EditarContrato(contratos);
                }
                catch (DbUpdateConcurrencyException)
                {
                  
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
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
          await _icontratos.EliminarContrato(id);
            Alerta("Contrato eliminado correctamente.", NotificationType.success);
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> ExportarExcel()
        {
            string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        
            return File(await _iexportaciones.ExportarExcel(), excelContentType, "Listado_Contratos.xlsx");
        }

   
        [MiddlewareFilter(typeof(JsReportPipeline))]
        public async Task<IActionResult> ExportarPDF()
        {
            HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf);
          ViewBag.ListadoContratos = await _icontratos.ListarContratos();

            return View();

        }







    }
}
