using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF_16_2016_GUI.NoviGUI.DodavanjeIzmena
{
    class ValidatorInt : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string unetiString = (string)value;

            int broj;
            if (string.IsNullOrEmpty(unetiString))
            {
                return new ValidationResult(false, "Ovo polje je obavezno!");
            }
            try
            {
                broj = int.Parse(unetiString);
                if (broj < 0)
                {
                    return new ValidationResult(false, "Broj mora biti veci od 0!");
                }
            }
            catch
            {
                return new ValidationResult(false, "Unesite ceo broj!");

            }
            return new ValidationResult(true, null);
        }
    }
}
