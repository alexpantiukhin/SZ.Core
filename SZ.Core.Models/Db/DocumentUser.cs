using Al.Components.DB.Interfaces;

using System;
using System.Collections.Generic;

namespace SZ.Core.Models.Db
{
    /// <summary>
    /// Пользователь документа. Если заявление или решение, то пользователь 1.
    /// Если протокол десятки, то пользователей множество
    /// </summary>
    public class DocumentUser : IDBEntity
    {
        //public DocumentUser()
        //{
        //    DocumentDecisions = new HashSet<DocumentDecision>();
        //}
        public Guid Id { get; set; }
        /// <summary>
        /// id документа, в котором обозначены пользователи
        /// </summary>
        public Guid DocumentId { get; set; }
        /// <summary>
        /// id пользователя, обозначенного в документе
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Присутствовал на собрании десятки.
        /// Указывается для протоколов собраний десяток
        /// </summary>
        public bool AttendsTheMeeting { get; set; }
        public int ShowId { get; set; }


        /// <summary>
        /// Документ, в котором обозначены пользователи
        /// </summary>
        public Document Document { get; set; }
        /// <summary>
        /// Пользователь, обозначенный в документе
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Решения, принятые пользователем в рамках документов
        /// </summary>
        public ICollection<DocumentDecision> DocumentDecisions { get; set; }
    }
}
