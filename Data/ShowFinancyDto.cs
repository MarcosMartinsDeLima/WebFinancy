namespace WebFinancy.Data
{
    public record ShowFinancyDto(
        string Nome,
        string Descricao,
        float Valor,
        DateOnly Data
    );
}