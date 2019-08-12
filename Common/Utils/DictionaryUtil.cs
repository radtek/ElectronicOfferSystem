using BusinessData;
using BusinessData.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class DictionaryUtil
    {
        /// <summary>
        /// 根据字段名称获取字典
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionaryByName(string name)
        {
            if (name == null) return null;
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (CustomDictionary(name , ref dic))
            {
                return dic;
            }

            CONSTCLSDal constclsDal = new CONSTCLSDal();
            CONSTDal constDal = new CONSTDal();
            CONSTCLS catalog = new CONSTCLS();
            // 获取目录
            catalog = constclsDal.GetModel(c => c.CONSTCLSNAME == name);
            if (catalog == null) return null;
            List<CONST> constList = new List<CONST>();
            // 获取字典List
            constList = constDal.GetListBy(c => c.CONSTSLSID == catalog.CONSTSLSID);
            if (constList == null) return null;
            foreach (var item in constList)
            {
                // 去掉重复键值
                if (dic.ContainsKey(item.CONSTVALUE)) continue;
                dic.Add(item.CONSTVALUE, item.CONSTTRANS);
            }
            // 字典排序
            dic = dic.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);
            return dic;
        }

        /// <summary>
        /// 根据键值和字典获取 键.值 格式的字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dicName"></param>
        /// <returns></returns>
        public static string GetStringByKeyAndDic(string key, string dicName)
        {
            StringBuilder res = new StringBuilder(key); 
            Dictionary<string, string> dic = GetDictionaryByName(dicName);
            if (string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }
            if (!dic.ContainsKey(key))
            {
                return key;
            }
            return res.Append(".").Append(dic[key]).ToString();
            //return dic[key];
        }

        /// <summary>
        /// 自定义字典
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        private static bool CustomDictionary(string name, ref Dictionary<string, string> dic)
        {
            if ("权籍调查/补录".Equals(name))
            {
                dic.Add("1", "权籍调查");
                dic.Add("2", "权籍补录");
                return true;
            }
            if ("测绘类型".Equals(name))
            {
                dic.Add("1", "预测绘");
                dic.Add("2", "实测绘");
                return true;
            }


            return false;
        }
    }
}
