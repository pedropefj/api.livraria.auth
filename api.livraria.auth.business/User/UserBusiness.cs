using System;
using System.Collections.Generic;
using api.livraria.auth.model.Request;
using api.livraria.auth.model.Response;
using api.livraria.auth.model.Util;
using api.livraria.auth.Model.Entidades;
using api.livraria.auth.Model.Interfaces.Business.User;
using api.livraria.auth.Model.Interfaces.Services.User;
using api.livraria.auth.Util;
using System.Net;
namespace api.livraria.auth.Business.User
{
    public class UserBusiness : IUserBusiness
    {
        private IUserService _userService;

        public UserBusiness(IUserService userService)
        {
            _userService = userService;
        }
        public UsuarioResponse Create(UsuarioRequest user)
        {
            if (user == null)
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest,"M0001"));
            if(string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Nome) | string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Senha))
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0005"));
            try
            {
                if(_userService.ExistEmail(user.Email))
                    throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0003"));
                if (_userService.ExistUserName(user.UserName))
                    throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0002"));

                Usuario usuario = new Usuario()
                {
                    Nome = user.Nome,
                    Email = user.Email,
                    DataCriacao = DateTime.Now,
                    Senha = user.Senha,
                    UserName = user.UserName
                };

                return _userService.Create(usuario);
            }
            catch (ServicoException)
            {
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0004"));

            }

        }

        public void Delete(long id)
        {
            if (id <= 0)
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0001"));
            try
            {
                _userService.Delete(id);
            }
            catch (ServicoException)
            {
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0004"));

            }
        }

        public List<Usuario> FindAll()
        {
            try
            {
                return _userService.FindAll();
            }
            catch (ServicoException)
            {
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0004"));

            }
        }

        public UsuarioResponse FindById(long id)
        {
            if(id<=0)
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0001"));
            try{ 
                Usuario usuario = _userService.FindById(id);
               
                if(usuario ==  null)
                    throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.NoContent,""));

                return new UsuarioResponse()
                {
                    Nome = usuario.Nome,
                    UserName = usuario.UserName,
                    DataCriacao = usuario.DataCriacao,
                    Email = usuario.Email
                }; 
            }
            catch (ServicoException)
            {
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0004"));

            }
        }

        public UsuarioResponseUpdate Update(UsuarioRequest user, int id)
        {
            if (user == null)
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0001"));
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Nome) | string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Senha))
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0005"));
            if (id <= 0)
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0001"));

            Usuario usuario = new Usuario()
            {
                Nome = user.Nome,
                Email = user.Email,
                DataCriacao = DateTime.Now,
                Senha = user.Senha,
                UserName = user.UserName,
                DataAtualizacao = DateTime.Now,
                Id = id
            };

            Usuario usuarioResp = null;

            try {

                usuarioResp = _userService.Update(usuario);

                if (usuario == null)
                    throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.NoContent, ""));

                return new UsuarioResponseUpdate()
                {
                    Nome = usuario.Nome,
                    UserName = usuario.UserName,
                    DataCriacao = usuario.DataCriacao,
                    DataAtualizacao = usuario.DataAtualizacao,
                    Email = usuario.Email
                };

            }
            catch (ServicoException)
            {
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0004"));

            }
        }
    }
}
