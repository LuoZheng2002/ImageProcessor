using ImageProcessor.Models;
using ImageProcessor.ViewModels.Smooth;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ImageProcessor.ViewModels
{
    internal class MethodViewModel : ViewModelBase
    {
        private static readonly string ARG_OPTION_0 = "Kernel Size";
        private static readonly string ARG_OPTION_1 = "Kernel Width, Kernel Height, ...";

        private readonly Model _model;
        public ObservableCollection<string> ArgOptions { get; set; } = new()
        {
            ARG_OPTION_0,
            ARG_OPTION_1
        };

        public ViewModelBase? CurrentViewModel { get; set; }
        private string _currentArgOption = "";
        private string CurrentArgOption
        {
            get { return _currentArgOption; }
            set
            {
                _currentArgOption = value;
                CurrentViewModel = _strToViewModel[_currentArgOption](this);
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public Command SelectionChangedCommand { get; }
        public MethodViewModel(Model model)
        {
            _model = model;
            SelectionChangedCommand = new Command(OnSelectionChanged);
        }

        private void OnSelectionChanged(object? obj)
        {
            SelectionChangedEventArgs e = (obj as SelectionChangedEventArgs)!;
            if (e.AddedItems.Count > 0)
            {
                Console.WriteLine(e.AddedItems[0] as string);
                CurrentArgOption = (e.AddedItems[0] as string)!;
            }
        }

        private readonly Dictionary<string, Func<MethodViewModel, ViewModelBase>> _strToViewModel = new()
        {
            {ARG_OPTION_0, viewModel=>new GaussianSmoothViewModel0(viewModel._model) },
            {ARG_OPTION_1, viewModel=>new GaussianSmoothViewModel1(viewModel._model) }
        };
    }
}
