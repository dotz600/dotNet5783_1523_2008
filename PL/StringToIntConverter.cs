using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL;

public class StringToIntConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)//int to string
    {
        if(value != null)
        {
            string res = value!.ToString();
            if (res != null)  return res;

        }
        return "";
    }
    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)//string to int
    {
        try
        {
            if (value != null)
            {
                string str = (string)value;
                return int.Parse(str);
            }
            return null;
        }
        catch(FormatException ) 
        {
            return null;
        }
        catch(Exception ex) { throw ex; }
    }
}
