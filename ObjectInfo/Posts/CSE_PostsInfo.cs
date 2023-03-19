using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class CSE_PostsInfo
    {
        public decimal STT { get; set; }
        public decimal Id { get; set; }
        public decimal Post_Type { get; set; }
        public string Post_Type_Text { get; set; }
        public string Category_Id { get; set; }
        public string Category_Name { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime Start_Date { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Status_Text { get; set; }
        public string Reason { get; set; }
        public decimal Deleted { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }
    }
}
