using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleAssistance.Models.Customer
{
    public class CustomerModel
    {
        public int RequestId { get; set; }
        public int MechanicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
    }
}