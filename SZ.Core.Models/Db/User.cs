using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;

using SZ.Core.Models.Interfaces;

namespace SZ.Core.Models.Db
{
    public class User : IdentityUser<Guid>, IDBEntity
    {
        //public User()
        //{
        //    UserRoles = new HashSet<UserRole>();
        //    CreatedDocuments = new HashSet<Document>();
        //    CreatedTens = new HashSet<Ten>();
        //    UserTens = new HashSet<UserTen>();
        //    DocumentUsers = new HashSet<DocumentUser>();
        //    DocumentDecisions = new HashSet<DocumentDecision>();
        //    Questions = new HashSet<Question>();
        //    ZemstvoPositions = new HashSet<ZemstvoUserPosition>();
        //    CreatedUserPositions = new HashSet<ZemstvoUserPosition>();
        //}


        //[Display(Name = "№")]
        public override Guid Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        //[Display(Name = "Имя")]
        //[Required(ErrorMessage = "Имя обязательно")]
        //[MaxLength(250, ErrorMessage = "Имя должно содержать не более 250 символов")]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        //[Display(Name = "Фамилия")]
        //[Required(ErrorMessage = "Фамилия обязательна")]
        //[MaxLength(250, ErrorMessage = "Фамилия должна содержать не более 250 символов")]
        public string SecondName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        //[Display(Name = "Отчество")]
        //[Required(ErrorMessage = "Отчество обязательно")]
        //[MaxLength(250, ErrorMessage = "Отчество должно содержать не более 250 символов")]
        public string Patronym { get; set; }

        //[Display(Name = "Телефон")]
        //[Required(ErrorMessage = "Телефон обязателен")]
        //[Phone(ErrorMessage = "Телефон должен содержать 11 цифр")]
        //[MaxLength(11, ErrorMessage = "Телефон должен содержать 11 цифр")]
        //[MinLength(11, ErrorMessage = "Телефон должен содержать 11 цифр")]
        public override string PhoneNumber { get; set; }

        //[Display(Name = "Дата рождения")]
        //[Required(ErrorMessage = "Дата рождения обязательна")]
        public DateTime Birthday { get; set; }
        /// <summary>
        /// Тип документа, удостоверяющего личность
        /// </summary>
        //[MaxLength(20, ErrorMessage = "Тип документа должен содержать не более 20 символов")]
        public string DocType { get; set; }
        /// <summary>
        /// Серия документа, удостоверяющего личность
        /// </summary>
        //[MaxLength(10, ErrorMessage = "Серия документа должна содержать не более 10 символов")]
        public string DocSeria { get; set; }
        /// <summary>
        /// Номер документа, удостоверяющего личность
        /// </summary>
        //[MaxLength(20, ErrorMessage = "Серия документа должна содержать не более 10 символов")]
        public string DocNumber { get; set; }
        /// <summary>
        /// Дата выдачи документа, удостоверяющего личность
        /// </summary>
        //[DataType(DataType.Date)]
        public DateTime? DocDate { get; set; }
        /// <summary>
        /// Организация, вадавшая документ, удостоверяющий личность
        /// </summary>
        //[MaxLength(250, ErrorMessage = "Организация, выдавшая документ должна содержать не более 250 символов")]
        public string DocOrganization { get; set; }
        /// <summary>
        /// Регион проживания
        /// </summary>
        //[MaxLength(250, ErrorMessage = "Регион должен содержать не более 250 символов")]
        public string Region { get; set; }
        /// <summary>
        /// Район региона проживания
        /// </summary>
        //[MaxLength(250, ErrorMessage = "Район должен содержать не более 250 символов")]
        public string District { get; set; }
        /// <summary>
        /// Город проживания
        /// </summary>
        //[MaxLength(250, ErrorMessage = "Город должен содержать не более 250 символов")]
        public string City { get; set; }
        /// <summary>
        /// Населённый пункт проживания
        /// </summary>
        //[MaxLength(250, ErrorMessage = "Населённый пункт должен содержать не более 250 символов")]
        public string Locality { get; set; }
        /// <summary>
        /// Улица проживания
        /// </summary>
        //[MaxLength(250, ErrorMessage = "Улица должна содержать не более 250 символов")]
        public string Street { get; set; }
        /// <summary>
        /// Дом проживания
        /// </summary>
        //[MaxLength(10, ErrorMessage = "Дом должен содержать не более 10 символов")]
        public string House { get; set; }
        /// <summary>
        /// Строения проживания
        /// </summary>
        //[MaxLength(10, ErrorMessage = "Строегие должно содержать не более 10 символов")]
        public string Building { get; set; }
        /// <summary>
        /// Квартира проживания
        /// </summary>
        //[MaxLength(10, ErrorMessage = "Квартира должна содержать не более 10 символов")]
        public string Flat { get; set; }
        /// <summary>
        /// Комната проживания
        /// </summary>
        //[MaxLength(10, ErrorMessage = "Комната должна содержать не более 10 символов")]
        public string Room { get; set; }
        /// <summary>
        /// Пол. True - мужской, False - женский
        /// </summary>
        //[Display(Name = "Пол")]
        //[Required(ErrorMessage = "Пол обязателен")]
        public bool Gender { get; set; }
        /// <summary>
        /// Пользователь заблокирован
        /// </summary>
        public bool Block { get; set; }
        /// <summary>
        /// Дата блокировки пользователя
        /// </summary>
        public DateTime? DTBlockUTC { get; set; }
        /// <summary>
        /// Основание блокировки
        /// </summary>
        public string BlockBasis { get; set; }
        /// <summary>
        /// Id пользователя, заблокировавшего аккаунт
        /// </summary>
        public Guid? BlockerId { get; set; }
        /// <summary>
        /// Дата создания пользователя
        /// </summary>
        public DateTimeOffset DTCUTC { get; set; }



        /// <summary>
        /// Пользователь, заблокировавший аккаунт
        /// </summary>
        public User Blocker { get; set; }



        ///// <summary>
        ///// Роли пользователя в системе
        ///// </summary>
        //public ICollection<UserRole> UserRoles { get; set; }
        /// <summary>
        /// Созданные записи документов
        /// </summary>
        public ICollection<Document> CreatedDocuments { get; set; }
        /// <summary>
        /// Созданные записи десяток
        /// </summary>
        public ICollection<Ten> CreatedTens { get; set; }
        /// <summary>
        /// Десятки, в которые входил пользователь
        /// </summary>
        public ICollection<UserTen> UserTens { get; set; }
        /// <summary>
        /// Документы, в которых был обозначен пользователь
        /// </summary>
        public ICollection<DocumentUser> DocumentUsers { get; set; }
        ///// <summary>
        ///// Решения, которые принимал пользователь
        ///// </summary>
        //public ICollection<DocumentDecision> DocumentDecisions { get; set; }
        /// <summary>
        /// Вопросы, сгенерированные пользователем
        /// </summary>
        public ICollection<Question> Questions { get; set; }
        /// <summary>
        /// Вопросы, скорректированные пользователем
        /// </summary>
        public ICollection<QuestionRepeat> QuestionRepeats { get; set; }
        /// <summary>
        /// Должности, на которые выбран пользователь в Земствах
        /// </summary>
        public ICollection<ZemstvoUserPosition> ZemstvoPositions { get; set; }
        /// <summary>
        /// Записи назначенных на должности пользователей, созданные данным пользователем
        /// </summary>
        public ICollection<ZemstvoUserPosition> CreatedUserPositions { get; set; }
        /// <summary>
        /// Пользователи, заблокированные текущим пользователем
        /// </summary>
        public ICollection<User> BlockUsers { get; set; }

        /// <summary>
        /// Варианты ответов на голосованиях, где пользователь заявлен кандидатом
        /// </summary>
        public ICollection<QuestionRepeatAnswer> CandidatInAnswers { get; set; }

        /// <summary>
        /// Решения в протоколе, которые реально отметил пользователь
        /// </summary>
        public ICollection<DocumentDecision> RealDecisions { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}",
                string.IsNullOrWhiteSpace(SecondName) ? "" : SecondName,
                string.IsNullOrWhiteSpace(FirstName) ? "" : FirstName,
                string.IsNullOrWhiteSpace(Patronym) ? "" : Patronym);
        }

        public string ToStringShort()
        {
            return string.Format("{0} {1}{2}",
                string.IsNullOrWhiteSpace(SecondName) ? "" : SecondName,
                string.IsNullOrWhiteSpace(FirstName) ? "" : (FirstName.First() + "."),
                string.IsNullOrWhiteSpace(Patronym) ? "" : (Patronym.First() + "."));
        }

    }
}
