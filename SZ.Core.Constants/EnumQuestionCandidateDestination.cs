using System.ComponentModel.DataAnnotations;

namespace SZ.Core.Constants
{
    /// <summary>
    /// Назначение кандидата в вопросе
    /// </summary>
    public enum EnumQuestionCandidateDestination
    {
        /// <summary>
        /// Для произвольных целей
        /// </summary>
        [Display(Name = "Произвольное")]
        NONE = 0,
        /// <summary>
        /// Выбор делегата в следующий круг
        /// </summary>
        [Display(Name = "Делегатом в следующий круг")]
        Delegate = 1,
        /// <summary>
        /// Выбор кандадата на должность
        /// </summary>
        [Display(Name = "На должность в Земстве")]
        Position = 2
    }
}
