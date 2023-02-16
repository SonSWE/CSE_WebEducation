using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class ResponseInfo
    {
        public string success { get; set; }
    }

    public class SearchResponseInfo
    {
        public string jsondata { get; set; }
        public decimal totalrows { get; set; }
    }
}
