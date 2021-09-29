using Al.Components.DB.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

using SZ.Core.Constants;

namespace SZ.Core.Models.Db
{
    /// <summary>
    /// Документ Земств. Может выпускаться десяткой (протоколы собраний), членами земства (заявления), должностными
    /// лицами Земства (решения)
    /// </summary>
    public class Document : IDBEntity
    {
        //public Document()
        //{
        //    this.ProtocolQuestions = new HashSet<ProtocolQuestion>();
        //    this.RecallUserPositions = new HashSet<ZemstvoUserPosition>();
        //    this.ElectionUserPositions = new HashSet<ZemstvoUserPosition>();
        //    this.DocumentUsers = new HashSet<DocumentUser>();
        //    this.ExitUserTens = new HashSet<UserTen>();
        //    this.EntranceUserTens = new HashSet<UserTen>();
        //}

        public Guid Id { get; set; }
        /// <summary>
        /// Создатель документ
        /// </summary>
        public Guid UserCreatorId { get; set; }
        /// <summary>
        /// Дата и время создания документа
        /// </summary>
        public DateTimeOffset DTCUTC { get; set; }
        /// <summary>
        /// Id десятки, выпустившей протокол собрания
        /// </summary>
        public Guid? ProtocolTenId { get; set; }
        /// <summary>
        /// id документа о назначении должностного лица, создавшего документ
        /// </summary>
        public Guid? UserPositionCreatorId { get; set; }
        /// <summary>
        /// Id документа о назначении должностного лица, проверившего документ на соответствие действительности
        /// </summary>
        public Guid? UserPositionCheckerId { get; set; }
        /// <summary>
        /// Id документа о назначении должностного лица, принявшего документ
        /// </summary>
        public Guid? UserPositionAcceptorId { get; set; }
        /// <summary>
        /// Id документа о назначении должностного лица, являющегося получателем
        /// </summary>
        public Guid? UserPositionRecipientId { get; set; }
        /// <summary>
        /// Дата и время принятия оригинала (распечатанного) документа
        /// </summary>
        public DateTimeOffset? DTCUTCAccept { get; set; }
        /// <summary>
        /// Дата и время проверки оригинала (распечатанного) документа
        /// </summary>
        public DateTimeOffset? DTCUTCCheck { get; set; }
        /// <summary>
        /// Тип документа
        /// </summary>
        public EnumDocumentType Type { get; set; }
        /// <summary>
        /// Статус документа
        /// </summary>
        public EnumDocumentStatus Status { get; set; }
        /// <summary>
        /// Фактическая дата документа (на распечатанном экземпляре)
        /// </summary>
        public DateTime? FactDateDocument { get; set; }
        /// <summary>
        /// Дата и время принятия документа в архив главой секретариата
        /// </summary>
        public DateTimeOffset? DTUTCAcceptArchive { get; set; }
        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Статус (стадия) протокола/собрания
        /// </summary>
        public EnumTenProtocolStatus ProtocolStatus { get; set; }
        /// <summary>
        /// Кворум собрания десятки на момент его начала
        /// </summary>
        public int? MeetingQuorum { get; set; }
        /// <summary>
        /// Кворум голосования по вопросу на момент начала собрания десятки
        /// </summary>
        public int? VotingQuorum { get; set; }
        public Guid ZemstvoId { get; set; }
        /// <summary>
        /// Id должности, на которую назначен пользователь или снят с неё этим документом.
        /// Для документов-решений
        /// </summary>
        public EnumPositions? PositionId { get; set; }
        /// <summary>
        /// Обязателен ли бумажный вариант для хранения в архиве
        /// и при приёме секретариатом
        /// </summary>
        public bool RequirePaper { get; set; }
        public int ShowId { get; set; }





        /// <summary>
        /// Документ о назначении должностного лица, создавшего своё решение
        /// </summary>
        public ZemstvoUserPosition UserPositionCreator { get; set; }
        /// <summary>
        /// Документ о назначении должностного лица, проверившего документ
        /// </summary>
        public ZemstvoUserPosition UserPositionChecker { get; set; }
        /// <summary>
        /// Документ о назначении должностного лица, принявшего документ
        /// </summary>
        public ZemstvoUserPosition UserPositionAcceptor { get; set; }
        /// <summary>
        /// Документа о назначении должностного лица, являющегося получателем
        /// </summary>
        public ZemstvoUserPosition UserPositionRecipient { get; set; }
        /// <summary>
        /// Создатель записи
        /// </summary>
        public User UserCreator { get; set; }
        /// <summary>
        /// Десятка, выпустившая протокол
        /// </summary>
        public Ten ProtocolTen { get; set; }
        public Zemstvo Zemstvo { get; set; }
        /// <summary>
        /// Должность, на которую назначен пользователь или снят с неё этим документом.
        /// Для документов-решений
        /// </summary>
        public Position Position { get; set; }



        /// <summary>
        /// Вопросы протокола
        /// </summary>
        public ICollection<ProtocolQuestionRepeat> ProtocolQuestionRepeats { get; set; }
        /// <summary>
        /// Пользователи документа
        /// </summary>
        public ICollection<DocumentUser> DocumentUsers { get; set; }
        /// <summary>
        /// Пользователи, вошедшие в десяток, согласно данного решения
        /// </summary>
        public ICollection<UserTen> EntranceUserTens { get; set; }
        /// <summary>
        /// Пользователи, вышедшие из десятка, согласно данного решения
        /// </summary>
        public ICollection<UserTen> ExitUserTens { get; set; }
        /// <summary>
        /// Должности, назначенные документом
        /// </summary>
        public ICollection<ZemstvoUserPosition> ElectionUserPositions { get; set; }
        /// <summary>
        /// Должности, снятые документом
        /// </summary>
        public ICollection<ZemstvoUserPosition> RecallUserPositions { get; set; }

        public override string ToString()
        {
            var idString = "(" + Id.ToString() + ")";
            var user = DocumentUsers?.FirstOrDefault();
            var userName = user?.ToString() + "(" + user?.Id + ")";
            var decisionPosition = UserPositionCreator?.Position?.Name;
            switch (Type)
            {
                case EnumDocumentType.StatementInput:
                    return idString + " Заявление о вступлении в Земство пользователя " + userName;
                case EnumDocumentType.StatementOutput:
                    return idString + " Заявление о выходе из Земства пользователя " + userName;
                case EnumDocumentType.StatementTransfer:
                    return idString + " Заявление о переходе в другую десятку пользователя " + userName;
                case EnumDocumentType.ProtocolTen:
                    return idString + " Протокол собрания десятки " + ProtocolTenId;
                case EnumDocumentType.DecisionOusterPosition:
                    return idString + " Решение о назначении на должность " + Position.Name + " " + userName;
                case EnumDocumentType.DecisionAppointmentPosition:
                    return idString + " Решение " + decisionPosition + " о снятии с должности " + Position.Name + " " + userName;
                case EnumDocumentType.DecisionArbitrary:
                    return idString + " Решение " + decisionPosition + " о снятии с должности " + Position.Name + " " + userName;
                case EnumDocumentType.DecisionTransfer:
                    return idString + " Решение " + decisionPosition + " о переводе из одной десятки в другую пользователя " + userName;
                default:
                    return idString + " Неопределённый документ";
            }
        }
    }
}
