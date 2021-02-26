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
       
        /*Declaro el constructor de esta clase que hereda de la clase DbContext,por tanto tengo que 
         * enviarle los parametros que estan declarado en el contructor de DbContext, en este caso 
         * le envio un objeto llamado options de tipo DbContextOptions<ContratoUEBContext> pasandole ContratoUEBContext.
        */
        public ContratosContext(DbContextOptions<ContratosContext> options)
            : base(options)
        {

        }

        /*En este metodo también prodria definir el tipo de Proveedor de Base de Datos que utilizo y la cadena de conexión.
        //tengo que usar el namespace ----- using Microsoft.EntityFrameworkCore; con solo poner override on la el intelisense me crea este metodo
        //En este proyecto se configura en la clase Startup
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=HOME;Initial Catalog=GestionContratos;User ID=sa;Password=Sasql*2019;Trusted_Connection=True;MultipleActiveResultSets=True");
        }

        //En este metodo se establecen los filtros por defecto, o varias de las foncionalidades de entityFramework
        //por ejemplo el dataseeding que consiste en insertar campos por defectos ne la BD.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contratos>().HasQueryFilter(x => x.Id>2);
        }

    */



        public DbSet<Contratos> Contratos { get; set; }
        public DbSet<Facturas> Facturas { get; set; }


        
    }


    
}
