using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Examples;

namespace api.livraria.auth.model.Response
{
    public class UsuariosResponse
    {
        public List<UsuarioResponse> Usuarios;

    }

    public class UsuariosResponseModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new UsuariosResponse()
            {
                Usuarios = new List<UsuarioResponse>() {
                    new UsuarioResponse()
                    {
                        Nome = "Pedro",
                        Email = "pedro.pefj@hotmail.com",
                        UserName = "pedropefj",
                        DataCriacao = DateTime.Now
                    },new UsuarioResponse()
                    {
                        Nome = "Pedro",
                        Email = "pedro.pefj@hotmail.com",
                        UserName = "pedropefj",
                        DataCriacao = DateTime.Now
                    }
                }

            };

        }
    }

}
