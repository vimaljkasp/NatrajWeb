//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Platform.Sql
{
    using System;
    using System.Collections.Generic;
    
    public partial class NatrajSMSLog
    {
        public int SMSId { get; set; }
        public int ComponentId { get; set; }
        public int SMSTypeId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string SMSMessage { get; set; }
        public Nullable<System.DateTime> SMSSentTime { get; set; }
        public string SMSErrorMsg { get; set; }
        public int SMSStatus { get; set; }
    }
}
