using DiabetesGuard.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiabetesGuard.API.Data
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Create reader and writer role
            var readerRoleId = "bb0b82eb-9278-448d-af99-36172f7429ca";
            var writerRoleId = "7bedc82a-494f-4ddf-82e8-09075e63c448 ";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper( ),
                    ConcurrencyStamp=readerRoleId
                },

                new IdentityRole()
                {
                    Id=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper( ),
                    ConcurrencyStamp=writerRoleId
                }

            };
            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            //Create an admin user
            var adminUserId = "86ef1250-178e-4379-8b7b-ead954249500";

            var admin = new User()
            {
                Id = adminUserId,
                UserName = "admin@DiabetesGuard.com",
                Email = "admin@DiabetesGuard.com",
                NormalizedEmail = "admin@DiabetesGuard.com".ToUpper(),
                NormalizedUserName = "admin@DiabetesGuard.com".ToUpper()
            };
            admin.PasswordHash = new PasswordHasher<User>().HashPassword(admin, "Admin@123");
            
            builder.Entity<User>().HasData(admin);

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                     UserId=adminUserId,
                     RoleId=readerRoleId
                },
                 new()
                {
                     UserId=adminUserId,
                     RoleId=writerRoleId
                }

            };


        }
    }
}
