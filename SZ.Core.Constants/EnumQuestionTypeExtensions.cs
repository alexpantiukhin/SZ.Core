namespace SZ.Core.Constants
{
    public static class EnumQuestionTypeExtensions
    {
        public static string DisplayName(this EnumQuestionType type)
        {
            switch (type)
            {
                case EnumQuestionType.Arbitrary:
                    return "Произвольный";
                case EnumQuestionType.ChoicePerson:
                    return "Выбор кандидата из десятки";
                case EnumQuestionType.Boolean:
                    return "За или против";
                case EnumQuestionType.ChoiceInVariants:
                    return "Выбор из вариантов";
                case EnumQuestionType.SupportQuestion:
                    return "Поддержка вопроса предыдущего круга";
                //case EnumQuestionType.ChoiceSomeVariants:
                //    return "Выбор нескольких вариантов";
                default:
                    return null;
            }
        }
    }
}
