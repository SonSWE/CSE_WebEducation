using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class CSE_UsersInfo
    {
        public decimal STT { get; set; }
        public decimal User_Id { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string Status_Text { get; set; }
        public decimal User_Type { get; set; }
        public string User_Type_Text { get; set; }
        public string Full_Name { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Identity_Card { get; set; }

        public decimal Deleted { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }

        public decimal Of_Group { get; set; }
        public List<CSE_FunctionsInfo> List_User_Functions { get; set; }
    }

    public class CSE_User_Function_Info
    {
        public decimal User_Id { get; set; }
        public decimal Group_Id { get; set; }
        public decimal Function_Id { get; set; }
        public string Authcode { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public string Function_Name { get; set; }
    }
}
