using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF_16_2016_GUI.NoviGUI.DodavanjeIzmena
{
    class ValidatorDouble : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string unetiString = (string)value;
            double broj;
            if (string.IsNullOrEmpty(unetiString))
            {
                return new ValidationResult(false, "Ovo polje je obavezno!");
            }
            try
            {
                broj = double.Parse(unetiString);
                if (broj < 0)
                {
                    return new ValidationResult(false, "Broj mora biti veci od 0!");
                }
            }
            catch
            {
                return new ValidationResult(false, "Unesite ceo ili decimalan broj broj!");

            }
            return new ValidationResult(true, null);
        }
    }
}
