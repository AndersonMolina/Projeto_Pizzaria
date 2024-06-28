using Microsoft.AspNetCore.Mvc;
using Pizzaria.Dto;
using Pizzaria.Models;
using Pizzaria.Services.Pizza;

namespace Pizzaria.Controllers
{
    public class PizzaController : Controller
    {

        //Injeções de dependência
        private readonly IPizzaInterface _pizzaInterface;

        public PizzaController(IPizzaInterface pizzaInterface)
        {
            _pizzaInterface = pizzaInterface;
        }
        public async Task<IActionResult> Index()
        {
            var pizzas = await _pizzaInterface.GetPizzas();
            return View(pizzas);
        }

        public IActionResult Cadastrar() 
        { 
            return View();
        }

        //Busca as pizzas pelo ID para exibir a tela de dados
        public async Task<IActionResult> Detalhes(int id) 
        {    
            var pizza = await _pizzaInterface.GetPizzaPorId(id);
            return View(pizza);
        }

        //Busca a pizza pelo ID para abrir a tela de edição dos dados
        public async Task<IActionResult> Editar(int id)
        {
            var pizza = await _pizzaInterface.GetPizzaPorId(id);

            return View(pizza);
        }

        //Busca a pizza pelo ID para excluí-la
        public async Task<IActionResult> Remover (int id)
        {
            var pizza = await _pizzaInterface.RemoverPizza(id);
            return RedirectToAction("Index", "Pizza");
        }

        //Identificando o método como Post
        [HttpPost]
        public async Task<IActionResult> Cadastrar(PizzaCriacaoDto pizzaCriacaoDto,IFormFile foto) 
        {
            //Verifica se o model foi montado corretamente
            if (ModelState.IsValid)
            {
                //inicia o processo de criação da pizza baseada nos dados do model de forma assíncrona
                var pizza = await _pizzaInterface.CriarPizza(pizzaCriacaoDto, foto);

                return RedirectToAction("Index", "Pizza");
            }
            else
            {
                return View(pizzaCriacaoDto);
            }
        }

        //Identificando o método como Post
        [HttpPost]
        public async Task<IActionResult> Editar(PizzaModel pizzaModel, IFormFile? foto)
        {
            if (ModelState.IsValid)
            {
                //Edita os dados de forma assíncrona
                var pizza = await _pizzaInterface.EditarPizza(pizzaModel, foto);
                return RedirectToAction("Index", "Pizza");
            }
            else
            {
                return View(pizzaModel);
            }
        }

    }


}
