using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestExample
{
    public class Product
    {
        public string GTIN { get; set; }
        public decimal? PriceAmount { get; set; }
        public string Merchant { get; set; }
        public string ProductApiStep { get; set; }
    }
}
