using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FusianValid.Wpf
{
    public class Error
    {
        public static readonly DependencyProperty ObserveProperty =
            DependencyProperty.RegisterAttached("Observe", typeof(string), typeof(Error));

        public static string GetObserve(FrameworkElement element)
        {
            return (string)element.GetValue(ObserveProperty);
        }

        public static void SetObserve(FrameworkElement element, string property)
        {
            element.SetValue(ObserveProperty, property);
            element.DataContextChanged += DataContextChanged;

            DataContextChanged(element, default(DependencyPropertyChangedEventArgs));
        }

        private static void DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is FrameworkElement control)
            {
                if (control.DataContext is IValidationContextHolder holder)
                {
                    holder.ValidationContext.PropertyChanged +=
                        (s2, e2) => ValidationResultChanged(control, s2, e2);
                }
                else if (control.DataContext != null)
                {

                    throw new ArgumentException($"{control.GetType().Name} has DataContext that is not IValidationContextHolder.");
                }
            }
        }

        private static void ValidationResultChanged(FrameworkElement control, object sender, PropertyChangedEventArgs e)
        {
            if (sender is ValidationContext context
                && e.PropertyName == nameof(ValidationContext.ErrorMessages))
            {
                var propNm = GetObserve(control);

                if (context.ErrorMessages.TryGetValue(propNm, out var message))
                {
                    if (control.Tag is null) control.Tag = "error";
                    control.ToolTip = message;
                }
                else
                {
                    if ("error".Equals(control.Tag)) control.Tag = null;
                    control.ToolTip = message;
                }
            }
        }
    }
}
