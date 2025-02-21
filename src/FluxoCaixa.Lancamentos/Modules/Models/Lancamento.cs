namespace FluxoCaixa.Lancamentos.Modules.Models
{
    public class Lancamento
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; } // "Credito" ou "Debito"
        public DateTime Data { get; set; }
    }
}