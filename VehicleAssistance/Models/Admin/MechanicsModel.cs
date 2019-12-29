using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleAssistance.Models.Admin
{
    public class MechanicsModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public  bool? IsBlock { get; set; }
    }
}