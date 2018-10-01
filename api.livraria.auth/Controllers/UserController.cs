using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.livraria.auth.model.Request;
using api.livraria.auth.model.Response;
using api.livraria.auth.model.Util;
using api.livraria.auth.Model.Interfaces.Business.User;
using api.livraria.auth.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Examples;

namespace api.livraria.auth.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class UserController : BaseController
    {
        private IUserBusiness _userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }
        /// <summary>
        /// Lista todos usuários.
        /// </summary>
        /// <response code="200">Usuários</response>
        /// <response code="204">Usuário não encontrado."</response>
        /// <response code="400">"Ocorreu um erro na execução."</response>
        [HttpGet]

        [SwaggerResponseExample(200, typeof(UsuariosResponseModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerResponseExample(400, typeof(AllUsariosResponse400), jsonConverter: typeof(StringEnumConverter))]
        [ProducesResponseType(200, Type = typeof(UsuariosResponse))]
        [ProducesResponseType(400, Type = typeof(MensagemError))]
        [ProducesResponseType(204, Type = null)]
        [Authorize("Bearer")]

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


        /// <summary>
        /// Lista usuário por Id.
        /// </summary>
        ///<param name="id"></param>  
        ///  <response code="200">Usuário</response>
        /// <response code="204">Usuário não encontrado."</response>
        /// <response code="400">"Ocorreu um erro na execução."</response>
        [HttpGet("{id}")]
        [SwaggerResponseExample(200, typeof(UsuarioResponseModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerResponseExample(400, typeof(UsuarioResponse400), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(UsuarioRequest), typeof(UsuarioRequestModelExample), jsonConverter: typeof(StringEnumConverter))]
        [ProducesResponseType(200, Type = typeof(UsuarioResponse))]
        [ProducesResponseType(400, Type = typeof(MensagemError))]
        [ProducesResponseType(204, Type = null)]
        [Authorize("Bearer")]

        public HttpResponseMessage Get([FromUri]int id)
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

        /// <summary>
        /// Atualizar Usuário
        /// </summary>
        ///  <response code="200">Usuário Atualizado</response>
        /// <response code="400">"Ocorreu um erro na execução."</response>
        [HttpPut("{id}")]
        [SwaggerResponseExample(200, typeof(UsuarioResponseUpdateModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerResponseExample(400, typeof(PutUsarioResponse400), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(UsuarioRequest), typeof(UsuarioRequestModelExample), jsonConverter: typeof(StringEnumConverter))]
        [ProducesResponseType(200, Type = typeof(UsuarioResponseUpdate))]
        [ProducesResponseType(400, Type = typeof(MensagemError))]
        [Authorize("Bearer")]

        public HttpResponseMessage Update([FromBody] UsuarioRequest usuario, [FromUri] int id)
        {
            try
            {

                return ResponseBasicJson(HttpStatusCode.OK, _userBusiness.Update(usuario,id));
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
        /// Deleta usuário por Id.
        /// </summary>
        ///<param name="id"></param>  
        ///  <response code="200">Usuário Deletado</response>
        /// <response code="400">"Ocorreu um erro na execução."</response>
        [HttpDelete("{id}")]
        [SwaggerResponseExample(400, typeof(UsuarioResponse400), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(UsuarioRequest), typeof(UsuarioRequestModelExample), jsonConverter: typeof(StringEnumConverter))]
        [ProducesResponseType(200, Type = null)]
        [ProducesResponseType(400, Type = typeof(MensagemError))]
        [Authorize("Bearer")]


        public HttpResponseMessage Delete([FromUri]int id)
        {
            try
            {
                _userBusiness.Delete(id);
                return ResponseBasicJson(HttpStatusCode.OK, null);
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