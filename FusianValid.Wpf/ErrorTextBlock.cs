using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FusianValid.Wpf
{
    public class ErrorTextBlock : TextBlock
    {
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(nameof(Path), typeof(string), typeof(ErrorTextBlock));

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }


        public ErrorTextBlock()
        {
            this.DataContextChanged += ErrorTextBox_DataContextChanged;

            this.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void ErrorTextBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is IValidationContextHolder holder)
            {
                holder.ValidationContext.PropertyChanged += ValidationContext_PropertyChanged;

                ValidationContext_PropertyChanged(
                    holder.ValidationContext,
                    new System.ComponentModel.PropertyChangedEventArgs(nameof(ValidationContext.ErrorMessages)));
            }
        }

        private void ValidationContext_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is ValidationContext context
                && e.PropertyName == nameof(ValidationContext.ErrorMessages))
            {
                if (Path is null) return;

                if (context.ErrorMessages.TryGetValue(Path, out var message))
                {
                    Text = message;
                }
                else
                {
                    Text = null;
                }
            }
        }
    }
}
