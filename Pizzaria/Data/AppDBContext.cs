using Microsoft.EntityFrameworkCore;
using Pizzaria.Models;

namespace Pizzaria.Data
{
    //Herda o DBContext do EntityFrameworkCore
    public class AppDBContext : DbContext 
    {

        //Faz o meio de campo entre a aplicação e o banco de dados
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        {
        }
        
        //Seta a tabela que será utilizada, baseada no modelo PizzaModel
        public DbSet<PizzaModel> Pizzas { get; set; }
    }
}
