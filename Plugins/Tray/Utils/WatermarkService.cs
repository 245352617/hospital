using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YiJian.Tray.Utils
{
    public class WatermarkService
    {
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached(
            "Watermark",
            typeof(string),
            typeof(WatermarkService),
            new UIPropertyMetadata(string.Empty));

        public static string GetWatermark(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        public static readonly DependencyProperty IsWatermarkEnabledProperty = DependencyProperty.RegisterAttached(
            "IsWatermarkEnabled",
            typeof(bool),
            typeof(WatermarkService),
            new UIPropertyMetadata(false, OnIsWatermarkEnabledChanged));

        public static bool GetIsWatermarkEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsWatermarkEnabledProperty);
        }

        public static void SetIsWatermarkEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsWatermarkEnabledProperty, value);
        }

        private static void OnIsWatermarkEnabledChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var textBox = obj as TextBox;
            if (textBox == null)
            {
                return;
            }

            var isEnabled = (bool)e.NewValue;
            if (isEnabled)
            {
                textBox.GotFocus += OnTextBoxGotFocus;
                textBox.LostFocus += OnTextBoxLostFocus;
                textBox.TextChanged += OnTextBoxTextChanged;
                SetWatermarkText(textBox);
            }
            else
            {
                textBox.GotFocus -= OnTextBoxGotFocus;
                textBox.LostFocus -= OnTextBoxLostFocus;
                textBox.TextChanged -= OnTextBoxTextChanged;
                ClearWatermarkText(textBox);
            }
        }

        private static void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            ClearWatermarkText(textBox);
        }

        private static void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            SetWatermarkText(textBox);
        }

        private static void OnTextBoxTextChanged(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                textBox.FontStyle = FontStyles.Normal;
                textBox.Foreground = SystemColors.ControlTextBrush;
            }
        }

        private static void SetWatermarkText(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = GetWatermark(textBox);
                textBox.FontStyle = FontStyles.Italic;
                textBox.Foreground = Brushes.Gray;
            }
        }

        private static void ClearWatermarkText(TextBox textBox)
        {
            if (textBox.Text == GetWatermark(textBox))
            {
                textBox.Text = string.Empty;
                textBox.FontStyle = FontStyles.Normal;
                textBox.Foreground = SystemColors.ControlTextBrush;
            }
        }
    }
}
