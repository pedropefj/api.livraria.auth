using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.livraria.auth.model.Util;
using Swashbuckle.AspNetCore.Examples;

namespace api.livraria.auth.model.Response
{
    public class UsuarioResponse
    {
        public string Nome { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime DataCriacao { get; set; }

    }

    public class UsuarioResponseModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new UsuarioResponse
            {
                Nome = "Pedro",
                Email = "pedro.pefj@hotmail.com",
                UserName = "pedropefj",
                DataCriacao = DateTime.Now
            };

        }
    }
    
        public class PostUsarioResponse400 : IExamplesProvider
        {
            public object GetExamples()
            {
                List<string> listaCodigos = new List<string> { "M199", "M0001","M0002","M0003","M0004", "M0005" };
                var retorno = ExemplosUtil.ListarMensagens(listaCodigos, HttpStatusCode.BadRequest);
                return retorno;
            }
        }
    
        public class UsuarioResponse400 : IExamplesProvider
        {
            public object GetExamples()
            {
                List<string> listaCodigos = new List<string> { "M199", "M0001", "M0004" };
                var retorno = ExemplosUtil.ListarMensagens(listaCodigos, HttpStatusCode.BadRequest);
                return retorno;
            }
        }

        public class AllUsariosResponse400 : IExamplesProvider
        {
            public object GetExamples()
            {
                List<string> listaCodigos = new List<string> { "M199", "M0001" };
                var retorno = ExemplosUtil.ListarMensagens(listaCodigos, HttpStatusCode.BadRequest);
                return retorno;
            }
        }

    public class UsuarioResponse401 : IExamplesProvider
    {
        public object GetExamples()
        {
            List<string> listaCodigos = new List<string> { "M199", "M0006" };
            var retorno = ExemplosUtil.ListarMensagens(listaCodigos, HttpStatusCode.BadRequest);
            return retorno;
        }
    }
}
