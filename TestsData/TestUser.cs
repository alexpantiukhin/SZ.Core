using System;

namespace TestData
{sdfsdfsdfsdf
    public static class TestUsers
    {
        public static class User1
        {
            static Guid _Id;
            public static Guid Id
            {
                get
                {
                    if (_Id == default)
                        _Id = Guid.NewGuid();

                    return _Id;
                }
            }

            public static string UserName => "User1";
            public static string Phone => "79991112231";
        }
        public static class User2
        {
            static Guid _Id;
            public static Guid Id
            {
                get
                {
                    if (_Id == default)
                        _Id = Guid.NewGuid();

                    return _Id;
                }
            }

            public static string UserName => "User2";
            public static string Phone => "79991112232";
        }
        public static class User3
        {
            static Guid _Id;
            public static Guid Id
            {
                get
                {
                    if (_Id == default)
                        _Id = Guid.NewGuid();

                    return _Id;
                }
            }

            public static string UserName => "User3";
            public static string Phone => "79991112233";
        }

    }
}
