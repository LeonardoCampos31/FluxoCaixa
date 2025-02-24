namespace FluxoCaixa.Lancamentos.Modules.DataTransfers
{
    public class LancamentoRequest
    {
        public decimal Valor { get; set; }
        public required string Tipo { get; set; }
    }
}