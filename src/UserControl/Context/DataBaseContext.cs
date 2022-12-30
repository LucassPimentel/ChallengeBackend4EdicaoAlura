using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserControl.Context
{
    public class DataBaseContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> opts) : base(opts)
        {

        }
    }
}
