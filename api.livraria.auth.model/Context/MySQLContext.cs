using api.livraria.auth.Model.Entidades;
using Microsoft.EntityFrameworkCore;

namespace api.livraria.auth.model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() { }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options){ }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
