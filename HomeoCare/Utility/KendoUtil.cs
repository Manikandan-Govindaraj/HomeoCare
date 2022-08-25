using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeoCare.Utility
{
    public class KendoUtil
    {

    }
    
    public class SortDescription
    {
        public string field { get; set; }
        public string dir { get; set; }
    }

    public class FilterContainer
    {
        public List<FilterDescription> filters { get; set; }
        public string logic { get; set; }
    }

    public class FilterDescription
    {
        public string @operator { get; set; }
        public string field { get; set; }
        public string value { get; set; }
    }
}


