using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class TreeNode
    {
        public string Name { get; set; } // 名称

        public ObservableCollection<TreeNode> Children { get; set; }

        public bool IsExpanded { get; set; } // 节点是否展开
        public bool IsSelected { get; set; } // 节点是否选中

        public TreeNode(string name)
        {
            Name = name;
        }
        public TreeNode(string name, params TreeNode[] children)
        {
            Name = name;
            Children = new ObservableCollection<TreeNode>(children);
        }

        public virtual void AddChildren(TreeNode node)
        {
            Children.Add(node);
        }
    }
}
