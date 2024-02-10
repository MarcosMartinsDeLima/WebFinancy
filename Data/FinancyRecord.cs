namespace WebFinancy.Data
{
    public record FinancyRecord
    {
        public string Nome {get;set;} = string.Empty;
        public string Descricao {get;set;} = string.Empty;
        public float Valor {get;set;}
        public DateOnly Data {get;set;}
        public int IdUser {get;set;}
    }
}