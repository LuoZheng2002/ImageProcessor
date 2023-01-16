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
	internal class OptionViewModel:ViewModelBase
	{
		private readonly Model _model;
		public ObservableCollection<string> SmoothMethods { get; set; } = new()
		{
			"Gaussian Smooth",
			"Other Smooth"
		};
		public ViewModelBase? CurrentViewModel { get; set; }
		private string _currentSmoothMethod = "";
		private string CurrentSmoothMethod
		{
			get { return _currentSmoothMethod; }
			set
			{
				_currentSmoothMethod = value;
				CurrentViewModel = _strToViewModel[_currentSmoothMethod](this);
				OnPropertyChanged(nameof(CurrentViewModel));
			}
		}
		public Command SelectionChangedCommand { get; }
		public OptionViewModel(Model model)
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
				CurrentSmoothMethod = (e.AddedItems[0] as string)!;
			}
		}

		private Dictionary<string, Func<OptionViewModel, ViewModelBase>> _strToViewModel = new()
		{
			{"Gaussian Smooth", viewModel=>new MethodViewModel(viewModel._model) },
			{"Other Smooth", viewModel=>new NotFoundViewModel() }
		};
	}
}
