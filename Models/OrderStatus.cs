//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderStatus
    {
        public string statusID { get; set; }
        public string orderCustomer { get; set; }
        public string status { get; set; }
        public System.DateTime statusDate { get; set; }
        public string itemDelivery { get; set; }
    
        public virtual Customers Customers { get; set; }
        public virtual ItemDelivery ItemDelivery1 { get; set; }
    }
}
