using ChallengeBackend4EdicaoAlura.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeBackend4EdicaoAlura.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> opts) : base(opts)
        {

        }

        public DbSet<ReceitaModel> Receitas { get; set; }
        public DbSet<DespesaModel> Despesas { get; set; }

    }
}
