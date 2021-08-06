using Al.Components.EF.Abstractions.Interfaces;

using System;

namespace SZ.Core.Models.Db
{
    /// <summary>
    /// Член десятки
    /// </summary>
    public class UserTen : IDBEntity
    {
        public Guid Id { get; set; }
        /// <summary>
        /// id пользователя десятки
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// id десятки
        /// </summary>
        public Guid TenId { get; set; }
        /// <summary>
        /// id решения документа о входе в десятку
        /// </summary>
        public Guid BasisEntranceDocumentId { get; set; }
        /// <summary>
        /// id решения документа о выходе из десятки
        /// </summary>
        public Guid? BasisExitDocumentId { get; set; }
        public int ShowId { get; set; }




        /// <summary>
        /// Десятка
        /// </summary>
        public Ten Ten { get; set; }
        /// <summary>
        /// Пользователь десятки
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Решение документа о входе в десятку
        /// </summary>
        public Document BasisEntranceDocument { get; set; }
        /// <summary>
        /// Решение документа о выходе из десятки
        /// </summary>
        public Document BasisExitDocument { get; set; }

        public override string ToString()
        {
            return "Десятка - " + TenId + ", Пользователь - " + User.ToString();
        }
    }
}
