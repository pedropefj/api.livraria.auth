using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace api.livraria.auth.Util
{
    public class MensagemError
    {
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        public string Codigo { get; set; }
        public string Mensagem { get; set; }

        public MensagemError(HttpStatusCode statusCode, string codigo, string mensagem)
        {
            StatusCode = statusCode;
            Codigo = codigo;
            Mensagem = mensagem;
        }

        public MensagemError() { }
    }
}
