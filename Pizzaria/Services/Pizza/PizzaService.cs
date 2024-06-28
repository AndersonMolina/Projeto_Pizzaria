using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Dto;
using Pizzaria.Models;
using System.Reflection.Metadata.Ecma335;

//Todos as funções, serviços, etc estão aqui, para não poluir a interface IPizzaInterface

namespace Pizzaria.Services.Pizza
{
    public class PizzaService : IPizzaInterface
    {
        //Injeções de dependência
        private readonly AppDBContext _context;
        private readonly string _sistema;

        public PizzaService(AppDBContext context, IWebHostEnvironment sistema)
        {
            _context = context; 
            _sistema = sistema.WebRootPath;
        }

        //Utilizado para criar o caminho completo do diretório onde a imagem será salva
        public string GeraCaminhoArquivo(IFormFile foto)
        {
            //Cria uma cadeia de caracteres aleatória para concatenar com o nome do arquivo e evitar que existam nomes de arquivos iguais no banco
            var idUnico = Guid.NewGuid().ToString(); 

            //Concatena o nome do arquivo com o IdUnico, limpando espaços em branco e deixando tudo em letra minúscula
            var nomeImagem = foto.FileName.Replace(" ", "").ToLower() + idUnico + ".png";

            //Define o diretório onde as imagens serão salvas
            var diretorio = _sistema + "\\imagens\\";

            //Verifica se existe o diretório antes de criar
            if(!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            //Cria o arquivo
            using (var stream = File.Create(diretorio + nomeImagem))
            {
                foto.CopyToAsync(stream).Wait();
            }

            return nomeImagem;

        }
        
        //Utilizado para criar as pizzas
        public async Task<PizzaModel> CriarPizza(PizzaCriacaoDto pizzaCriacaoDto, IFormFile foto)
        {
            try
            {
                //Cria o caminho da imagem
                var CaminhoImagem = GeraCaminhoArquivo(foto);

                //Cria a pizza
                var pizza = new PizzaModel
                {
                    Sabor = pizzaCriacaoDto.Sabor,
                    Descricao = pizzaCriacaoDto.Descricao,
                    Valor = pizzaCriacaoDto.Valor,
                    Capa = CaminhoImagem
                };

                //Adiciona a pizza ao banco
                _context.Add(pizza);

                //Aguarda o processo terminar
                await _context.SaveChangesAsync(); 

                return pizza;

            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        //Usado para listar as pizzas cadastradas
        public async Task<List<PizzaModel>> GetPizzas()
        {
            try
            {
                return await _context.Pizzas.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Usado para listar as pizza cadastrada, baseada no ID
        public async Task<PizzaModel> GetPizzaPorId(int id)
        {
            try
            {
                //Busca a pizza no banco de dados
                return await _context.Pizzas.FirstOrDefaultAsync(Pizza => Pizza.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    

            }
        }

        //Usado para editar os dados da pizza
        public async Task<PizzaModel> EditarPizza(PizzaModel pizza, IFormFile? foto)
        {
            try
            {
                //Busca os dados da pizza
                var pizzaBanco = await _context.Pizzas.AsNoTracking().FirstOrDefaultAsync(pizzaBD => pizzaBD.Id == pizza.Id);

                var nomeCaminhoImagem = "";

                //Checa a foto da pizza
                if (foto != null)
                {
                    string caminhoCapaExistente = _sistema + "\\imagens\\" + pizzaBanco.Capa;

                    //Se a foto já existir, deleta e em seguida cria uma nova, evitando armazenamento de arquivos desnecessários
                    if (File.Exists(caminhoCapaExistente))
                    {
                        File.Delete(caminhoCapaExistente);
                    }

                    nomeCaminhoImagem = GeraCaminhoArquivo(foto);

                }

                //Preenche os dados da pizza com os dados novos
                pizzaBanco.Sabor = pizza.Sabor;
                pizzaBanco.Descricao = pizza.Descricao;
                pizzaBanco.Valor = pizza.Valor;

                if (nomeCaminhoImagem != "")
                {
                    pizzaBanco.Capa = nomeCaminhoImagem;
                }
                else 
                {
                    pizzaBanco.Capa = pizza.Capa;
                }

                //Atualiza o banco de dados
                _context.Update(pizzaBanco);
                await _context.SaveChangesAsync();

                return pizza;

            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        //Usado para deletar a pizza
        public async Task<PizzaModel> RemoverPizza(int id)
        {
            try
            {
                //Busca a pizza no banco e deleta
                var pizza = await _context.Pizzas.FirstOrDefaultAsync(pizzabanco => pizzabanco.Id == id);
                _context.Remove(pizza);
                await _context.SaveChangesAsync();

                return pizza;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);    
            }
        }

        //Usado para buscar a pizza no banco de dados a partir dos dados inseridos pelo usuário no campo de pesquisa da tela
        public async Task<List<PizzaModel>> GetPizzasFiltro(string? pesquisar)
        {
            try
            {
                //Busca as pizzas no banco de dados
                var pizzas = await _context.Pizzas.Where(pizzabanco => pizzabanco.Sabor.Contains(pesquisar)).ToListAsync();
                return pizzas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
