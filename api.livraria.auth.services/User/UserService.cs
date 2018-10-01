using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using api.livraria.auth.model.Context;
using api.livraria.auth.model.Request;
using api.livraria.auth.model.Response;
using api.livraria.auth.model.Util;
using api.livraria.auth.Model.Entidades;
using api.livraria.auth.Model.Interfaces.Services.User;
using api.livraria.auth.Util;

namespace api.livraria.auth.Services.User
{
    public class UserService : IUserService
    {
        private MySQLContext _context;
        public UserService(MySQLContext context)
        {
            _context = context;
        }
        public UsuarioResponse Create(Usuario user)
        {
            UsuarioResponse usuarioResponse = new UsuarioResponse();

            try
            {
                _context.Add(user);
                _context.SaveChanges();

                usuarioResponse = new UsuarioResponse()
                {
                    UserName = user.UserName,
                    Nome = user.Nome,
                    DataCriacao = user.DataCriacao,
                    Email = user.Email
                };

            }
            catch(Exception e)
            {

            }

            return usuarioResponse;

        }

        public void Delete(long? id)
        {
            var result = _context.Usuarios.SingleOrDefault(u => u.Id == id);

            try
            {
                if (result != null)
                {
                    _context.Usuarios.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> FindAll()
        {
            try
            {
                return _context.Usuarios.ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario FindById(long? id)
        {
            return _context.Usuarios.SingleOrDefault(p => p.Id == id);

        }

        public Usuario Update(Usuario user)
        {
            if (!Exist(user.Id))
            {
                return new Usuario();
            }

            var result = _context.Usuarios.SingleOrDefault(u => u.Id.Equals(user.Id));

            try
            {
                _context.Entry(result).CurrentValues.SetValues(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Exist(long? id)
        {
            return _context.Usuarios.Any(u => u.Id == id);
        }

        public bool ExistUserName(string UserName)
        {
            try
            {
                return _context.Usuarios.Any(u => u.UserName.Equals(UserName));
            }
            catch
            {
                throw new ServicoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0005"));
            }
        }

        public bool ExistEmail(string Email)
        {
            try{

                return _context.Usuarios.Any(u => u.Email.Equals(Email));

            }
            catch
            {
                throw new ServicoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0005"));
            }
        }

    }
}
