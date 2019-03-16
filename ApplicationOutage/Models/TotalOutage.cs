using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationOutage.Models
{
    public class TotalOutage
    {
        public int Years { get; set; }
        public Months Month { get; set; }
        public int Outage { get; set; }
    }

    public enum Months:int
        {
            January=1,
            February,
            March,
            April,
            May,
            Jun,
            July,
            August,
            September,
            Octomber,
            November,
            December

    }
}