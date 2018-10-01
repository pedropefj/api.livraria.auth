using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Examples;

namespace api.livraria.auth.model.Request
{
    public class UsuarioRequest
    {
            public string Nome { get; set; }

            public string UserName { get; set; }

            public string Email { get; set; }

            public string Senha { get; set; }        
    }

    public class UsuarioRequestModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new UsuarioRequest
            {
                Nome = "Pedro",
                Email = "pedro.pefj@hotmail.com",
                UserName = "pedropefj",
                Senha = "12345678"
            };

        }
    }
}
