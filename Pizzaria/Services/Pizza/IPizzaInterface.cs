using Pizzaria.Dto;
using Pizzaria.Models;

namespace Pizzaria.Services.Pizza
{
    public interface IPizzaInterface
    {
        //Chama o serviço de criar a pizza
        Task<PizzaModel> CriarPizza(PizzaCriacaoDto pizzaCriacaoDto, IFormFile foto );
        
        //Chama o serviço para retornar todas as pizzas
        Task<List<PizzaModel>> GetPizzas();

        //Chama o serviço para retornar a pizza por ID
        Task<PizzaModel> GetPizzaPorId(int id);

        //Chama o serviço de atualização dos dados/imagem da pizza
        Task<PizzaModel> EditarPizza(PizzaModel pizza, IFormFile? foto);

        //Chama o serviço para excluir a pizza do banco de dados
        Task<PizzaModel> RemoverPizza(int id);

        //Chama o serviço de seleção de pizzas baseado no que o usuário digitar no campo de pesquisa da tela
        Task<List<PizzaModel>> GetPizzasFiltro(string? pesquisar);

    }
}
