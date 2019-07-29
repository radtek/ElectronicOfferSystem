using Common.Rules;
using System.Globalization;
using System.Windows.Controls;

namespace Common.ValidationRules
{
    public class ZDDMValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // 非空验证
            NotEmptyValidationRule notEmptyValidation = new NotEmptyValidationRule();
            if (!notEmptyValidation.Validate(value, cultureInfo).IsValid)
                return notEmptyValidation.Validate(value, cultureInfo);
            // 数字和字母验证
            NumbericAndWordValidationRule numbericAndWordValidation = new NumbericAndWordValidationRule();
            if (!numbericAndWordValidation.Validate(value, cultureInfo).IsValid)
                return numbericAndWordValidation.Validate(value, cultureInfo);
            // 长度为28位验证
            return RuleHelper.IsRequiredLength(value, 19)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "长度应是19位");
        }
    }
}
