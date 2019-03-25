using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;

namespace sb_admin_2.Web.Models
{
    public partial class MultipleModelInOneView_List1
    {
        public List<User> users { get; set; }
        public List<Comment> comments { get; set; }
        public Document document { get; set; }
        public Image image2 { get; set; }
    }
}