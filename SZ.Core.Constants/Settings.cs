using Microsoft.AspNetCore.Identity;

using System;

namespace SZ.Core.Constants
{
    public static class Settings
    {
        public static class Users
        {
            static Guid _AdminId;
            public static Guid AdminId
            {
                get
                {
                    if (_AdminId == default)
                        _AdminId = Guid.Parse("2ca4c2fb-1033-4cb7-bd81-66adafd61867"); ;

                    return _AdminId;
                }
            }

            public static readonly string AdminUserName = "Admin";
            /// <summary>
            /// 1234Qwerty
            /// </summary>
            public static readonly string AdminPasswordHash = "AQAAAAEAACcQAAAAEEUFx0jIuCPgIwq13eLmj/7r07Q2A2dMO02o+XPlxcRU4xFeHxOZx3Dq4L3Icf+KVw==";
            public static readonly string AdminConcurrencyStamp = "2177d55a-454a-4bec-8631-8f9e79767a64";
            public static readonly string AdminSecurityStamp = "7FI7SGIK5V7WVU7FPQXRCTS5DH3YXH2C";
        }

        public static class Roles
        {
            static Guid _AdminId;
            public static Guid AdminId
            {
                get
                {
                    if (_AdminId == default)
                        _AdminId = Guid.Parse("c037b07d-5a25-4931-ba03-1ba04a695380"); ;

                    return _AdminId;
                }
            }
            public static readonly string AdminConcurrencyStamp = "8daaf047-c435-4e0e-8185-a941bbd96f75";
        }
        public static PasswordOptions PasswordOptions => new PasswordOptions
        {
            RequireDigit = true,
            RequiredLength = 5,
            RequiredUniqueChars = 2,
            RequireLowercase = false,
            RequireNonAlphanumeric = false,
            RequireUppercase = false
        };
    }
}
