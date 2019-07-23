using Common.Rules;
using System.Globalization;
using System.Windows.Controls;

namespace Common.ValidationRules
{
    /// <summary>
    /// 非空验证
    /// </summary>
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return RuleHelper.IsNotEmpty(value)
               ? ValidationResult.ValidResult
               : new ValidationResult(false, "字段不能为空");
        }
    }
}
