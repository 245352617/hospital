using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace YiJian.Tray.Utils
{
    public class TreeViewItemModel
    {
        public string? Header { get; set; }

        public string? Name { get; set; }
        public ObservableCollection<TreeViewItemModel> Items { get; set; }

        public TreeViewItemModel()
        {
            Items = new ObservableCollection<TreeViewItemModel>();
        }
    }

    public class TreeViewItemHelper
    {
        /// <summary>
        /// 把 System.Windows.ControlsTreeViewItem 转换成自定义 TreeViewItemModel
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static TreeViewItemModel ConvertToTreeViewItemModel(TreeViewItem item)
        {
            TreeViewItemModel model = new TreeViewItemModel();
            model.Header = item.Header.ToString();
            model.Name = item.Name.ToString();
            model.Items = new ObservableCollection<TreeViewItemModel>();
            foreach (object childItem in item.Items)
            {
                TreeViewItem childTreeViewItem = childItem as TreeViewItem;
                if (childTreeViewItem != null)
                {
                    TreeViewItemModel childModel = ConvertToTreeViewItemModel(childTreeViewItem);
                    model.Items.Add(childModel);
                }
            }
            return model;
        }

        /// <summary>
        /// 把自定义 TreeViewItemModel 转换成 System.Windows.ControlsTreeViewItem
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="treeViewItemModel"></param>
        public static void ConvertTreeViewToModel(System.Windows.Controls.TreeView treeView, TreeViewItemModel treeViewItemModel)
        {
            treeViewItemModel.Header = treeView.Name;
            foreach (object item in treeView.Items)
            {
                TreeViewItem treeViewItem = item as TreeViewItem;
                if (treeViewItem != null)
                {
                    TreeViewItemModel childModel = ConvertToTreeViewItemModel(treeViewItem);
                    treeViewItemModel.Items.Add(childModel);
                }
            }
        }
    }
}
