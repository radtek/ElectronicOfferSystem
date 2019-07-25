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
            // 处理特例
            if ("房屋用途".Equals(name))
            {
                dic.Add("10", "住宅");
                dic.Add("11", "成套住宅");
                dic.Add("111", "别墅");
                dic.Add("112", "高档公寓");
                return dic;
            }
            if ("国家和地区".Equals(name))
            {
                dic.Add("142", "中华人民共和国");
                dic.Add("1421", "香港特别行政区");
                return dic;
            }
            if ("权利类型".Equals(name))
            {
                dic.Add("1", "集体土地所有权");
                dic.Add("2", "国家土地所有权");
                dic.Add("3", "国有建设用地使用权");
                dic.Add("4", "国有建设用地使用权 / 房屋（构筑物）所有权");
                dic.Add("5", "宅基地使用权");
                dic.Add("6", "宅基地使用权 / 房屋（构筑物）所有权");
                dic.Add("7", "集体建设用地使用权");
                dic.Add("8", "集体建设用地使用权 / 房屋（构筑物）所有权");
                return dic;
            }
            if ("不动产单元类型".Equals(name))
            {
                dic.Add("1", "实测户");
                dic.Add("2", "预测户");
                dic.Add("3", "使用权宗地");
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

    }
}
