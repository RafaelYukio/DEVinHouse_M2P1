using M2P1.Fintech.Enums;

namespace M2P1.Fintech.Entidades
{
    public class Transferencia : Transacao
    {
        public Conta DadosContaOrigem { get; private set; }
        public Conta DadosContaDestino { get; private set; }

        public Transferencia(TipoTransacaoEnum tipoTransacao, string descricao, decimal valor, Conta dadosContaOrigem, Conta dadosContaDestino) : base(tipoTransacao, descricao, valor)
        {
            DadosContaOrigem = dadosContaOrigem;
            DadosContaDestino = dadosContaDestino;

        }
    }
}
