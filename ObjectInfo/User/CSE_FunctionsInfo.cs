using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class CSE_FunctionsInfo
    {
        public decimal STT { get; set; }
        public decimal Function_Id { get; set; }
        public string Function_Code { get; set; }
        public string Function_Name { get; set; }
        public string Function_Icon { get; set; }
        public string Display_Name { get; set; }
        public decimal Display_On_Menu { get; set; }

        public decimal Function_Type { get; set; }
        public string Function_Url { get; set; }
        public string Function_Url_Post { get; set; }
        public decimal Position { get; set; }
        public decimal Prid { get; set; }
        public decimal Lev { get; set; }

        public decimal Added_To_Group { get; set; }
        public string Authcode { get; set; }
        public string Full_Request_Url { get; set; }
    }
}
