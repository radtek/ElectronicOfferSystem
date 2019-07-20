using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Common.ValidationRules
{
    /// <summary>
    /// 数字和非空验证
    /// </summary>
    public class NumbericAndNotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // 非空验证
            NotEmptyValidationRule notEmptyValidation = new NotEmptyValidationRule();
            if (!notEmptyValidation.Validate(value, cultureInfo).IsValid)
                return notEmptyValidation.Validate(value, cultureInfo);
            // 数字验证
            NumbericValidationRule numbericValidation = new NumbericValidationRule();
            return numbericValidation.Validate(value, cultureInfo);
        }
    }
}
