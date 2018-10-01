using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Examples;

namespace api.livraria.auth.model.Request
{
    public class TokenRequest
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }

    public class TokenRequestModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new TokenRequest
            {
                Login = "pedro.pefj@hotmail.com",
                Senha = "12345678"
            };

        }
    }
}
