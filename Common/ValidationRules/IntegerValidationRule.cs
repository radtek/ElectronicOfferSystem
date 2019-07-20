using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

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
            string text = value.ToString();
            // 匹配整数
            string pattern = @"^\d+$";
            return Regex.IsMatch(text, pattern)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "请输入整数");
        }
    }
}