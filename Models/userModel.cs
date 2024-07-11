using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    class userModel
    {
        public int usr_id { get; set; }
        public string usr_name { get; set; }
        public string usr_username { get; set; } 
        public string usr_password { get; set; }
        public string usr_phone { get; set; }
        public string usr_email { get; set; }
        public int usr_status { get; set; }
        public string getusr_status { get; set; }
    }
}
