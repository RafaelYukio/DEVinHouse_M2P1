using M2P1.Fintech.Enums;

namespace M2P1.Fintech.Entidades
{
    public class Transacao : BaseFintech
    {
        public TipoTransacaoEnum TipoTransacao { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime Data { get; private set; }

        public Transacao(TipoTransacaoEnum tipoTransacao, string descricao, decimal valor, DateTime data)
        {
            TipoTransacao = tipoTransacao;
            Descricao = descricao;
            Valor = valor;
            Data = data;
        }
    }
}
