using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleAssistance.Models.Admin
{
    public class UsersFeedbackModel
    {
        public int? UserRoleId { get; set; }
        public string FullName { get; set; }
        public string Feedbackdata { get; set; }
    }
}