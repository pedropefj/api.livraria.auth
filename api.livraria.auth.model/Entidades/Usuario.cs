using System;
using Newtonsoft.Json;

namespace api.livraria.auth.Model.Entidades
{
    public class Usuario
    {
        [JsonIgnore]
        public long? Id { get; set; }

        public string Nome { get; set; }

        public string UserName{ get; set; }

        public string Email { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public string Senha { get; set; }
    }
}
