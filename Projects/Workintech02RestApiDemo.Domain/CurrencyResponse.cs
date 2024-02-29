using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workintech02RestApiDemo.Domain
{
    public class CurrencyResponse
    {
        public double CAD { get; set; }
        public double EUR { get; set; }
        public double TRY { get; set; }
        public double USD { get; set; }
    }

    public class CurrencyRoot
    {
        public CurrencyResponse data { get; set; }
    }
 
}
