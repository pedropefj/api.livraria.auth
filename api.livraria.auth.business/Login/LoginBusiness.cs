using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using api.livraria.auth.model.Interfaces.Business.Login;
using api.livraria.auth.model.Request;
using api.livraria.auth.model.Util;
using api.livraria.auth.Model.Interfaces.Services.User;
using api.livraria.auth.seguranca.Configuration;
using api.livraria.auth.Util;

namespace api.livraria.auth.business.Login
{
    public class LoginBusiness : ILoginBusiness
    {
        private IUserService _userService;
        private SigningConfigurations _signingConfigurations;
        private TokenConfiguration _tokenConfigurations;


        public LoginBusiness(IUserService userService, SigningConfigurations signingConfigurations, TokenConfiguration tokenConfiguration)
        {
            _userService = userService;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfiguration;
        }

        public object FindByLogin(TokenRequest user)
        {
            bool credentialsIsValid = false;
            if (user != null && (!string.IsNullOrWhiteSpace(user.Login) ))
            {
                try {
                    var baseUser = _userService.FindByLogin(user.Login);
                    credentialsIsValid = (baseUser != null && (user.Login == baseUser.UserName || user.Login == baseUser.Email) && user.Senha == baseUser.Senha);

                }
                catch (ServicoException)
                {
                    throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0004"));

                }
            }
            else
            {
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0005"));

            }
            if (credentialsIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Login"),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                        }
                    );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return SuccessObject(createDate, expirationDate, token);
            }
            else
            {
                throw new ValidacaoException(MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M0006"));
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Failed to autheticate"
            };
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}
