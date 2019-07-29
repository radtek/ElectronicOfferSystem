using System.Globalization;
using System.Windows.Controls;
using Common.Rules;

namespace Common.ValidationRules
{
    public class NumbericAndWordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return ValidationResult.ValidResult;
            return RuleHelper.IsNumberAndWord(value)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "请输入数字和字母");
        }
    }
}
