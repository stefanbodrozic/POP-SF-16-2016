using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF_16_2016_GUI.NoviGUI.DodavanjeIzmena
{
    class ValidatorString : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string unetiString = (string)value;

            if (string.IsNullOrEmpty(unetiString))
            {
                return new ValidationResult(false, "Polje mora biti popunjeno!");
            }
            return new ValidationResult(true, null);
        }
    }
}
