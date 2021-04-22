using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;

using SZ.Core.Constants;
using SZ.Core.Models.Db;

namespace TestData
{
    public class TestDBFactory : IDbContextFactory<SZDb>
    {

        public SZDb CreateDbContext()
        {
            SZDb _SZDb;

            try
            {
                var builder = new DbContextOptionsBuilder<SZDb>();
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
                builder.EnableSensitiveDataLogging();
                DbContextOptions<SZDb> _options = builder.Options;

                _SZDb = new SZDb(_options);

                _SZDb.Roles.Add(new IdentityRole<Guid>
                {
                    Id = Settings.Roles.AdminId,
                    ConcurrencyStamp = Settings.Roles.AdminConcurrencyStamp,
                    Name = Settings.Users.AdminUserName,
                    NormalizedName = Settings.Users.AdminUserName
                });

                _SZDb.Users.Add(new User
                {
                    Id = Settings.Users.AdminId,
                    UserName = Settings.Users.AdminUserName,
                    ConcurrencyStamp = Settings.Users.AdminConcurrencyStamp,
                    SecurityStamp = Settings.Users.AdminSecurityStamp,
                    DTCUTC = DateTime.Now,
                    FirstName = Settings.Users.AdminUserName,
                    SecondName = Settings.Users.AdminUserName,
                    Patronym = Settings.Users.AdminUserName,
                    PasswordHash = Settings.Users.AdminPasswordHash
                });

                _SZDb.UserRoles.Add(new IdentityUserRole<Guid>
                {
                    UserId = Settings.Users.AdminId,
                    RoleId = Settings.Roles.AdminId
                });


                _SZDb.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }

            return _SZDb;
        }
    }
}
