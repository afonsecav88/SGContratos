using Microsoft.AspNetCore.Mvc;
using SistemaGestionContratos;
using SistemaGestionContratos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestionContratos.Models
{
    
    public class Facturas
    {
        public int FacturasID { set; get; }

        [Display(Name = "Número de Factura")]
        [Required(ErrorMessage = "Debes introducir el Número de la Factura ")]
        public string NumeroFactura { set; get; }


        [Display(Name = "Fecha de la Factura")]
        [Required(ErrorMessage = "Debes introducir la fecha donde se Generó la Factura ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime FechaFactura { set; get; }


        [Display(Name = "Valor en MN")]
        [Required(ErrorMessage = "Debes introducir el valor de la Factura en MN,si no tiene inserte  cero ")]
        public decimal FacturaMN { set; get; }

        [Required(ErrorMessage = "Debes introducir el valor de la Factura en CUC,si no tiene inserte  cero ")]
        [Display(Name = "Valor en CUC")]
        public decimal FacturaCUC { set; get; }

        [Display(Name = "Observaciones")]
        public string FacturaObservacion { set; get; }

        public int ContratosId { set; get; }

        public Contratos Contrato { set; get; }

    }
}
