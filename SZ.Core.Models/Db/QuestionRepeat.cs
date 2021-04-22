using System;
using System.Collections.Generic;

using SZ.Core.Constants;
using SZ.Core.Models.Interfaces;

namespace SZ.Core.Models.Db
{
    /// <summary>
    /// Повтор обсуждения вопроса.
    /// В рамках повтора вопрос не может изменяться.
    /// </summary>
    public class QuestionRepeat : IDBEntity
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Номер повторного обсуждения вопроса
        /// </summary>
        public int SequenceNumber { get; set; }

        public Guid QuestionId { get; set; }

        /// <summary>
        /// Суть вопроса до 250 символов
        /// </summary>
        public string Essence { get; set; }
        /// <summary>
        /// Комментарий и описание вопроса
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата и время внесения версии контента
        /// </summary>
        public DateTimeOffset DTCUTC { get; set; }

        public Guid UpdaterId { get; set; }

        /// <summary>
        /// Причина внесения новой версии
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// id должности, которую выбирают в этом вопросе
        /// </summary>
        public EnumPositions? PositionId { get; set; }

        /// <summary>
        /// Максимальное количество вариантов, которые можно выбрать из предложенных
        /// </summary>
        public byte MaxChoiceVariantCount { get; set; }

        /// <summary>
        /// Количество вариантов, которые выбираются вопросом
        /// </summary>
        public byte VariantCount { get; set; }

        /// <summary>
        /// Повтор является последний в вопросе
        /// </summary>
        public bool IsLast { get; set; }

        /// <summary>
        /// Каждый вариант для голосования должен набрать кворму.
        /// Если флаг не установлен, то выбираются варианты,
        /// набравшие простое большинство
        /// </summary>
        public bool RequireVariantsQuorum { get; set; }
        public int ShowId { get; set; }


        /// <summary>
        /// Вопрос
        /// </summary>
        public Question Question { get; set; }

        /// <summary>
        /// Пользователь, внёсший изменения
        /// </summary>
        public User Updater { get; set; }

        /// <summary>
        /// Должность, которую выбирают в этом вопросе
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Ответы, предложенные для данного обсуждения вопроса
        /// </summary>
        public ICollection<QuestionRepeatAnswer> QuestionRepeatAnswers { get; set; }
        /// <summary>
        /// Протоколы, в которых вынесен на обсуждение данный вопрос
        /// </summary>
        public ICollection<ProtocolQuestionRepeat> ProtocolQuestionRepeats { get; set; }
        /// <summary>
        /// Протоколы, в которых предложен данный повтор на замену
        /// </summary>
        public ICollection<ProtocolQuestionRepeat> ProtocolSupportingQuestionRepeats { get; set; }
        /// <summary>
        /// Вопросов поддержки данного повтора вопроса, которые создавались для делегатов каждого круга
        /// </summary>
        public ICollection<Question> SupportQuestions { get; set; }
    }
}
