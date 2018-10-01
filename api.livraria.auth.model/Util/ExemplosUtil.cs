using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Resources;
using System.Threading.Tasks;
using api.livraria.auth.Util;

namespace api.livraria.auth.model.Util
{
    public class ExemplosUtil
    {
        public static List<MensagemError> ListarMensagens(List<string> listaCodigos, HttpStatusCode statusCode)
        {
            List<MensagemError> listaMsgs = new List<MensagemError>();

            foreach (string codigo in listaCodigos)
            {
                try
                {
                    string msg = mensagens_livraria_api.ResourceManager.GetString(codigo);
                    if (!string.IsNullOrEmpty(msg))
                        listaMsgs.Add(new MensagemError() { StatusCode = statusCode, Codigo = codigo.Replace("M", ""), Mensagem = msg });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

            }
            return listaMsgs.OrderBy(s => Convert.ToInt32(s.Codigo)).ToList();
        }
    }
}
