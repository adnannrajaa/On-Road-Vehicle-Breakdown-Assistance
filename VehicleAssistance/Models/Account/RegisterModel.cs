using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleAssistance.Models.DataBaseModel;

namespace VehicleAssistance.Models.Account
{
    public class RegisterModel
    {
        public User User { get; set; }
        public CustomerDetail CustomerDetail { get; set; }
        public MechanicsDetail MechanicsDetail { get; set; }
       
    }
}