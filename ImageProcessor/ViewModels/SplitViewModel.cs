using ImageProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ImageProcessor.ViewModels
{
    class SplitViewModel:ViewModelBase
    {
        private readonly Model _model;
		public ImageSource RawImageSource
		{
			get { return _model.RawImageSource; }
		}
		public ImageSource? ProcessedImageSource
		{
			get { return _model.ProcessedImageSource; }
		}
		public SplitViewModel(Model model)
        {
            _model = model;
			_model.RawImageChanged += OnRawImageChanged;
			_model.ProcessedImageChanged += OnProcessedImageChanged;
		}
		private void OnRawImageChanged()
		{
			OnPropertyChanged(nameof(RawImageSource));
		}
		private void OnProcessedImageChanged()
		{
			OnPropertyChanged(nameof(ProcessedImageSource));
		}
	}
}
