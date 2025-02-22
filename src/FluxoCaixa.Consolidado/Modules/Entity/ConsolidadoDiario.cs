namespace FluxoCaixa.Consolidado.Modules.Entity
{
    public class ConsolidadoDiario
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Saldo { get; set; }
    }
}