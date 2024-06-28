using Microsoft.AspNetCore.Mvc;
using Pizzaria.Models;
using Pizzaria.Services.Pizza;
using System.Diagnostics;

namespace Pizzaria.Controllers
{
    public class HomeController : Controller
    {
        //Injeção de dependência
        private readonly IPizzaInterface _pizzaInterface;

        public HomeController(IPizzaInterface pizzaInterface)
        {
            _pizzaInterface = pizzaInterface;
        }

        //método para buscar as pizzas quando o usuário clicar no botão de pesquisa da página
        public async Task<IActionResult> Index(string? pesquisar)
        {
            if (pesquisar == null) 
            {
                //Obtém todas as pizzas
                var pizzas = await _pizzaInterface.GetPizzas();
                return View(pizzas);
            }
            else
            {
                //Obtém somente as pizzas que contiver o texto digitado no campo de pesquisa da tela
                var pizzas = await _pizzaInterface.GetPizzasFiltro(pesquisar);
                return View(pizzas);
            }
        }

    }
}
