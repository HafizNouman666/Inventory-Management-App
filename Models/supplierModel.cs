using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    class supplierModel
    {
        public int sup_id { get; set; }
        public string sup_company { get; set; }
        public string sup_contactPerson { get; set; }
       public string sup_phone1 { get; set; }
       public string sup_phone2 { get; set; }
       public string sup_address { get; set; }
       public string sup_ntn { get; set; }
       public int sup_status { get; set; }
       public string get_status { get; set; }
    }
}
