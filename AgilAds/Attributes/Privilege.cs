using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgilAds.Attributes
{
    public class Privilege
    {
        public Valid.Context Context { get; set; }
        public Valid.Action Action { get; set; }
    }
}