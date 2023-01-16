using Emgu.CV.Structure;
using Emgu.CV;
using ImageProcessor.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor.ViewModels.Smooth
{
	internal class GaussianSmoothViewModel1:ViewModelBase
	{
		private static readonly int KERNEL_WIDTH_DEFAULT_VALUE = 3;
		private static readonly int KERNEL_HEIGHT_DEFAULT_VALUE = 3;
		private static readonly double SIGMA_1_DEFAULT_VALUE = 1.0;
		private static readonly double SIGMA_2_DEFAULT_VALUE = 1.0;

		private readonly Model _model;
		private int KernelWidth
		{
			get { return KernelWidthViewModel.TextValue; }
		}
		private int KernelHeight
		{
			get { return KernelHeightViewModel.TextValue; }
		}
		private double Sigma1
		{
			get { return Sigma1ViewModel.TextValue;}
		}
		private double Sigma2
		{
			get { return Sigma2ViewModel.TextValue;}
		}
		private bool updating = false;
		public ScrollViewModel<int> KernelWidthViewModel { get; }
		public ScrollViewModel<int> KernelHeightViewModel { get; }
		public ScrollViewModel<double> Sigma1ViewModel { get; }
		public ScrollViewModel<double> Sigma2ViewModel { get; }
		public GaussianSmoothViewModel1(Model model)
		{
			_model = model;
			KernelWidthViewModel = new ScrollViewModel<int>("Kernel Width:", 1, 50, KERNEL_WIDTH_DEFAULT_VALUE);
			KernelHeightViewModel = new ScrollViewModel<int>("Kernel Height:", 1, 50, KERNEL_HEIGHT_DEFAULT_VALUE);
			Sigma1ViewModel = new ScrollViewModel<double>("Sigma 1:", 0.2, 5.0, SIGMA_1_DEFAULT_VALUE);
			Sigma2ViewModel = new ScrollViewModel<double>("Sigma 2:", 0.2, 5.0, SIGMA_2_DEFAULT_VALUE);
			KernelWidthViewModel.ValueChanged += UpdateModel;
			KernelHeightViewModel.ValueChanged += UpdateModel;
			Sigma1ViewModel.ValueChanged += UpdateModel;
			Sigma2ViewModel.ValueChanged += UpdateModel;
		}
		void UpdateModel()
		{
			if (!updating)
			{
				updating = true;
				if (KernelWidth % 2 == 1 && KernelHeight%2==1)
				{
					Image<Bgr, byte> image = _model.RawImage.ToImage<Bgr, byte>();
					Image<Bgr, byte> processedImage = image.SmoothGaussian(KernelWidth, KernelHeight, Sigma1, Sigma2);
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
