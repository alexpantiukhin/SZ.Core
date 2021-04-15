using System.ComponentModel.DataAnnotations;

namespace SZ.Core.Constants
{
    /// <summary>
    /// Тип вопроса протокола десятки
    /// </summary>
    public enum EnumQuestionType
    {
        /// <summary>
        /// Произвольный вопрос
        /// </summary>
        [Display(Name = "Произвольный")]
        Arbitrary = 0,
        /// <summary>
        /// Выбор кандидата в десятке
        /// </summary>
        [Display(Name = "Выбор кандидата")]
        ChoicePerson = 1,
        /// <summary>
        /// Голосование за/против
        /// </summary>
        [Display(Name = "За или против")]
        Boolean = 2,
        /// <summary>
        /// Выбор из вариантов
        /// </summary>
        [Display(Name = "Выбор из вариантов")]
        ChoiceInVariants = 3,
        /// <summary>
        /// Поддержать дальнейшее обсуждение вопроса
        /// (для 2 и последующих кругов)
        /// </summary>
        [Display(Name = "Поддержка вопроса предыдущего круга")]
        SupportQuestion = 4
    }
}
