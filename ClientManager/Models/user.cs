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
    
    public partial class user
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password_hash { get; set; }
        public string secret { get; set; }
        public Nullable<int> person_id { get; set; }
    
        public virtual person person { get; set; }
    }
}