namespace SZ.Core.Constants
{
    /// <summary>
    /// Тип документа
    /// </summary>
    public enum EnumDocumentType
    {
        /// <summary>
        /// Заявление о входе в Земство
        /// </summary>
        StatementInput = 1,
        /// <summary>
        /// Заявление о выходе из Земства
        /// </summary>
        StatementOutput = 2,
        /// <summary>
        /// Заявление о переходе из одной десятки 1 круга в другую
        /// </summary>
        StatementTransfer = 3,
        /// <summary>
        /// Протокол десятки
        /// </summary>
        ProtocolTen = 4,
        /// <summary>
        /// Решение должностного лица о снятии с должности
        /// </summary>
        DecisionOusterPosition = 5,
        /// <summary>
        /// Решение должностного лица о назначении на должность
        /// </summary>
        DecisionAppointmentPosition = 6,
        /// <summary>
        /// Решение должностного лица произвольное
        /// </summary>
        DecisionArbitrary = 7,
        /// <summary>
        /// Решение о переводе пользователя из одной десятки в другую
        /// </summary>
        DecisionTransfer = 8
        
    }
}
