//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Login_UploadFile.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Document
    {
        public int id { get; set; }
        public Nullable<int> id_Student { get; set; }
        public string name_Document { get; set; }
        public string type_Document { get; set; }
        public string path_Document { get; set; }
        public Nullable<System.DateTime> date_Summit { get; set; }
        public Nullable<int> status_Document { get; set; }
    
        public virtual Account Account { get; set; }
    }
}
