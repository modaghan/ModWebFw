using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModWebFw
{
    public class CurrencyRate
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public decimal ForexBuying { get; set; }        
        public decimal ForexSelling { get; set; }        
        public decimal BanknoteBuying { get; set; }        
        public decimal BanknoteSelling { get; set; }

    }

    public class Doviz
    {
        public string code { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public decimal selling { get; set; }
        public decimal buying { get; set; }
        public decimal currency { get; set; }
        public decimal change_rate { get; set; }
        public int update_date { get; set; }
    }
}
