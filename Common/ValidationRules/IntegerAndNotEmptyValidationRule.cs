using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Common.ValidationRules
{
    /// <summary>
    /// 整数和非空验证
    /// </summary>
    public class IntegerAndNotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // 非空验证
            NotEmptyValidationRule notEmptyValidation = new NotEmptyValidationRule();
            if (!notEmptyValidation.Validate(value, cultureInfo).IsValid)
                return notEmptyValidation.Validate(value, cultureInfo);
            // 整数验证
            IntegerValidationRule integerValidation = new IntegerValidationRule();
            return integerValidation.Validate(value, cultureInfo);
        }
    }
}
