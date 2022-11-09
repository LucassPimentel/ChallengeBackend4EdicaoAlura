using ChallengeBackend4EdicaoAlura.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeBackend4EdicaoAlura.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> opts) : base(opts)
        {

        }

        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Despesa> Despesas { get; set; }

    }
}
