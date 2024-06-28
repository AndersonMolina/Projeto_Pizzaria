using Microsoft.AspNetCore.Mvc;
using Pizzaria.Models;
using Pizzaria.Services.Pizza;
using System.Diagnostics;

namespace Pizzaria.Controllers
{
    public class HomeController : Controller
    {
        //Inje��o de depend�ncia
        private readonly IPizzaInterface _pizzaInterface;

        public HomeController(IPizzaInterface pizzaInterface)
        {
            _pizzaInterface = pizzaInterface;
        }

        //m�todo para buscar as pizzas quando o usu�rio clicar no bot�o de pesquisa da p�gina
        public async Task<IActionResult> Index(string? pesquisar)
        {
            if (pesquisar == null) 
            {
                //Obt�m todas as pizzas
                var pizzas = await _pizzaInterface.GetPizzas();
                return View(pizzas);
            }
            else
            {
                //Obt�m somente as pizzas que contiver o texto digitado no campo de pesquisa da tela
                var pizzas = await _pizzaInterface.GetPizzasFiltro(pesquisar);
                return View(pizzas);
            }
        }

    }
}
