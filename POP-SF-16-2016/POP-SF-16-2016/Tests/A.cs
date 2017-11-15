using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI.Tests
{
    public class A
    {
        private string ime;

        //public string GetIme()
        //{
        //    return this.ime;
        //}

        //public void SetIme(string ime)
        //{
        //    this.ime = ime;
        //}

        public string Ime
        {
            get {
                Console.WriteLine(this.ime);
                return this.ime;
            }
            set {
                this.ime = value;
            }
        }
    }
}
