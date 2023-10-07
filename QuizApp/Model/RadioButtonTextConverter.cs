using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace QuizApp.Model
{
    public class RadioButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is RadioButton radioButton && radioButton.IsChecked == true)
            {
                return radioButton.Content.ToString();
            }
            return Binding.DoNothing;
        }
    }
}
