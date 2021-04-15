namespace SZ.Core.Constants
{
    /// <summary>
    /// Статус документ
    /// </summary>
    public enum EnumDocumentStatus
    {
        /// <summary>
        /// Документ в работе (не сдан в секретариат)
        /// </summary>
        InWork = 0,
        /// <summary>
        /// Документ готов к проверке
        /// </summary>
        ForCheck = 1,
        /// <summary>
        /// Документ принят секретариатом на проверку
        /// </summary>
        Accept = 2,
        /// <summary>
        /// Документ проверен секретариатом (не подлежит изменению)
        /// </summary>
        Confirmation = 3,


    }
}
