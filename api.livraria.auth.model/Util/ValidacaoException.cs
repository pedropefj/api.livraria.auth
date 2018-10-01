using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.livraria.auth.Util
{
    public class ValidacaoException : Exception
    {
        public MensagemError MensagemError { get; set; }

        public ValidacaoException(MensagemError mensagem)
        {
            MensagemError = mensagem;
        }
    }
}
