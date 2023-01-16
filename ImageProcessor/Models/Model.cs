using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Models
{
    class Model
    {
		private Bitmap _rawImage;
		public Bitmap RawImage
		{
			get { return _rawImage; }
			set 
			{ 
				_rawImage = value;
				_rawImageSource = BitmapConverter.Bitmap2ImageSource(_rawImage);
			}
		}
		private ImageSource _rawImageSource;
		public ImageSource RawImageSource
		{
			get { return _rawImageSource; }
		}
		private Bitmap? _processedImage;
		public Bitmap? ProcessedImage
		{
			get { return _processedImage; }
			set
			{
				_processedImage = value;
				_processedImageSource = BitmapConverter.Bitmap2ImageSource(_processedImage!);
			}
		}
		private ImageSource? _processedImageSource;
		public ImageSource? ProcessedImageSource
		{
			get { return _processedImageSource; }
		}
		public event Action? RawImageChanged;
		public event Action? ProcessedImageChanged;
		public Model()
		{
			_rawImage = new Bitmap("C:/ss/screen.png");
			_rawImageSource = BitmapConverter.Bitmap2ImageSource(_rawImage);
		}
		public void UpdateRawImage(Bitmap bitmap)
		{
			RawImage = bitmap;
			RawImageChanged?.Invoke();
		}
		public void UpdateProcessedImage(Bitmap bitmap)
		{
			ProcessedImage = bitmap;
			ProcessedImageChanged?.Invoke();
			Console.WriteLine("Processed Image Updated!");
		}
	}
}
