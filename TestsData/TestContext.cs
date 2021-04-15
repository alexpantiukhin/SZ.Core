
//using Microsoft.AspNetCore.Identity;

//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;

//using SZ.Core.Abstractions.Interfaces;
//using SZ.Core.Constants;
//using SZ.Models.Db;

//namespace SZ.Test.TestData
//{
//    public class TestContext : IDBContext
//    {
//        public List<User> Users { get; set; } = new List<User>();
//        public List<IdentityRole<Guid>> Roles { get; set; } = new List<IdentityRole<Guid>>();
//        public List<IdentityUserRole<Guid>> UserRoles { get; set; } = new List<IdentityUserRole<Guid>>();
//        public List<Document> Documents { get; set; } = new List<Document>();
//        public List<DocumentDecision> DocumentDecisions { get; set; } = new List<DocumentDecision>();
//        public List<DocumentUser> DocumentUsers { get; set; } = new List<DocumentUser>();
//        public List<Position> Positions { get; set; } = new List<Position>();
//        public List<ProtocolQuestionRepeat> ProtocolQuestionRepeats { get; set; } = new List<ProtocolQuestionRepeat>();
//        public List<Question> Questions { get; set; } = new List<Question>();
//        public List<QuestionRepeatAnswer> QuestionRepeatAnswers { get; set; } = new List<QuestionRepeatAnswer>();
//        public List<Ten> Tens { get; set; } = new List<Ten>();
//        public List<ZemstvoUserPosition> ZemstvoUserPositions { get; set; } = new List<ZemstvoUserPosition>();
//        public List<UserTen> UserTens { get; set; } = new List<UserTen>();
//        public List<Zemstvo> Zemstvos { get; set; } = new List<Zemstvo>();
//        public List<QuestionRepeat> QuestionRepeats { get; set; } = new List<QuestionRepeat>();

//        public TestContext()
//        {
//            var admin = AddUser(0);

//            Roles.Add(new IdentityRole<Guid>
//            {
//                Id = Settings.Roles.AdminId,
//                Name = Settings.Roles.AdminName
//            });

//            UserRoles.Add(new IdentityUserRole<Guid>
//            {
//                UserId = admin.Id,
//                RoleId = Settings.Roles.AdminId
//            });
//        }

//        public IDBSet<T> GetDbSet<T>() where T : class
//        {
//            return GetDbSet<T, User>(Users)
//                ?? GetDbSet<T, IdentityRole<Guid>>(Roles)
//                ?? GetDbSet<T, IdentityUserRole<Guid>>(UserRoles)
//                ?? GetDbSet<T, DocumentDecision>(DocumentDecisions)
//                ?? GetDbSet<T, DocumentUser>(DocumentUsers)
//                ?? GetDbSet<T, Position>(Positions)
//                ?? GetDbSet<T, ProtocolQuestionRepeat>(ProtocolQuestionRepeats)
//                ?? GetDbSet<T, Question>(Questions)
//                ?? GetDbSet<T, QuestionRepeatAnswer>(QuestionRepeatAnswers)
//                ?? GetDbSet<T, Ten>(Tens)
//                ?? GetDbSet<T, ZemstvoUserPosition>(ZemstvoUserPositions)
//                ?? GetDbSet<T, UserTen>(UserTens)
//                ?? GetDbSet<T, Zemstvo>(Zemstvos)
//                ?? GetDbSet<T, QuestionRepeat>(QuestionRepeats);
//        }

//        IDBSet<T> GetDbSet<T, TEntity>(List<TEntity> list)
//            where T : class
//            where TEntity : class
//        {
//            if (typeof(T) == typeof(TEntity))
//                return new TestDBSet<T, TEntity>(list);

//            return null;
//        }


//        public async Task<int> SaveChangesAsync(int count = 1, CancellationToken token = default)
//        {
//            return count;
//        }


//        /// <summary>
//        /// 0 - админ, далее по порядку
//        /// </summary>
//        /// <param name="counter"></param>
//        /// <returns></returns>
//        public User AddUser(int counter)
//        {
//            Guid id;
//            string UserName;
//            string PassHash = $"passHash_{counter}";
//            string Email = $"email@email.{counter}";
//            string Phone;

//            var a = counter.ToString();
//            var array = new char[11 - a.Length];

//            for (int i = 0; i < array.Length; i++)
//            {
//                array[i] = '0';
//            }

//            var prefix = string.Join("", array);
//            Phone = prefix + a;

//            if (counter == 0)
//            {
//                id = Settings.Users.AdminId;
//                UserName = Settings.Users.AdminUserName;
//            }
//            else
//            {
//                id = Guid.NewGuid();
//                UserName = $"User{counter}";
//            }

//            var user = new User
//            {
//                Id = id,
//                UserName = UserName,
//                PhoneNumber = Phone,
//                PasswordHash = PassHash,
//                Email = Email
//            };

//            Users.Add(user);

//            return user;
//        }
//    }
//}
