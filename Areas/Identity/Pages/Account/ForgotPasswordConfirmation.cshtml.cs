﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaGestionContratos.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordConFirmation : PageModel
    {
        public void OnGet()
        {
        }
    }
}
