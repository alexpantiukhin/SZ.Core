using System;
using System.Collections.Generic;

using SZ.Core.Models.Interfaces;

namespace SZ.Core.Models.Db
{
    /// <summary>
    /// Земство. Может быть частью другого земства
    /// </summary>
    public class Zemstvo : IDBEntity
    {
        //public Zemstvo()
        //{
        //    Tens = new HashSet<Ten>();
        //    ChildZemvstvs = new HashSet<Zemstvo>();
        //    UsersPositions = new HashSet<ZemstvoUserPosition>();
        //}
        public Guid Id { get; set; }
        public string Name { get; set; }
        ///// <summary>
        ///// Id вышестоящего Земства
        ///// </summary>
        //public Guid? ParentZemstvoId { get; set; }
        ///// <summary>
        ///// Круг земства (местный - 1, региональный - 2, федеральный - 3)
        ///// </summary>
        //public byte Circle { get; set; }
        /// <summary>
        /// Доля членов десятки, присутствующих на собрании десятки, который
        /// определяет кворум
        /// </summary>
        public double QuorumMeetingTen { get; set; }
        /// <summary>
        /// Доля членов десятки, которые проголосовали за какой-либо вопрос,
        /// необходимый для принятия этого вопроса
        /// </summary>
        public double QuorumVotingTen { get; set; }

        /// <summary>
        /// Доля поддесяток, которые должны обсудить вопрос, прежде, чем к его обсуждению
        /// приступит десятка следующего круга
        /// </summary>
        public double QuorumTensForQuestion { get; set; }
        /// <summary>
        /// Начиная с указанного в свойстве круга
        /// обязателен бумажный вариант протокола.
        /// Если указан 0, то электронный вариант
        /// не допустим на всех кругах
        /// </summary>
        public byte RequirePaperCircle { get; set; }
        /// <summary>
        /// До указанного в свойстве круга
        /// протоколы подтверждаются автоматически
        /// в момент завершения собрания.
        /// Если указан 0, то все протоколы подтверждаются
        /// секретариатом
        /// </summary>
        public byte AutoConfirmProtocolCircle { get; set; }

        ///// <summary>
        ///// Тип земства (организации).
        ///// Земства одного типа могут объединяться в
        ///// иерархию, а члены этих организаций могут
        ///// состоять только в одной на 1-м круге десятке
        ///// и могут только переходить из одного земства в другое.
        ///// Пользователи могут состоять в нескольких организациях,
        ///// если их тип разный. Тип не изменяется
        ///// </summary>
        //public string Type { get; set; }
        public int ShowId { get; set; }




        ///// <summary>
        ///// Вышестоящее Земство
        ///// </summary>
        //public Zemstvo ParentZemstvo { get; set; }

        /// <summary>
        /// Десятки Земства
        /// </summary>
        public ICollection<Ten> Tens { get; set; }
        /// <summary>
        /// Нижестоящие Земства
        /// </summary>
        public ICollection<Zemstvo> ChildZemvstvs { get; set; }
        /// <summary>
        /// Должности, учреждённые в Земстве и пользователи, назначенные на них
        /// </summary>
        public ICollection<ZemstvoUserPosition> UsersPositions { get; set; }
        public ICollection<Question> Questions { get; set; }
        /// <summary>
        /// Документы, выпущенные в рамках земства
        /// </summary>
        public ICollection<Document> Documents { get; set; }
    }
}
