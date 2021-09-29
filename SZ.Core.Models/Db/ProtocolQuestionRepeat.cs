using Al.Components.DB.Interfaces;

using System;
using System.Collections.Generic;

namespace SZ.Core.Models.Db
{
    /// <summary>
    /// Вопросы для обсуждения в протоколе
    /// </summary>
    public class ProtocolQuestionRepeat : IDBEntity
    {
        //public ProtocolQuestion()
        //{
        //    DocumentDecisions = new HashSet<DocumentDecision>();
        //}
        public Guid Id { get; set; }
        /// <summary>
        /// Id протокола, содержащего вопросы
        /// </summary>
        public Guid ProtocolId { get; set; }
        /// <summary>
        /// Id Вопроса для обсуждения
        /// </summary>
        public Guid QuestionRepeatId { get; set; }
        /// <summary>
        /// Комментарий к решению по вопросу
        /// </summary>
        public string DecisionComment { get; set; }
        /// <summary>
        /// Id повтора вопроса, предложенный решением этого протокола
        /// </summary>
        public Guid? SupportQuestionNewRepeatId { get; set; }
        public int ShowId { get; set; }



        /// <summary>
        /// Протокол, содержащий вопросы
        /// </summary>
        public Document Protocol { get; set; }
        /// <summary>
        /// Вопрос для обсуждения
        /// </summary>
        public QuestionRepeat QuestionRepeat { get; set; }

        /// <summary>
        /// Повтор вопроса, предложенный решением этого протокола
        /// </summary>
        public QuestionRepeat SupportQuestionNewRepeat { get; set; }

        /// <summary>
        /// Решения принятые, относительно вопроса в протоколе
        /// </summary>
        public ICollection<DocumentDecision> DocumentDecisions { get; set; }
    }
}
