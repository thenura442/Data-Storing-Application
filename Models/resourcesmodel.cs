using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Storing_App.Models
{
    public class resourcesmodel
    {
        public string Invoice_No { get; set; }

        public string Item_Name { get; set; }

        public string Type { get; set; }

        public double Priceper { get; set; }

        public double Quantity { get; set; }

        public string Payment_Type { get; set; }

        public string Status { get; set; }

        public double Pending_Amount { get; set; }

        public double Total_Amt { get; set; }

    }
}
