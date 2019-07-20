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
            CONSTCLSDal constclsDal = new CONSTCLSDal();
            CONSTDal constDal = new CONSTDal();
            if (name == null) return null;
            CONSTCLS catalog = new CONSTCLS();
            // 获取目录
            catalog = constclsDal.GetModel(c => c.CONSTCLSNAME == name);
            if (catalog == null) return null;
            List<CONST> constList = new List<CONST>();
            // 获取字典List
            constList = constDal.GetListBy(c => c.CONSTSLSID == catalog.CONSTSLSID);
            if (constList == null) return null;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in constList)
            {
                // 去掉重复键值
                if (dic.ContainsKey(item.CONSTVALUE)) continue;
                dic.Add(item.CONSTVALUE, item.CONSTTRANS);
            }

            return dic;
        }

        /// 房屋结构
        ///"1"="钢结构"
        ///"2"="钢和钢筋混凝土结构"
        ///"3"="钢筋混凝土结构"
        ///"4"="混合结构"
        ///"5"="砖木结构"
        ///"6"="其它结构"

    }
}
