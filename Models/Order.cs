using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Storing_App.Models
{
    public class ordermodel
    {
        public string Order_ID { get; set; }

        public string Material { get; set; }

        public double Intial_Weight { get; set; }

        public double Loaded_Weight { get; set; }

        public double Quantity { get; set; }

        public string Lorry_No { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string Month { get; set; }

        public double Perton { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public double Pending_Amt { get; set; }

        public double Total_Amt { get; set; }
    }
}
