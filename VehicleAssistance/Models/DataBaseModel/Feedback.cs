//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VehicleAssistance.Models.DataBaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> RequestId { get; set; }
        public string FeedbackData { get; set; }
    
        public virtual CustomerRequest CustomerRequest { get; set; }
        public virtual User User { get; set; }
    }
}
