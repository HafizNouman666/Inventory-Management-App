using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    class productModel
    {
        public int pro_id { get; set; }
        public string pro_name { get; set; }
        public string pro_barcode { get; set; }
        public decimal pro_price { get; set; }
        public string pro_expiry { get; set; }
        public DateTime getexpiry { get; set; }
        public int pro_catID { get; set; }
        public string pro_Category { get; set; }
    }
}
