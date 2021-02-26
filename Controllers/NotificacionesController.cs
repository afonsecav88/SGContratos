using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using static SistemaGestionContratos.Controllers.NotificacionesController.Enum;

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
