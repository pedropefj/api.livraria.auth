using System.Collections.Generic;
using api.livraria.auth.model.Request;
using api.livraria.auth.model.Response;
using api.livraria.auth.Model.Entidades;

namespace api.livraria.auth.Model.Interfaces.Services.User
{
    public interface IUserService
    {
        UsuarioResponse Create(Usuario user);

        Usuario FindById(long? id);

        List<Usuario> FindAll();

        Usuario Update(Usuario usuario);

        void Delete(long? id);

        bool ExistEmail(string Email);

        bool ExistUserName(string UserName);

        Usuario FindByLogin(string login);
    }
}
