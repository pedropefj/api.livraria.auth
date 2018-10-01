using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.livraria.auth.model.Util;
using Swashbuckle.AspNetCore.Examples;

namespace api.livraria.auth.model.Response
{
    public class UsuarioResponseUpdate
    {
        public string Nome { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataAtualizacao { get; set; }


    }

    public class UsuarioResponseUpdateModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new UsuarioResponseUpdate
            {
                Nome = "Pedro",
                Email = "pedro.pefj@hotmail.com",
                UserName = "pedropefj",
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now.AddYears(1)

            };

        }
    }

    public class PutUsarioResponse400 : IExamplesProvider
    {
        public object GetExamples()
        {
            List<string> listaCodigos = new List<string> { "M199", "M0001", "M0002", "M0003", "M0004", "M0005" };
            var retorno = ExemplosUtil.ListarMensagens(listaCodigos, HttpStatusCode.BadRequest);
            return retorno;
        }
    }
}


