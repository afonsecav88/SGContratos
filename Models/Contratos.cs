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
    public class Contratos
    {
        
        public int Id { set; get; }

        [Display(Name = "#")]
        [Required(ErrorMessage = "Debes introducir un número de Contrato ")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Debes intoducir un número de contrato")]
        public string Numero { set; get; }

        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "Debes introducir el Proveedor del Contrato ")]
        public string Proveedor { set; get; }

        [Display(Name = "Modalidad")]
        [Required(ErrorMessage = "Debes introducir la Modalidad del Contrato ")]
        public string Modalidad { set; get; }


        [Display(Name = "Firmado")]
        [Required(ErrorMessage = "Debes introducir una fecha de Firma ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime Firma { set; get; }

        [Display(Name = "Vencimiento")]
        [Required(ErrorMessage = "Debes introducir una fecha de Vencimiento ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]     
        public DateTime Vence { set; get; }
        

        [Display(Name = "CUC")]
        [Required(ErrorMessage = "Debes introducir un valor en CUC")]
        public decimal Cuc { set; get; }

        [Display(Name = "MN")]
        [Required(ErrorMessage = "Debes introducir un valor en MN")]
        public decimal Mn { set; get; }

        [Display(Name = "Aprobado")]
        public string Aprobacion { set; get; }
        [Display(Name = "Observ.")]
        public string Observacion { set; get; }

        public List<Facturas> ListadoFacturas {set;get;}
        

    }
           
}
