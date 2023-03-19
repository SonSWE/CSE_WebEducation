using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class CSE_GroupsInfo
    {
        public decimal STT { get; set; }
        public decimal Group_Id { get; set; }
        public string Group_Name { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string Status_Text { get; set; }
        //public decimal Group_Type { get; set; }
        //public string Group_Type_Text { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public decimal Deleted { get; set; }
        public decimal Count_User { get; set; }
    }

    public class CSE_Group_Function_Info
    {
        public decimal Group_Id { get; set; }
        public decimal Function_Id { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public string Function_Name { get; set; }
    }

    public class CSE_Group_User_Info
    {
        public decimal Group_User_Id { get; set; }
        public decimal User_Id { get; set; }
        public string User_Name { get; set; }
        public string Full_Name { get; set; }
        public string Status_User_Text { get; set; }
        public string Status_User { get; set; }
        public string User_Type_Text { get; set; }
        public string Ref_Name { get; set; }
        public decimal Group_Id { get; set; }
        public string Group_Name { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public decimal Deleted { get; set; }

    }

}
