using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleAssistance.Models.Admin
{
    public class UsersModel
    {
        public int UserId { get; set; }
        public int RequestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string CarType { get; set; }
    }
}