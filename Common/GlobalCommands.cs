using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class GlobalCommands
    {
        /// <summary>
        /// 在项目列表中选择一个项目
        /// </summary>
        public static CompositeCommand SelectProjectCommand = new CompositeCommand();

        /// <summary>
        /// 楼盘业务中选择一项（如自然幢、逻辑幢）
        /// </summary>
        public static CompositeCommand SelectBusinessCommand = new CompositeCommand();
    }
}
