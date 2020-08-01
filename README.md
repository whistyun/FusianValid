# FusianValid
Validator for MVVM


## How to use

### MVVM code
```cs
public class MainWindowViewModel : INotifyPropertyChanged, IValidationContextHolder<MainWindowViewModel>
{
	ValidationContext IValidationContextHolder.ValidationContext => ValidationContext;
	public ValidationContext<MainWindowViewModel> ValidationContext { get; }

	#region MVVM Property

		public string Directory { ... }

		public string Keyword { ... }

	#endregion

	public MainWindowViewModel()
	{
		ValidationContext = FusianValid.ValidationContext.Build(this);

		ValidationContext.Add(
			"directory is not exists",
			nameof(Directory),
			Validators.DirectoryExists);
			
		ValidationContext.Add(
			"must input any word",
			nameof(Keyword),
			Validators.NotNullOrEmpty);
	}
}
```

### View (AvaloniaUI)
```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:vm="clr-namespace:MultiReptCore.ViewModels;assembly=MultiReptCore"
        xmlns:fsnv="clr-namespace:FusianValid.Avalonia;assembly=FusianValid.Avalonia"

        x:Class="MultiReptCore.Views.MainWindow"
        Width="400" Height="450"
        Title="MultiReptCore">

    <Design.DataContext>
        <vm:MainWindowViewModel/>
    </Design.DataContext>

	<StackPanel>

        <TextBox Text="{Binding Directory}" />
        <fsnv:ErrorTextBlock Path="Directory"/>

        <TextBox Text="{fsnv:BindingWithError Keyword}"/>
	
	</StackPanel>
>
```
