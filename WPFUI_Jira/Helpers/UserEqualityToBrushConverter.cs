using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WPFUI_Jira.Models;

namespace WPFUI_Jira.Helpers
{
	public class UserEqualityToBrushConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values[0] is User executor && values[1] is User currentUser)
			{
				return executor.Id == currentUser.Id ? new LinearGradientBrush(Color.FromArgb(150, 211, 211, 211), Colors.Transparent, 0): Brushes.Transparent;
			}
			return Brushes.Transparent;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
