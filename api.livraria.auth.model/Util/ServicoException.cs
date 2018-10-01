using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.livraria.auth.Util
{
    public class ServicoException : Exception
    {
        public MensagemError MensagemError { get; set; }

        public ServicoException(MensagemError mensagem)
        {
            MensagemError = mensagem;
        }
    }
}
