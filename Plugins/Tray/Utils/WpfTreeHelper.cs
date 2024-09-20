using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace YiJian.Tray.Utils
{
    public class WpfTreeHelper
    {
        static string? GetTypeDescription(object obj)
        {
            return obj.GetType().FullName;
        }

        /// <summary>
        /// 获取逻辑树
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TreeViewItem? GetLogicTree(DependencyObject? obj)
        {
            if (obj == null)
            {
                return null;
            }
            //创建逻辑树的节点
            TreeViewItem treeItem = new TreeViewItem { Header = GetTypeDescription(obj), IsExpanded = true };

            //循环遍历，获取逻辑树的所有子节点
            foreach (var child in LogicalTreeHelper.GetChildren(obj))
            {
                //递归调用
                var item = GetLogicTree(child as DependencyObject);
                if (item != null)
                {
                    treeItem.Items.Add(item);
                }
            }

            return treeItem;
        }

        /// <summary>
        /// 获取可视树
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TreeViewItem? GetVisualTree(DependencyObject obj)
        {
            if (obj == null)
            {
                return null;
            }

            TreeViewItem treeItem = new TreeViewItem { Header = GetTypeDescription(obj), IsExpanded = true };

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                var item = GetVisualTree(child);
                if (item != null)
                {
                    treeItem.Items.Add(item);
                }
            }

            return treeItem;
        }


    }
}
