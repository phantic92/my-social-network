//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientManager.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class comment_like
    {
        public int comment_id { get; set; }
        public int person_id { get; set; }
        public string timestamp { get; set; }
        public bool read { get; set; }
    
        public virtual comment comment { get; set; }
        public virtual person person { get; set; }
    }
}
