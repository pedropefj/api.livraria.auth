using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.livraria.auth.model.Request;
using api.livraria.auth.model.Response;
using api.livraria.auth.Model.Entidades;

namespace api.livraria.auth.Model.Interfaces.Business.User
{
    public interface IUserBusiness
    {
        UsuarioResponse Create(UsuarioRequest user);

        UsuarioResponse FindById(long id);

        List<Usuario> FindAll();

        Usuario Update(Usuario usuario);

        void Delete(long id);
    }
}
