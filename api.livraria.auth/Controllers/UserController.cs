using System;
using System.Net;
using System.Net.Http;
using api.livraria.auth.model.Request;
using api.livraria.auth.model.Response;
using api.livraria.auth.model.Util;
using api.livraria.auth.Model.Interfaces.Business.User;
using api.livraria.auth.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Examples;

namespace api.livraria.auth.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : BaseController
    {
        private IUserBusiness _userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }
        [HttpGet]
        /// <summary>
        /// Lista todos usuários.
        /// </summary>
        /// <response code="200">Usuários</response>
        /// <response code="204">Usuário não encontrado."</response>
        /// <response code="400">"Ocorreu um erro na execução."</response>
        [SwaggerResponseExample(200, typeof(UsuariosResponseModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerResponseExample(400, typeof(GetAllUsariosResponse400), jsonConverter: typeof(StringEnumConverter))]
        [ProducesResponseType(200, Type = typeof(UsuariosResponse))]
        [ProducesResponseType(400, Type = typeof(MensagemError))]
        public HttpResponseMessage GetAll()
        {
            try
            {

                return ResponseBasicJson(HttpStatusCode.OK, _userBusiness.FindAll());
            }
            catch (ValidacaoException e)
            {
                return ResponseBasicJson(e.MensagemError.StatusCode, e.MensagemError);
            }
            catch (Exception e)
            {
                MensagemError msg = MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M199");
                return ResponseBasicJson(msg.StatusCode, msg);
            }
        }
        
        [HttpGet("{id}")]
        /// <summary>
        /// Lista usuário por Id.
        /// </summary>
        ///<param name="id"></param>  
        ///  <response code="200">Usuário</response>
        /// <response code="204">Usuário não encontrado."</response>
        /// <response code="400">"Ocorreu um erro na execução."</response>
        [SwaggerResponseExample(200, typeof(UsuarioResponseModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerResponseExample(400, typeof(GetUsarioResponse400), jsonConverter: typeof(StringEnumConverter))]
        [ProducesResponseType(200, Type = typeof(UsuarioResponse))]
        [ProducesResponseType(400, Type = typeof(MensagemError))]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                UsuarioResponse usuarioResponse = _userBusiness.FindById(id);
                return ResponseBasicJson(HttpStatusCode.OK, usuarioResponse);
            }
            catch (ValidacaoException e)
            {
                return ResponseBasicJson(e.MensagemError.StatusCode, e.MensagemError);
            }
            catch (Exception e)
            {
                MensagemError msg = MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M199");
                return ResponseBasicJson(msg.StatusCode, msg);
            }
        }

        /// <summary>
        /// Cadastro de Usuário
        /// </summary>
        ///  <response code="201">Usuário Criado</response>
        /// <response code="400">"Ocorreu um erro na execução."</response>
        [HttpPost]
        [SwaggerResponseExample(201, typeof(UsuarioResponseModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerResponseExample(400, typeof(PostUsarioResponse400), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(UsuarioRequest), typeof(UsuarioRequestModelExample), jsonConverter: typeof(StringEnumConverter))]
        [ProducesResponseType(201, Type = typeof(UsuarioResponse))]
        [ProducesResponseType(400, Type = typeof(MensagemError))]
        public HttpResponseMessage Post([FromBody] UsuarioRequest usuario)
        {
            try{

                return ResponseBasicJson(HttpStatusCode.Created, _userBusiness.Create(usuario));
            }
            catch (ValidacaoException e)
            {
                return ResponseBasicJson(e.MensagemError.StatusCode, e.MensagemError);
            }
            catch (Exception e)
            {
                MensagemError msg = MensagensUtil.ObterMensagem(HttpStatusCode.BadRequest, "M199");
                return ResponseBasicJson(msg.StatusCode, msg);
            }
        }
    }
}