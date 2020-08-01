using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FusianValid.WpfDemo
{
    public class MainWindowViewModel : INotifyPropertyChanged, IValidationContextHolder<MainWindowViewModel>
    {
        ValidationContext IValidationContextHolder.ValidationContext => ValidationContext;
        public ValidationContext<MainWindowViewModel> ValidationContext { get; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private string _Directory;
        public string Directory
        {
            get => _Directory;
            set
            {
                if (_Directory == value) return;
                _Directory = value;
                RaisePropertyChanged();
            }
        }

        private string _Keyword;

        public string Keyword
        {
            get => _Keyword;
            set
            {
                if (_Keyword == value) return;
                _Keyword = value;
                RaisePropertyChanged();
            }
        }


        public MainWindowViewModel() {
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
}
