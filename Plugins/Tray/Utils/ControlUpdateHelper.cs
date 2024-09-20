using System;
using System.Windows;
using System.Windows.Controls;

namespace YiJian.Tray.Utils
{
    /// <summary>
    /// 用于调用主线程更新控件信息
    /// </summary>
    public class ControlUpdateHelper
    {
        /// <summary>
        /// 调用主线程更新TextBlock信息
        /// </summary>
        /// <param name="controlName">控件Name</param>
        /// <param name="message">需要更新为的信息</param>
        public static void TextBlockTextUpdate(string controlName, string message)
        {
            ControlMsgUpdate(controlName, message, typeof(TextBlock));
        }

        /// <summary>
        /// 用于调用主线程更新控件信息，目前加了更新 <see cref="TextBlock.Text"/>  逻辑
        /// </summary>
        /// <param name="controlName">控件Name</param>
        /// <param name="message">需要更新为的信息</param>
        /// <param name="controlType">控件类型，如:typeof(TextBlock)</param>
        public static void ControlMsgUpdate(string controlName, string message, Type controlType)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var mainWin = Application.Current.MainWindow;
                var control = mainWin.FindName(controlName) as FrameworkElement;

                if (control != null && control.GetType() == controlType)
                {
                    if(typeof(TextBlock) == controlType)
                    {
                        var textBlock = control as TextBlock;
                        if (textBlock != null)
                        {
                            textBlock.Text = message;
                        }
                    }
                }
            }));
        }

    }
}
