using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;

using SZ.Core.Constants;
using SZ.Core.Models.Db;

namespace TestData
{
    public class TestDBFactory : IDbContextFactory<SZDb>
    {
        readonly string permanentIdDB;

        public TestDBFactory()
        {

        }

        public TestDBFactory(bool permanent)
        {
            if (permanent)
                permanentIdDB = Guid.NewGuid().ToString();
        }

        public SZDb CreateDbContext()
        {
            SZDb _SZDb;

            try
            {
                var builder = new DbContextOptionsBuilder<SZDb>();
                builder.UseInMemoryDatabase(permanentIdDB ?? Guid.NewGuid().ToString());
                builder.EnableSensitiveDataLogging();
                DbContextOptions<SZDb> _options = builder.Options;

                _SZDb = new SZDb(_options);
                _SZDb.Database.EnsureCreated();

                //_SZDb.Roles.Add(new IdentityRole<Guid>
                //{
                //    Id = Settings.Roles.AdminId,
                //    ConcurrencyStamp = Settings.Roles.AdminConcurrencyStamp,
                //    Name = Settings.Users.AdminUserName,
                //    NormalizedName = Settings.Users.AdminUserName
                //});

                //_SZDb.Users.Add(new User
                //{
                //    Id = Settings.Users.AdminId,
                //    UserName = Settings.Users.AdminUserName,
                //    ConcurrencyStamp = Settings.Users.AdminConcurrencyStamp,
                //    SecurityStamp = Settings.Users.AdminSecurityStamp,
                //    DTCUTC = DateTime.Parse("01.01.2020"),
                //    FirstName = Settings.Users.AdminUserName,
                //    SecondName = Settings.Users.AdminUserName,
                //    Patronym = Settings.Users.AdminUserName,
                //    PasswordHash = Settings.Users.AdminPasswordHash,
                //    NormalizedUserName = Settings.Users.AdminUserName.ToUpper()
                //});

                //_SZDb.UserRoles.Add(new IdentityUserRole<Guid>
                //{
                //    UserId = Settings.Users.AdminId,
                //    RoleId = Settings.Roles.AdminId
                //});

                try
                {
                    _SZDb.SaveChanges();

                }
                catch
                {

                }

            }
            catch (Exception e)
            {
                throw;
            }

            return _SZDb;
        }
    }
}
