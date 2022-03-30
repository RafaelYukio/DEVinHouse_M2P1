using M2P1.Fintech.Enums;

namespace M2P1.Fintech.Entidades
{
    public class ContaInvestimento : Conta
    {
        public decimal ValorRendimentoLCI { get; private set; }
        public decimal ValorRendimentoLCA { get; private set; }
        public decimal ValorRendimentoCDB { get; private set; }
        public decimal ValorAplicacoLCI { get; private set; }
        public decimal ValorAplicacoLCA { get; private set; }
        public decimal ValorAplicacoCDB { get; private set; }

        public ContaInvestimento(string id, string nome, string cpf, string endereco, decimal rendaMensal, int contaNumero, AgenciaEnum agencia)
        : base(id, nome, cpf, endereco, rendaMensal, contaNumero, agencia)
        {
            TipoConta = TipoContaEnum.Investimento;
            ValorRendimentoLCI = 1.08M;
            ValorRendimentoLCA = 1.09M;
            ValorRendimentoCDB = 1.10M;
            ValorAplicacoLCI = 0;
            ValorAplicacoLCA = 0;
            ValorAplicacoCDB = 0;
        }

        private decimal SimulacaoRendimentoPorDia(DateOnly dataAplicacao, DateOnly dataResgate, decimal valorRendimento)
        {
            decimal rendimentoTotal = 1.00M;

            DateTime dataAplicada = dataAplicacao.ToDateTime(new TimeOnly(0, 0));
            DateTime dataRetirada = dataResgate.ToDateTime(new TimeOnly(0, 0));

            decimal dias = (dataRetirada.Date - dataAplicada.Date).Days;
            decimal anos = Math.Truncate(dias / 365);

            dias = (dias / 365) - anos;

            for (int i = 0; i < anos; i++)
            {
                rendimentoTotal = rendimentoTotal * valorRendimento;
            }

            rendimentoTotal = rendimentoTotal + ((valorRendimento - 1) * dias);

            return rendimentoTotal;

        }
        public decimal SimularRendimentoLCI(decimal valor, DateOnly dataAplicacao, DateOnly dataResgate) => valor * SimulacaoRendimentoPorDia(dataAplicacao, dataResgate, ValorRendimentoLCI);
        public decimal SimularRendimentoLCA(decimal valor, DateOnly dataAplicacao, DateOnly dataResgate) => valor * SimulacaoRendimentoPorDia(dataAplicacao, dataResgate, ValorRendimentoLCA);
        public decimal SimularRendimentoCDB(decimal valor, DateOnly dataAplicacao, DateOnly dataResgate) => valor * SimulacaoRendimentoPorDia(dataAplicacao, dataResgate, ValorRendimentoCDB);

        public void AplicarLCI(decimal valor)
        {
            ValorAplicacoLCI += valor;
        }
        public void AplicarLCA(decimal valor)
        {
            ValorAplicacoLCA += valor;
        }
        public void AplicarCDB(decimal valor)
        {
            ValorAplicacoCDB += valor;
        }

        public void ResgatarLCI(decimal valor)
        {
            ValorAplicacoLCI -= valor;
        }
        public void ResgatarLCA(decimal valor)
        {
            ValorAplicacoLCA -= valor;
        }
        public void ResgatarCDB(decimal valor)
        {
            ValorAplicacoCDB -= valor;
        }

        public decimal RetornarValorLCI() => ValorAplicacoLCI;
        public decimal RetornarValorLCA() => ValorAplicacoLCA;
        public decimal RetornarValorCDB() => ValorAplicacoCDB;

        public void Render(DateOnly dataNova, DateOnly dataAntiga)
        {
            ValorAplicacoLCI = SimularRendimentoLCI(ValorAplicacoLCI, dataAntiga, dataNova);
            ValorAplicacoLCA = SimularRendimentoLCA(ValorAplicacoLCA, dataAntiga, dataNova);
            ValorAplicacoCDB = SimularRendimentoCDB(ValorAplicacoCDB, dataAntiga, dataNova);
        }

    }
}
