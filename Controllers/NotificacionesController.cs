using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using static SistemaGestionContratos.Controllers.NotificacionesController.Enum;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaGestionContratos.Controllers
{
    public class NotificacionesController : Controller
    {
        // GET: /<controller>/
        public void Alerta(string message, NotificationType notificationType)
        {
            var msg = "<script language ='javascript'> Swal.fire('" + "', '" + message + "','" + notificationType + "')" + "</script>";
            TempData["notification2"] = msg;
        }


        public class Enum
        {
            public enum NotificationType
            {
                error,
                success,
                warning,
                info
            }
        }

    }
}
