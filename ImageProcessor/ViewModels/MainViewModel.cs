using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ImageProcessor.Models;

namespace ImageProcessor.ViewModels
{
	internal class MainViewModel:ViewModelBase
	{
		private readonly Model _model;
		
		public ObservableCollection<string> ProcessingOptions { get; } = new()
		{
			"Smooth",
			"Edge Detection",
			"Line Detection"
		};
		private string _currentProcessingOption = "";
		public ViewModelBase? CurrentViewModel { get; set; }
		public ViewModelBase CurrentDisplayViewModel { get; set; }
		private string CurrentProcessingOption
		{
			get { return _currentProcessingOption; }
			set 
			{ 
				_currentProcessingOption = value;
				CurrentViewModel = _strToViewModel[_currentProcessingOption](this);
				OnPropertyChanged(nameof(CurrentViewModel));
			}
		}
		public Command SelectImageCommand { get; }
		public Command SelectionChangedCommand { get; }
		public Command ToggleViewCommand { get; }

		private Dictionary<string, Func<MainViewModel, ViewModelBase>> _strToViewModel = new()
		{
			{"Smooth", mainViewModel=>new OptionViewModel(mainViewModel._model) },
			{"Edge Detection", mainViewModel=>new OptionViewModel(mainViewModel._model) },
			{"Line Detection", mainViewModel=>new OptionViewModel(mainViewModel._model) }
		};
		public MainViewModel(Model model)
		{
			_model = model;
			CurrentDisplayViewModel = new SplitViewModel(_model);
			SelectImageCommand = new Command(OnSelectImage);
			SelectionChangedCommand = new Command(OnSelectionChanged);
			ToggleViewCommand = new Command(OnToggleView);
			
		}

		private void OnToggleView(object? obj)
		{
			if (CurrentDisplayViewModel is SplitViewModel)
			{
				CurrentDisplayViewModel = new NotFoundViewModel();
				OnPropertyChanged(nameof(CurrentDisplayViewModel));
			}
			else
			{
				CurrentDisplayViewModel = new SplitViewModel(_model);
				OnPropertyChanged(nameof(CurrentDisplayViewModel));
			}
		}

		private void OnSelectionChanged(object? obj)
		{
			SelectionChangedEventArgs e = (obj as SelectionChangedEventArgs)!;
			Console.WriteLine(e.AddedItems[0] as string);
			CurrentProcessingOption = (e.AddedItems[0] as string)!;
		}

		private void OnSelectImage(object? obj)
		{
			OpenFileDialog openFileDialog= new OpenFileDialog();
			bool? result = openFileDialog.ShowDialog();
			if (result != null && result == true)
			{
				var image = new Bitmap(openFileDialog.FileName);
				_model.UpdateRawImage(image);
			}
		}
	}
}
