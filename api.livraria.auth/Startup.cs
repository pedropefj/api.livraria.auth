using System;
using System.Collections.Generic;
using System.IO;
using api.livraria.auth.business.Login;
using api.livraria.auth.Business.User;
using api.livraria.auth.model.Context;
using api.livraria.auth.model.Interfaces.Business.Login;
using api.livraria.auth.Model.Interfaces.Business.User;
using api.livraria.auth.Model.Interfaces.Services.User;
using api.livraria.auth.seguranca.Configuration;
using api.livraria.auth.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;

namespace api.livraria.auth
{
    public class Startup
    {

        private Container container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var conection = Configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(conection));

            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new JsonNameToPropertyNameContractResolver()
            };

            services.AddMvc().AddWebApiConventions(); // Adicionar WebApi

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddApiVersioning();

            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Livraria API",
                        Version = "v1",
                        Description = "Exemplo de API REST criada com o ASP.NET Core",
                        Contact = new Contact
                        {
                            Name = "Pedro Eustáquio",
                            Url = "https://github.com/pedropefj"
                        }
                    });


                c.OperationFilter<ExamplesOperationFilter>(); // [SwaggerRequestExample] & [SwaggerResponseExample]


                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");
                c.IncludeXmlComments(caminhoXmlDoc);

            });

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Enables the use of the token as a means of
            // authorizing access to this project's resources
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            //Content negociation - Support to XML and JSON
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));

            })
            .AddXmlSerializerFormatters();

            // Injetor de dependências
            IntegrateSimpleInjector(services);

        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(container));

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();

            InitializeContainer(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Livraria API Authorization");
            });

            var options = new RewriteOptions();
            options.AddRedirect("^$", "swagger");
            app.UseRewriter(options);

        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            container.RegisterMvcControllers(app);
            container.RegisterMvcViewComponents(app);

            // Add application services. For instance:
            container.Register<IUserBusiness, UserBusiness>(Lifestyle.Scoped);
            container.Register<IUserService, UserService>(Lifestyle.Scoped);
            container.Register<ILoginBusiness, LoginBusiness>(Lifestyle.Scoped);

            // Allow Simple Injector to resolve services from ASP.NET Core.
            container.AutoCrossWireAspNetComponents(app);
        }
    }

    public class JsonNameToPropertyNameContractResolver : CamelCasePropertyNamesContractResolver
    {
        /// <summary>
        /// Método para gerar os nomes de propriedades.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);

            foreach (JsonProperty prop in list)
            {
                string name = Char.ToLowerInvariant(prop.UnderlyingName[0]) + prop.UnderlyingName.Substring(1);
                prop.PropertyName = name;
            }

            return list;
        }
    }

}
