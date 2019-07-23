using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Common.Rules;

namespace Common.ValidationRules
{
    /// <summary>
    /// 整数验证
    /// </summary>
    public class IntegerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {  
            if (value == null) return ValidationResult.ValidResult;
            return RuleHelper.IsInteger(value)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "请输入整数");
        }
    }
}