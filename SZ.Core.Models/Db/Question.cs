using Al.Components.EF.Abstractions.Interfaces;

using System;
using System.Collections.Generic;

using SZ.Core.Constants;

namespace SZ.Core.Models.Db
{
    /// <summary>
    /// Вопрос для обсуждения в десятке
    /// </summary>
    public class Question : IDBEntity
    {
        //public Question()
        //{
        //    ProtocolQuestions = new HashSet<ProtocolQuestion>();
        //    QuestionAnswers = new HashSet<QuestionAnswer>();
        //}

        public Guid Id { get; set; }

        ///// <summary>
        ///// Суть вопроса до 250 символов
        ///// </summary>
        //public string Essence { get; set; }

        ///// <summary>
        ///// Комментарий и описание вопроса
        ///// </summary>
        //public string Description { get; set; }

        /// <summary>
        /// id инициатора вопроса
        /// </summary>
        public Guid InitiatorId { get; set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTimeOffset DTCUTC { get; set; }

        /// <summary>
        /// Тип вопроса
        /// </summary>
        public EnumQuestionType Type { get; set; }

        /// <summary>
        /// Обязательно ли обсуждение вопроса дальше своей десятки?
        /// </summary>
        public bool RequireNextDiscussion { get; set; }

        /// <summary>
        /// Показывать вопрос в списке вопросов обсуждения десяткой
        /// </summary>
        public bool IsShow { get; set; }

        ///// <summary>
        ///// id должности, которую выбирают в этом вопросе
        ///// </summary>
        //public EnumPositions? PositionId { get; set; }

        /// <summary>
        /// Дата и время окончания голосования по вопросу
        /// </summary>
        public DateTimeOffset? DTEnd { get; set; }

        ///// <summary>
        ///// Требуется голосование по вопросу?
        ///// </summary>
        //public bool RequireVoting { get; set; }

        /// <summary>
        /// Важность вопроса для его создателя
        /// </summary>
        public byte Rating { get; set; }

        ///// <summary>
        ///// Количество вариантов, которые можно выбрать из предложенных
        ///// </summary>
        //public byte ChoiceCount { get; set; }

        /// <summary>
        /// Произвольный вопрос требует принятия решения
        /// </summary>
        public bool RequireAcceptDecigionForArbitrarty { get; set; }

        /// <summary>
        /// Назначение кандидата
        /// </summary>
        public EnumQuestionCandidateDestination CandidateDestination { get; set; }
        /// <summary>
        /// Источник выбора кандидатов
        /// </summary>
        public EnumQuestionCandidateSource QuestionCandidateSource { get; set; }

        /// <summary>
        /// id десятки, в которой выбирается делегат
        /// </summary>
        public Guid? DelegatTenId { get; set; }
        public Guid ZemstvoId { get; set; }
        /// <summary>
        /// Id повтора вопроса, поддерживаемого делегатом
        /// </summary>
        public Guid? SupportQuestionRepeatId { get; set; }
        public int ShowId { get; set; }


        /// <summary>
        /// Инициатор вопроса
        /// </summary>
        public User Initiator { get; set; }
        /// <summary>
        /// Вопрос для этого Земства
        /// </summary>
        public Zemstvo Zemstvo { get; set; }
        /// <summary>
        /// Десятка, в которой выбирается делегат
        /// </summary>
        public Ten DelegateTen { get; set; }
        /// <summary>
        /// Повтор вопроса, поддерживаемый делегатом
        /// </summary>
        public QuestionRepeat SupportQuestionRepeat { get; set; }


        ///// <summary>
        ///// Ответы, приготовленные для данного вопроса
        ///// </summary>
        //public ICollection<QuestionAnswer> QuestionAnswers { get; set; }

        /// <summary>
        /// Повторы обсуждения вопроса
        /// </summary>
        public ICollection<QuestionRepeat> QuestionRepeats { get; set; }
    }
}
