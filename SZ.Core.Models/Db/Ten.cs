using Al.Components.EF.Abstractions.Interfaces;

using System;
using System.Collections.Generic;

namespace SZ.Core.Models.Db
{
    /// <summary>
    /// Десятка
    /// </summary>
    public class Ten : IDBEntity
    {
        //public Ten()
        //{
        //    ChildTens = new HashSet<Ten>();
        //    UserTens = new HashSet<UserTen>();
        //    Protocols = new HashSet<Document>();
        //}
        public Guid Id { get; set; }
        /// <summary>
        /// Id вышестоящей десятки
        /// </summary>
        public Guid? ParentTenId { get; set; }
        /// <summary>
        /// Дата создания десятки
        /// </summary>
        public DateTimeOffset DTCUTC { get; set; }
        /// <summary>
        /// Пользователь, создавший десятку
        /// </summary>
        public Guid CreatorId { get; set; }
        /// <summary>
        /// Круг десятки
        /// </summary>
        public byte Circle { get; set; }
        /// <summary>
        /// Id Земства
        /// </summary>
        public Guid ZemstvoId { get; set; }
        public int ShowId { get; set; }



        /// <summary>
        /// Вышестоящая десятка
        /// </summary>
        public Ten ParentTen { get; set; }
        /// <summary>
        /// Земство
        /// </summary>
        public Zemstvo Zemstvo { get; set; }
        /// <summary>
        /// Создатель записи
        /// </summary>
        public User Creator { get; set; }


        /// <summary>
        /// Вопросы по выбору делегатов в десятку
        /// </summary>
        public ICollection<Question> DelegateQuestions { get; set; }

        /// <summary>
        /// Нижестоящие десятки
        /// </summary>
        public ICollection<Ten> ChildTens { get; set; }
        /// <summary>
        /// Члены десятки
        /// </summary>
        public ICollection<UserTen> UserTens { get; set; }
        /// <summary>
        /// Протоколы десятки
        /// </summary>
        public ICollection<Document> Protocols { get; set; }

        public override string ToString()
        {
            return "№" + Id + ", круг - " + Circle + (Zemstvo == null ? "" : (", земство - " + Zemstvo.Name + ". "));
        }
    }
}
