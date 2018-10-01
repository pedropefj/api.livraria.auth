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

        UsuarioResponseUpdate Update(UsuarioRequest user, int id);

        void Delete(long id);
    }
}
