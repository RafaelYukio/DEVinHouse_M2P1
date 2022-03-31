using M2P1.Fintech.Enums;

namespace M2P1.Fintech.Entidades
{
    public class ContaPoupanca : Conta
    {
        public decimal ValorRendimentoPoupanca { get; private set; }
        public decimal ValorAplicacoPoupanca { get; private set; }

        public ContaPoupanca(string id, string nome, string cpf, string endereco, decimal rendaMensal, int contaNumero, AgenciaEnum agencia)
            : base(id, nome, cpf, endereco, rendaMensal, contaNumero, agencia)
        {
            TipoConta = TipoContaEnum.Poupanca;
            ValorRendimentoPoupanca = 1.005M;
            ValorAplicacoPoupanca = 0;
        }

        private decimal SimulacaoRendimentoPorMes(DateOnly dataResgate, DateOnly dataAntiga, decimal valorRendimento)
        {
            decimal rendimentoTotal = 1.00M;

            int meses = ((dataResgate.Year - dataAntiga.Year) * 12) + dataResgate.Month - dataAntiga.Month;

            for (int i = 0; i < meses; i++)
            {
                rendimentoTotal = rendimentoTotal * valorRendimento;
            }

            return rendimentoTotal;

        }
        public decimal SimulacaoRendimento(decimal valor, DateOnly dataResgate, decimal rendimento) => valor * SimulacaoRendimentoPorMes(dataResgate, DateOnly.FromDateTime(DateTime.Now), rendimento);
        
        public void AplicarPoupanca(decimal valor) => ValorAplicacoPoupanca += valor;
        public void ResgatarPoupanca(decimal valor) => ValorAplicacoPoupanca -= valor;

        public decimal RetornarValorPoupanca() => ValorAplicacoPoupanca;
        public void Render(DateOnly dataNova, DateOnly dataAntiga)
        {
            ValorAplicacoPoupanca = ValorAplicacoPoupanca * SimulacaoRendimentoPorMes(dataNova, dataAntiga, ValorRendimentoPoupanca);
        }

    }
}
