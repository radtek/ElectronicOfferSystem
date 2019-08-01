using BusinessData;
using BusinessData.Dal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Common.ValidationRules
{
    public class ProjectNameUniqueValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // 非空验证
            NotEmptyValidationRule notEmptyValidation = new NotEmptyValidationRule();
            if (!notEmptyValidation.Validate(value, cultureInfo).IsValid)
                return notEmptyValidation.Validate(value, cultureInfo);
            // 唯一性验证
            ProjectDal projectDal = new ProjectDal();
            int count = projectDal.GetListBy(p => p.ProjectName == value.ToString()).Count;
            return count < 1
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "项目名称已存在");
        }
    }
}
