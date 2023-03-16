using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Model
{
    public partial class Pass
    {
        public  string FullName
        {
            get
            {
                return $"{Surname} {Name} {Patromic}";
            }
        }

        public string Passport
        {
            get
            {
                return $"{PassportSeria} {PassportNumber}";
            }
        }
    }
}
