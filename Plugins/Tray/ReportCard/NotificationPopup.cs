using System;
using System.Windows.Controls.Primitives;

namespace Tray.ReportCard
{
    public class NotificationPopup
    {
        private Popup _popup = new Popup();
        private int _popupDisplaySec; // 提示框显示时间（秒）
        private string Message { get; set; } = string.Empty;

        public NotificationPopup(string msg, int popupDisplaySec = 10)
        {
            this._popupDisplaySec = popupDisplaySec;
            CreateNotificationPopup(msg);
        }

        /// <summary>
        /// 初始化 Popup 控件
        /// </summary>
        /// <param name="msg">要显示的内容文本</param>
        private void CreateNotificationPopup(string msg)
        {
            // 创建一个 Popup 控件用于显示提示内容
            _popup = new Popup();
            _popup.Placement = PlacementMode.Absolute; // 使用绝对位置模式
            _popup.PopupAnimation = PopupAnimation.Fade;

            // 将用户控件或窗口设置为 Popup 的内容
            SetMsg(msg);
        }
        /// <summary>
        /// 设置要显示的内容文本
        /// </summary>
        /// <param name="msg">要显示的内容文本</param>
        public void SetMsg(string msg)
        {
            _popup.Child = new System.Windows.Controls.Label()
            {
                Content = msg,
                FontSize = 18,
                Background = System.Windows.Media.Brushes.LightBlue,
                Height = 50,
                Width = 240,
            };
        }
        /// <summary>
        /// 显示Popup
        /// </summary>
        public void ShowNotification()
        {
            // 获取屏幕的工作区域（不包括任务栏）
            var screen = System.Windows.SystemParameters.WorkArea;

            // 设置提示框的位置
            double leftOffset = 10;
            double topOffset = 5; // 将提示框置于屏幕顶部

            _popup.HorizontalOffset = leftOffset;
            _popup.VerticalOffset = topOffset;
            _popup.IsOpen = true;

            // 设置一个定时器，在一定时间后关闭提示框
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(_popupDisplaySec);
            timer.Tick += (sender, e) =>
            {
                _popup.IsOpen = false;
                timer.Stop();
            };
            timer.Start();
        }

        public void CloseNotification()
        {
            _popup.IsOpen = false;
        }
    }
}
