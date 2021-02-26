using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaGestionContratos.Models;

namespace SistemaGestionContratos
{
    public class ContratosContext : DbContext
    {
       
      
        public ContratosContext(DbContextOptions<ContratosContext> options)
            : base(options)
        {

        }
      
        public DbSet<Contratos> Contratos { get; set; }
        public DbSet<Facturas> Facturas { get; set; }


        
    }


    
}
