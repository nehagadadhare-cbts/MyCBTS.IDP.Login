//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Automate.DataAccess.edmx
{
    using System;
    using System.Collections.Generic;
    
    public partial class TokenHistory
    {
        public int ID { get; set; }
        public string InvoiceNumber { get; set; }
        public System.Guid Guid { get; set; }
        public System.DateTime InsertedDate { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
    }
}