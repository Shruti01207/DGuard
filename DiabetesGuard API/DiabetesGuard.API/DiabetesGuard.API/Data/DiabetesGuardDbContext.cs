using DiabetesGuard.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DiabetesGuard.API.Data
{
    public class DiabetesGuardDbContext: DbContext
    {

        public DiabetesGuardDbContext(DbContextOptions options ) : base(options)
        {
            
        }

        public DbSet<MedicalBackground> MedicalBackgrounds { get; set; }



    }
}
