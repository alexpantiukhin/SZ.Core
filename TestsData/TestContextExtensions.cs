
using System;

using SZ.Core.Constants;
using SZ.Core.Models.Db;

namespace TestData
{
    public static class TestContextExtensions
    {
        public static User AddUser(this SZDb db, int counter)
        {
            Guid id;
            string UserName;
            string PassHash = $"passHash_{counter}";
            string Email = $"email@email.{counter}";
            string Phone;

            var a = counter.ToString();
            var array = new char[11 - a.Length];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = '0';
            }

            var prefix = string.Join("", array);
            Phone = prefix + a;

            if (counter == 0)
            {
                id = Settings.Users.AdminId;
                UserName = Settings.Users.AdminUserName;
            }
            else
            {
                id = Guid.NewGuid();
                UserName = $"User{counter}";
            }

            var user = new User
            {
                Id = id,
                UserName = UserName,
                PhoneNumber = Phone,
                PasswordHash = PassHash,
                Email = Email
            };

            db.Users.Add(user);

            db.SaveChanges();

            return user;
        }
    }
}
