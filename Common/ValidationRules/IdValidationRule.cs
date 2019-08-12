using Common.Enums;
using Common.Rules;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Common.ValidationRules
{
    public class IdValidationRule : ValidationRule
    {

        public ValidationParams Params
        {
            get; set;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // 非空验证
            NotEmptyValidationRule notEmptyValidation = new NotEmptyValidationRule();
            if (!notEmptyValidation.Validate(value, cultureInfo).IsValid)
                return notEmptyValidation.Validate(value, cultureInfo);

            //if (value.ToString() == "0") return ValidationResult.ValidResult;
            ValidationResult validationResult = new ValidationResult(true,null);
            switch ((EIdType)int.Parse(Params.Data.ToString()))
            {
                case EIdType.SFZ:
                    if(!RuleHelper.IsID(EIdType.SFZ, value))
                        validationResult = new ValidationResult(false, "身份证号格式错误");
                    break;
                case EIdType.GATSFZ:
                    if (!RuleHelper.IsID(EIdType.GATSFZ, value))
                        validationResult = new ValidationResult(false, "港澳台身份证号格式错误");
                    break;
                case EIdType.HZ:
                    if (!RuleHelper.IsID(EIdType.HZ, value))
                        validationResult = new ValidationResult(false, "护照格式错误");
                    break;
                case EIdType.HKB:
                    if (!RuleHelper.IsID(EIdType.HKB, value))
                        validationResult = new ValidationResult(false, "户口簿格式错误");
                    break;
                case EIdType.JGZ:
                    if (!RuleHelper.IsID(EIdType.JGZ, value))
                        validationResult = new ValidationResult(false, "军官证（士兵证）号格式错误");
                    break;
                case EIdType.ZZJGDM:
                    if (!RuleHelper.IsID(EIdType.ZZJGDM, value))
                        validationResult = new ValidationResult(false, "组织机构代码格式错误");
                    break;
                case EIdType.YYZZ:
                    if (!RuleHelper.IsID(EIdType.YYZZ, value))
                        validationResult = new ValidationResult(false, "营业执照格式错误");
                    break;
                case EIdType.QT:
                    if (!RuleHelper.IsID(EIdType.QT, value))
                        validationResult = new ValidationResult(false, "其它号码格式错误");
                    break;
                default:
                    break;
            }
            return validationResult;
        }
    }

    public class ValidationParams : DependencyObject
    {
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
           "Data", typeof(object), typeof(ValidationParams), new FrameworkPropertyMetadata(null));

        public object Data
        {
            get { return GetValue(DataProperty); }
            set
            {
                SetValue(DataProperty, value);
            }
        }
    }
}
