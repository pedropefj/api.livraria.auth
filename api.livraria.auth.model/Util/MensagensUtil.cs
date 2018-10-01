using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.livraria.auth.Util;

namespace api.livraria.auth.model.Util
{
    public class MensagensUtil
    {
        public static MensagemError ObterMensagem(HttpStatusCode statusCode, string codigo)
        {
            try
            {
                return new MensagemError(statusCode, codigo.Replace("M", ""), mensagens_livraria_api.ResourceManager.GetString(codigo));
            }
            catch
            {
                return new MensagemError(statusCode, "199", mensagens_livraria_api.ResourceManager.GetString("M199"));
            }
        }
    }
}
