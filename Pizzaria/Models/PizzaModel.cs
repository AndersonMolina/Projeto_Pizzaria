namespace Pizzaria.Models
{
    public class PizzaModel
    {
        //Dados da Pizza. Os dados inseridos aqui servirão de base para criar a tabela no banco de dados (Code First)
        public int Id { get; set; }

        public string  Sabor { get; set; } = string.Empty;

        public string Capa { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;
        
        public double Valor { get; set; }

    }
}
