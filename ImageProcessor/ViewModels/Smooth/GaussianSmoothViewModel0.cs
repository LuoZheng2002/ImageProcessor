using ImageProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace ImageProcessor.ViewModels.Smooth
{
	internal class GaussianSmoothViewModel0:ViewModelBase
	{
		private static readonly int KERNEL_SIZE_DEFAULT_VALUE = 3;
		private readonly Model _model;
		private int KernelSize
		{
			get { return KernelSizeViewModel.TextValue; }
		}
		private bool updating = false;
		public ScrollViewModel<int> KernelSizeViewModel { get; }
		public GaussianSmoothViewModel0(Model model)
		{
			KernelSizeViewModel = new ScrollViewModel<int>("Kernel Size:", 1, 50, KERNEL_SIZE_DEFAULT_VALUE);
			_model = model;
			KernelSizeViewModel.ValueChanged += UpdateModel;
		}
		void UpdateModel()
		{
			if (!updating)
			{
				updating = true;
				if (KernelSize % 2 == 1)
				{
					Image<Bgr, byte> image = _model.RawImage.ToImage<Bgr, byte>();
					Image<Bgr, byte> processedImage = image.SmoothGaussian(KernelSize);
					_model.UpdateProcessedImage(processedImage.ToBitmap());
				}
				else
				{
					_model.UpdateProcessedImage(new Bitmap("C:/Images/114514.jpeg"));
				}
				updating = false;
			}
		}
	}
}
