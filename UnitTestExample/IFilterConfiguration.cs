using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestExample
{
    public interface IFilterConfiguration
    {
        bool ShowWithoutPrice { get; set; }
        bool ShowByTextIfExistGtin { get; set; }
        int NumMaxPerMerchant { get; set; }
    }
}
