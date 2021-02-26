using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestionContratos;
using Rotativa.AspNetCore;
using SistemaGestionContratos.Controllers;
using SistemaGestionContratos.Models;
//agregado para metodo de la notificaciones
using static SistemaGestionContratos.Controllers.NotificacionesController.Enum;
//agregado para metodo de exportar a excel
using OfficeOpenXml;
using OfficeOpenXml.Table;
using jsreport.AspNetCore;
using jsreport.Types;
using SistemaGestionContratos.Services;

namespace SistemaGestionContratos.Models
{


    public class FacturasController : NotificacionesController
    {


        private readonly ContratosContext _context;

        public ISeleccionarContrato _selectContrato;

        public FacturasController(ContratosContext context, ISeleccionarContrato selectContrato)
        {
            _context = context;
            _selectContrato = selectContrato;
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


            // GET: Facturas
            var facturas = from s in _context.Facturas select s;


            if (!string.IsNullOrEmpty(busqString))
            {
                facturas = facturas.Where(s => s.NumeroFactura.Contains(busqString.Trim()) ||
                                                  s.FechaFactura.ToString().Contains(busqString.Trim()) ||
                                                  s.FacturaCUC.ToString().Contains(busqString.Trim()) ||
                                                  s.FacturaMN.ToString().Contains(busqString.Trim()) ||
                                                  s.FacturaObservacion.Contains(busqString.Trim()));
            }
            int pageSize = 10;

            return View(await CPaginacion<Facturas>.CreateAsync(facturas.AsNoTracking(), page ?? 1, pageSize));
        }







        [MiddlewareFilter(typeof(JsReportPipeline))]
        public IActionResult ExportarPDF()
        {
            HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf);

            ViewBag.ListadoContratos = _context.Contratos.ToList();

            return View();

        }

        // GET: Contratos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contratos = await _context.Contratos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contratos == null)
            {
                return NotFound();
            }

            return View(contratos);
        }

        // GET: Contratos/Create
        public IActionResult Create()
        {
            //Pasando Resultado del metodo Devolverlistado() a la vista 
            // ViewBag.Milistado = Devolverlistado();

            List<Contratos> lst = new List<Contratos>();
            lst = _context.Contratos.ToList();
            ViewBag.listado = lst;
            return View();

        }

              

        // POST: Contratos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroFactura,FechaFactura,FacturaMN,FacturaCUC,FacturaObservacion,ContratosId")]Facturas facturas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturas);
                await _context.SaveChangesAsync();
                //Metodo agregado para mostrar notificaciones
                Alerta("Factura creada correctamente.", NotificationType.success);
                return RedirectToAction(nameof(Create));

            }

            return View(facturas);
        }

        // GET: Contratos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contratos = await _context.Contratos.FindAsync(id);
            if (contratos == null)
            {
                return NotFound();
            }
            return View(contratos);
        }

        // POST: Contratos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContratosID,Numero,Proveedor,Modalidad,Firma,Vence,cuc,mn,Aprobacion,Observacion")] Contratos contratos)
        {
            if (id != contratos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contratos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContratosExists(contratos.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                Alerta("Factura editada correctamente.", NotificationType.success);
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

            var contratos = await _context.Contratos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contratos == null)
            {
                return NotFound();
            }
            Alerta("Si pulsas en botón eliminar,esta Fatura se eliminará de forma permanente.", NotificationType.warning);
            return View(contratos);
        }

        // POST: Contratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contratos = await _context.Contratos.FindAsync(id);
            _context.Contratos.Remove(contratos);
            await _context.SaveChangesAsync();
            Alerta("Factura eliminada correctamente.", NotificationType.success);
            return RedirectToAction(nameof(Index));
        }

        private bool ContratosExists(int id)
        {
            return _context.Contratos.Any(e => e.Id == id);
        }




        //Metodo agregado para exportar a excel
        public IActionResult ExportarExcel()
        {
            string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var contratos = _context.Contratos.AsNoTracking().ToList();
            using var libro = new ExcelPackage();
            var worksheet = libro.Workbook.Worksheets.Add("contratos");
            worksheet.Cells["A1"].LoadFromCollection(contratos, PrintHeaders: true);
            for (var col = 1; col < contratos.Count + 1; col++)
            {
                worksheet.Column(col).AutoFit();
            }

            // Agregar formato de tabla
            var tabla = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: contratos.Count + 1, toColumn: 10), "contratos");
            tabla.ShowHeader = true;
            tabla.TableStyle = TableStyles.Light10;
            tabla.ShowTotal = true;
            return File(libro.GetAsByteArray(), excelContentType, "Listado_Contratos.xlsx");
        }


        //Metodos para exportar a pdf







    }
}
