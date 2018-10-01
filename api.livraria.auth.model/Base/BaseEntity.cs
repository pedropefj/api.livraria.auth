using System.ComponentModel.DataAnnotations.Schema;

namespace api.livraria.auth.model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long? Id { get; set; }
    }
}
