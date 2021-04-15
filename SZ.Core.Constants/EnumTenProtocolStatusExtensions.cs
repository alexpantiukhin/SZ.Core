namespace SZ.Core.Constants
{
    public static class EnumTenProtocolStatusExtensions
    {
        public static string ToStr(this EnumTenProtocolStatus status)
        {
            switch (status)
            {
                case EnumTenProtocolStatus.NotStarted:
                    return "Не началось";
                case EnumTenProtocolStatus.Started:
                    return "Началось";
                case EnumTenProtocolStatus.Ended:
                    return "Закончилось";
                default:
                    return "";
            }
        }
    }
}
