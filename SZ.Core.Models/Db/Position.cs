using System.Collections.Generic;

using SZ.Core.Constants;

namespace SZ.Core.Models.Db
{
    /// <summary>
    /// Должность в Земстве
    /// </summary>
    public class Position
    {
        //public Position()
        //{
        //    Questions = new HashSet<Question>();
        //    ZemstvoUsers = new HashSet<ZemstvoUserPosition>();
        //}
        public EnumPositions Id { get; set; }
        /// <summary>
        /// Название должности
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Определяет, может ли являться получателем заявлений от граждан или членов Земства
        /// </summary>
        public bool IsRecipientStatement { get; set; }
        /// <summary>
        /// Тип должности
        /// </summary>
        public EnumPositionType Type { get; set; }
        /// <summary>
        /// Должность одновременно могут занимать несколько людей
        /// </summary>
        public bool IsPosibleMany { get; set; }




        /// <summary>
        /// Вопросы, на которых было поставлено голосование по выбору кандидата на эту должность
        /// </summary>
        public ICollection<QuestionRepeat> QuestionRepeats { get; set; }
        /// <summary>
        /// Пользователи, назначенные на должность в Земствах
        /// </summary>
        public ICollection<ZemstvoUserPosition> ZemstvoUsers { get; set; }
        /// <summary>
        /// Документы, которыми пользователи назначаются или снимаются с данной должности
        /// </summary>
        public ICollection<Document> Documents { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
