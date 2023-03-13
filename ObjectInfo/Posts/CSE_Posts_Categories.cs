using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class CSE_Posts_CategoriesInfo
    {
        public decimal STT { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string PrId { get; set; }
        public string Get_Url { get; set; }
        public decimal Lev { get; set; }
        public decimal Display_On_Menu { get; set; }
        public decimal LstOdr { get; set; }
        public decimal Deleted { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public DateTime? Modified_Date { get; set; }
    }
}
