using System.ComponentModel;
using System;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;

namespace FusianValid.Wpf
{
    public class BindingWithErrorExtension : MarkupExtension
    {

        public BindingWithErrorExtension()
        {
        }

        public BindingWithErrorExtension(PropertyPath path)
        {
            Path = path;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var provideValueTarget = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (provideValueTarget.TargetObject is FrameworkElement control
                && Path != null)
            {
                Error.SetObserve(control, Path.Path);

                control.SetBinding(
                    (DependencyProperty)provideValueTarget.TargetProperty,
                    new Binding
                    {
                        //TypeResolver = descriptorContext.ResolveType,
                        Converter = Converter,
                        ConverterParameter = ConverterParameter,
                        ElementName = ElementName,
                        FallbackValue = FallbackValue,
                        Mode = Mode,
                        Path = Path,
                        StringFormat = StringFormat,
                        UpdateSourceTrigger = UpdateSourceTrigger,
                        //DefaultAnchor = new WeakReference(GetDefaultAnchor(descriptorContext)),
                        //NameScope = new WeakReference<INameScope>(serviceProvider.GetService<INameScope>())
                    });

                return null;
            }
            else throw new ArgumentException("BindingWithErrorExtension can only use to FrameworkElement");

        }

        //private static object GetDefaultAnchor(IServiceProvider context)
        //{
        //    // If the target is not a control, so we need to find an anchor that will let us look
        //    // up named controls and style resources. First look for the closest IControl in
        //    // the context.
        //    object anchor = context.GetFirstParent<IControl>();
        //
        //    if (anchor is null)
        //    {
        //        // Try to find IDataContextProvider, this was added to allow us to find
        //        // a datacontext for Application class when using NativeMenuItems.
        //        anchor = context.GetFirstParent<IDataContextProvider>();
        //    }
        //
        //    // If a control was not found, then try to find the highest-level style as the XAML
        //    // file could be a XAML file containing only styles.
        //    return anchor ??
        //            context.GetService<IRootObjectProvider>()?.RootObject as IStyle ??
        //            context.GetLastParent<IStyle>();
        //}

        public IValueConverter Converter { get; set; }

        public object ConverterParameter { get; set; }

        public string ElementName { get; set; }

        public object FallbackValue { get; set; } = DependencyProperty.UnsetValue;

        public BindingMode Mode { get; set; }

        [ConstructorArgument("path")]
        public PropertyPath Path { get; set; }

        public string StringFormat { get; set; }

        public UpdateSourceTrigger UpdateSourceTrigger { get; set; }
    }
}
