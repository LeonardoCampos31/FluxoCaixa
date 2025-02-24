namespace FluxoCaixa.Lancamentos.Modules.Entity
{
    public class Lancamento
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public required string Tipo { get; set; } // "Credito" ou "Debito"
        public DateTime Data { get; set; }
    }
}