using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor.ViewModels
{
	internal class ScrollViewModel<T>:ViewModelBase where T:unmanaged, IComparable
	{
		private T _min;
		private T _max;
		private T _scrollValue;
		private T _textValue;
		public T ScrollMin
		{
			get { return _min; }
		}
		public T ScrollMax
		{
			get { return _max; }
		}
		public T ScrollValue
		{
			get { return _scrollValue; }
			set 
			{
				_scrollValue = value;
				OnScrollValueChanged();
			}
		}
		public T TextMin
		{
			get { return _min; }
			set 
			{ 
				OnMinChanged();
				_min = value;
			}
		}
		public T TextMax
		{
			get { return _max; }
			set 
			{
				_max = value; 
				OnMaxChanged();
			}
		}
		public T TextValue
		{
			get { return _textValue; }
			set
			{ 
				_textValue = value;
				OnTextValueChanged();
			}
		}
		public string VariableName { get; }
		public event Action? ValueChanged;
		public ScrollViewModel(string variableName, T min, T max, T defaultVal)
		{
			VariableName = variableName;
			_min = min;
			_max = max;
			_scrollValue = defaultVal;
			_textValue = defaultVal;
		}
		private void OnMaxChanged()
		{
			if (TextMax.CompareTo(TextMin) >= 0)
			{
				TryClampToBorder();
			}
			OnPropertyChanged(nameof(ScrollMax));
		}

		private void OnMinChanged()
		{
			if (TextMin.CompareTo(TextMax) <= 0)
			{
				TryClampToBorder();
			}
			OnPropertyChanged(nameof(ScrollMin));
		}
		private void OnScrollValueChanged()
		{
			_textValue = _scrollValue;
			OnPropertyChanged(nameof(TextValue));
			ValueChanged?.Invoke();
		}
		private void OnTextValueChanged()
		{
			_scrollValue = _textValue;
			TryToClampBoarder();
			OnPropertyChanged(nameof(ScrollValue));
			ValueChanged?.Invoke();
		}
		void TryClampToBorder()
		{
			if (ScrollValue.CompareTo(TextMax) > 0)
			{
				_scrollValue = TextMax;
				OnPropertyChanged(nameof(ScrollValue));
			}
			else if (ScrollValue.CompareTo(TextMin) < 0)
			{
				_scrollValue = TextMin;
				OnPropertyChanged(nameof(ScrollValue));
			}
		}
		void TryToClampBoarder()
		{
			if (TextMax.CompareTo(ScrollValue) < 0)
			{
				_max=ScrollValue;
				OnPropertyChanged(nameof(TextMax));
			}
			if (TextMin.CompareTo(ScrollValue) > 0)
			{
				_min = ScrollValue;
				OnPropertyChanged(nameof(TextMin));
			}
		}
	}
}
