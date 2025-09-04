using AzureFunctions.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureFunctions.Functions.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
    }
}
