using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Common.ValidationRules
{
    /// <summary>
    /// 数字验证
    /// </summary>
    public class NumbericValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value == null) return ValidationResult.ValidResult;
            string text = value.ToString();
            // 数字验证
            string pattern = @"^(-?\d+)(\.\d+)?$";
            return Regex.IsMatch(text, pattern)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "请输入数字");
        }
    }
}
