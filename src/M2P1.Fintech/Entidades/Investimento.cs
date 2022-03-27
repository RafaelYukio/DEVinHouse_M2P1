
using M2P1.Fintech.Enums;

namespace M2P1.Fintech.Entidades
{
    public class Investimento : Transacao
    {
        public TipoAplicacoEnum TipoAplicacao { get; private set; }
        public DateTime? DataAplicacao { get; private set; }
        public DateTime? DataResgate { get; private set; }
        public Investimento(TipoTransacaoEnum tipoTransacao, string descricao, TipoAplicacoEnum tipoAplicacao, decimal valor) : base(tipoTransacao, descricao, valor)
        {
            TipoAplicacao = tipoAplicacao;
        }

        public void Aplicar()
        {
            DataAplicacao = DateTime.Now;
            DataResgate = null;
        }

        public void Resgatar()
        {
            DataResgate = DateTime.Now;
            DataAplicacao = null;
        }
    }
}