using System;
using System.Collections.Generic;

using SZ.Core.Models.Interfaces;

namespace SZ.Core.Models.Db
{
    /// <summary>
    /// Варианты ответов на Вопрос
    /// </summary>
    public class QuestionRepeatAnswer : IDBEntity
    {
        //public QuestionAnswer()
        //{
        //    DocumentDecisions = new HashSet<DocumentDecision>();
        //}
        public Guid Id { get; set; }
        /// <summary>
        /// Id Вопроса
        /// </summary>
        public Guid QuestionRepeatId { get; set; }
        /// <summary>
        /// Вариант ответа на вопрос
        /// </summary>
        public string Answer { get; set; }

        public Guid? CandidatId { get; set; }

        /// <summary>
        /// Выбираемый кандидат
        /// </summary>
        public User Candidat { get; set; }

        /// <summary>
        /// Вопрос, для которого предложен данный ответ
        /// </summary>
        public QuestionRepeat QuestionRepeat { get; set; }

        /// <summary>
        /// Решения, в которых выбран данный ответ
        /// </summary>
        public ICollection<DocumentDecision> DocumentDecisions { get; set; }
        public int ShowId { get; set; }

        public override string ToString()
        {
            if (CandidatId == null)
                return Answer;

            return Candidat.ToString();
        }
    }
}
