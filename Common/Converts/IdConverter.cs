using Common.Enums;
using Common.Rules;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Common.Converts
{
    public class IdConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string res = "0";
            try
            {

                switch ((EIdType)int.Parse(values[0].ToString()))
                {
                    case EIdType.SFZ:
                        if (!RuleHelper.IsID(EIdType.SFZ, values[1]))
                            res = "1";
                        break;
                    case EIdType.GATSFZ:
                        if (!RuleHelper.IsID(EIdType.GATSFZ, values[1]))
                            res = "2";
                        break;
                    case EIdType.HZ:
                        if (!RuleHelper.IsID(EIdType.HZ, values[1]))
                            res = "3";
                        break;
                    case EIdType.HKB:
                        if (!RuleHelper.IsID(EIdType.HKB, values[1]))
                            res = "4";
                        break;
                    case EIdType.JGZ:
                        if (!RuleHelper.IsID(EIdType.JGZ, values[1]))
                            res = "5";
                        break;
                    case EIdType.ZZJGDM:
                        if (!RuleHelper.IsID(EIdType.ZZJGDM, values[1]))
                            res = "6";
                        break;
                    case EIdType.YYZZ:
                        if (!RuleHelper.IsID(EIdType.YYZZ, values[1]))
                            res = "7";
                        break;
                    case EIdType.QT:
                        if (!RuleHelper.IsID(EIdType.QT, values[1]))
                            res = "99";
                        break;
                    default:
                        break;
                }

            }
            catch (Exception)
            {
            }
            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
