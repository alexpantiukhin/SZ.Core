using Al.Components.DB.Interfaces;

using System;

using SZ.Core.Constants;

namespace SZ.Core.Models.Db
{
    /// <summary>
    /// Решение документа
    /// </summary>
    public class DocumentDecision : IDBEntity
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Id повтора вопроса, обозначенного в протоколе.
        /// Вопросы содержатся только в протоколах
        /// </summary>
        public Guid? ProtocolQuestionRepeatId { get; set; }
        /// <summary>
        /// id пользователя документа
        /// </summary>
        public Guid DocumentUserId { get; set; }
        /// <summary>
        /// Решение за или против, если вопрос был в виде соответствующего голосования
        /// </summary>
        public bool? DecisionBool { get; set; }
        ///// <summary>
        ///// id кандидата, за которого проголосовал пользователь
        ///// </summary>
        //public int? DecisionPersonId { get; set; }
        /// <summary>
        /// Id ответа из вариантов ответов, предложенных инициатором вопроса,
        /// который выбрал пользователь
        /// </summary>
        public Guid? QuestionRepeatAnswerId { get; set; }
        /// <summary>
        /// Решение по произвольному вопросу
        /// </summary>
        public string DecisionArbitrary { get; set; }
        /// <summary>
        /// Данное решение является итоговым в протоколе для вопроса.
        /// Если этот флаг установлен, то решение по вопросу принято
        /// </summary>
        public bool Total { get; set; }

        /// <summary>
        /// Решение является Решением Земства
        /// </summary>
        public bool ZemstvoDecision { get; set; }

        /// <summary>
        /// Решение будет публично размещено на сайте
        /// и будет видно не членам Земств
        /// </summary>
        public bool PublicDecision { get; set; }

        /// <summary>
        /// id пользователя, который реально поставил решение 
        /// в электронном протоколе
        /// </summary>
        public Guid RealDecideUserId { get; set; }
        public int ShowId { get; set; }




        /// <summary>
        /// Пользователь документа
        /// </summary>
        public DocumentUser DocumentUser { get; set; }
        /// <summary>
        /// Вопрос, обозначенный в протоколе.
        /// Вопросы содержится только в протоколах
        /// </summary>
        public ProtocolQuestionRepeat ProtocolQuestionRepeat { get; set; }
        ///// <summary>
        ///// Кандидат, за которого проголосовал пользователь
        ///// </summary>
        //public User DecisionPerson { get; set; }
        /// <summary>
        /// Ответ из вариантов ответов, предложенных инициатором вопроса,
        /// который выбрал пользователь
        /// </summary>
        public QuestionRepeatAnswer QuestionRepeatAnswer { get; set; }

        /// <summary>
        /// Пользователя, который реально поставил решение 
        /// в электронном протоколе
        /// </summary>
        public User RealDecideUser { get; set; }



        /// <summary>
        /// Принять решение за или против
        /// </summary>
        public void Decide(bool decision, User currentUser)
        {
            DecisionBool = decision;
            QuestionRepeatAnswerId = null;
            DecisionArbitrary = null;
            RealDecideUserId = currentUser.Id;
        }

        /// <summary>
        /// Принять решение по варианту выбора
        /// </summary>
        /// <param name="question"></param>
        public void Decide(QuestionRepeatAnswer decision, User currentUser)
        {

            DecisionBool = null;
            QuestionRepeatAnswerId = decision.Id;
            DecisionArbitrary = null;
            RealDecideUserId = currentUser.Id;
        }
        /// <summary>
        /// Принять решение по произвольному вопросу
        /// </summary>
        /// <param name="question"></param>
        public void Decide(string decision, User currentUser)
        {
            DecisionBool = null;
            QuestionRepeatAnswerId = null;
            DecisionArbitrary = decision;
            RealDecideUserId = currentUser.Id;
        }


        public override string ToString()
        {
            if (ProtocolQuestionRepeat?.QuestionRepeat?.Question == null)
                return "";

            switch (ProtocolQuestionRepeat.QuestionRepeat.Question.Type)
            {
                case EnumQuestionType.Arbitrary:
                    return DecisionArbitrary;
                case EnumQuestionType.ChoicePerson:
                case EnumQuestionType.ChoiceInVariants:
                    if (QuestionRepeatAnswer == null)
                        return "";
                    return QuestionRepeatAnswer.ToString();
                case EnumQuestionType.Boolean:
                case EnumQuestionType.SupportQuestion:
                    if (DecisionBool == true)
                        return "За";
                    else if (DecisionBool == false)
                        return "Против";
                    return "";
                default:
                    return "";
            }
        }
    }
}
