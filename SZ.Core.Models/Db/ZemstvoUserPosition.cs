using System;
using System.Collections.Generic;

using SZ.Core.Constants;
using SZ.Core.Models.Interfaces;

namespace SZ.Core.Models.Db
{
    /// <summary>
    /// Документы решений по должностям Земства
    /// </summary>
    public class ZemstvoUserPosition : IDBEntity
    {
        public Guid Id { get; set; }
        /// <summary>
        /// id должности в Земстве
        /// </summary>
        public EnumPositions PositionId { get; set; }
        /// <summary>
        /// id документа с решением о выборе пользователя на должность
        /// </summary>
        public Guid DocumentElectionId { get; set; }
        /// <summary>
        /// id документа с решение о снятии пользователя с должности
        /// </summary>
        public Guid? DocumentRecallId { get; set; }
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTimeOffset DTCUTC { get; set; }
        /// <summary>
        /// id создателя записи
        /// </summary>
        public Guid UserCreatorId { get; set; }
        /// <summary>
        /// Id пользователя, назначенного на должность
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Id Земства, в котором учреждена должность
        /// </summary>
        public Guid ZemstvoId { get; set; }


        /// <summary>
        /// Земство, в котором учреждена должность
        /// </summary>
        public Zemstvo Zemstvo { get; set; }
        /// <summary>
        /// Документ с решением о выборе пользователя на должность
        /// </summary>
        public Document DocumentElection { get; set; }
        /// <summary>
        /// Документ с решением о снятии пользователя с должности
        /// </summary>
        public Document DocumentRecall { get; set; }
        /// <summary>
        /// Должность, на которую избрали человека в данном решении
        /// </summary>
        public Position Position { get; set; }
        /// <summary>
        /// Пользователь, выбранный на должность
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Пользователь, создавший запись
        /// </summary>
        public User Creator { get; set; }



        /// <summary>
        /// Документы (оригиналы), проверенные должностным лицом, назначенным на должность согласно данной записи
        /// </summary>
        public ICollection<Document> CheckedDocuments { get; set; }
        /// <summary>
        /// Документы (оригиналы), принятые должностным лицом, назначенным на должность согласно данной записи
        /// </summary>
        public ICollection<Document> AcceptorsDocuments { get; set; }
        /// <summary>
        /// Документы, направленные должностному лицу, назначенному на должность согласно данной записи
        /// </summary>
        public ICollection<Document> RecipientsDocuments { get; set; }
        /// <summary>
        /// Документы, выпущенные должностным лицом, назначенным на должность согласно данной записи
        /// </summary>
        public ICollection<Document> CreatorDocuments { get; set; }
        public int ShowId { get; set; }
    }
}
